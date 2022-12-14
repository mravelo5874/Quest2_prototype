using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePerceptionController : MonoBehaviour
{
    public static TimePerceptionController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private float timeScale = 1f;
    public float GetGameTimeScale() { return timeScale; } // public getter

    public bool changeTimeScale;
    public float maxTimeScale = 2f;
    public float minTimeScale = 0.5f;

    void FixedUpdate()
    {
        if (changeTimeScale)
        {
            float startScale = PlayerScaleController.instance.startScale;
            float currentScale = PlayerScaleController.instance.GetCurrentScale();
            float maxScale = PlayerScaleController.instance.maxScale;
            float minScale = PlayerScaleController.instance.minScale;

            if (currentScale > startScale)
            {
                timeScale = Mathf.Lerp(1f, maxTimeScale, currentScale / maxScale);
            }
            else if (currentScale <= startScale)
            {
                timeScale = Mathf.Lerp(minTimeScale, 1f, currentScale / startScale);
            }
        }
    }
}
