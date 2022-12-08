using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHubManager : MonoBehaviour
{
    void Start()
    {
        int option = Random.Range(0, 2);
        if (option == 0)
        {
            // play song 1
            float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
            AudioManager.instance.PlaySound(
                    AudioManager.instance.database.song_1, 
                    0.2f,
                    true, 
                    0.85f, 
                    "song1"
                );
        }
        else if (option == 1)
        {
            // play song 2
            float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
            AudioManager.instance.PlaySound(
                    AudioManager.instance.database.song_2, 
                    0.2f,
                    true, 
                    0.85f, 
                    "song2"
                );
        }
        
    }
}
