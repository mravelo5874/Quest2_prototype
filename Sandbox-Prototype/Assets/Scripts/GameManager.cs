using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public enum PlayMode
    {
        KeyboardAndMouse,
        VRHeadset
    }
    public PlayMode playMode; // which play mode are we in?
}
