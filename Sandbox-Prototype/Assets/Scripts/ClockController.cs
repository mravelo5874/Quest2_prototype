using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public static ClockController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    const float MIN_TO_DEG = -6f;
    const float SEC_TO_DEG = -6f;

    public Transform secondHandPivot;
    public Transform minuteHandPivot;

    private float timer = 0f;
    private float prevTime = 0f;
    public float GetElapsedTime() { return timer; } // public getter

    void Update()
    {
        float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();

        prevTime = timer;
        timer += Time.deltaTime * currentTimeScale;

        // play clock tick every "second"
        if (Mathf.FloorToInt(prevTime) <Mathf.FloorToInt(timer))
        {
            AudioManager.instance.PlaySound(
                AudioManager.instance.database.clock_tick, 
                0.05f, 
                false, 
                Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                "clock_tick"
            );
        }

        // update hands
        secondHandPivot.localRotation = Quaternion.Euler(0f, 0f, timer * SEC_TO_DEG);
        minuteHandPivot.localRotation = Quaternion.Euler(0f, 0f, (timer / 60f) * MIN_TO_DEG);
    }
}
