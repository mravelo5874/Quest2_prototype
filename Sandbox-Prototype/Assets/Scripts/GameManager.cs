using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        // make this GameManager the only instance
        if (!instance)
        {
            instance = this;
        }

        // automatically set play mode
        #if UNITY_EDITOR
            playMode = PlayMode.KeyboardAndMouse;
        #else
            playMode = PlayMode.VRHeadset;
        #endif
    }

    public enum PlayMode
    {
        KeyboardAndMouse,
        VRHeadset
    }
    
    public PlayMode playMode; // which play mode are we in?

    private float resetTimer = 0f;
    private float resetHoldAmount = 2f; // hold button for 2 seconds to reset scene

    void Update()
    {
        OVRInput.Update();

        if (OVRInput.Get(OVRInput.Button.Three))
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > resetHoldAmount)
            {
                resetTimer = 0f;
                SceneManager.LoadScene("SandboxScene");
            }
        }
        else    
        {
            resetTimer = 0f;
        }
    }
}
