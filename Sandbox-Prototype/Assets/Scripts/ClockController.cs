using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    const float MIN_TO_DEG = -6f;
    const float SEC_TO_DEG = -6f;

    public Transform secondHandPivot;
    public Transform minuteHandPivot;

    private float timer = 0f;

    void Update()
    {        
        timer += Time.deltaTime;

        // update hands
        secondHandPivot.localRotation = Quaternion.Euler(0f, 0f, timer * SEC_TO_DEG);
        minuteHandPivot.localRotation = Quaternion.Euler(0f, 0f, (timer / 60f) * MIN_TO_DEG);
    }
}
