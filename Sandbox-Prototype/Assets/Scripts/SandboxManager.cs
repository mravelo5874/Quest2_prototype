using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxManager : MonoBehaviour
{
    void Start()
    {
        // play song 4
        float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
        AudioManager.instance.PlaySound(
                AudioManager.instance.database.song_4, 
                0.5f,
                true, 
                0.85f, 
                "song4"
            );
    }
}
