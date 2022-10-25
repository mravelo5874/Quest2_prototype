using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public bool changeTimeScale;
    public float maxTimeScale = 2f;
    public float minTimeScale = 0.5f;

    public TextMeshProUGUI timeScaleText;

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
                Time.timeScale = Mathf.Lerp(1f, maxTimeScale, currentScale / maxScale);
            }
            else if (currentScale <= startScale)
            {
                Time.timeScale = Mathf.Lerp(minTimeScale, 1f, currentScale / startScale);
            }

            // set time scale text
            timeScaleText.text = "current time scale: " + Time.timeScale;
        }
    }
}
