using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private int prevValue = 0;

    void Start()
    {
        timerText.text = "1:00";
        prevValue = (int)TimeBombGameManager.instance.GetTimeLeft();
    }

    void Update()
    {
        int timeLeft = (int)TimeBombGameManager.instance.GetTimeLeft();
        // play sound effect
        float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
        if (prevValue != timeLeft)
        {
            AudioManager.instance.PlaySound(
                        AudioManager.instance.database.alarm_beep, 
                        0.1f, 
                        false, 
                        Mathf.Lerp(0.25f, 4f, currentTimeScale / TimePerceptionController.instance.maxTimeScale),
                        "alarm"
                    );

            if (timeLeft < 10)
            {
                AudioManager.instance.PlaySound(
                        AudioManager.instance.database.sad_blip, 
                        0.1f, 
                        false, 
                        0.8f,
                        "alarm"
                    );
            }
        }

        if (timeLeft < 60 && timeLeft >= 10)
        {
            timerText.text = "0:" + timeLeft;
        }
        else if (timeLeft < 10)
        {
            timerText.text = "0:0" + timeLeft;
        }

        prevValue = timeLeft;
    }
}
