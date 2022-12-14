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

    // line colors
    [SerializeField]
    public Gradient defaultLineGrad;
    [SerializeField]
    public Gradient triggerLineGrad;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        OVRInput.Update();

        if (OVRInput.Get(OVRInput.Button.Three) || Input.GetKeyDown(KeyCode.R))
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > resetHoldAmount)
            {
                resetTimer = 0f;
                SceneManager.LoadScene("MainHubScene");
            }
        }
        else    
        {
            resetTimer = 0f;
        }
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioManager.instance.PlaySound(
                AudioManager.instance.database.teleport, 
                0.5f, 
                false, 
                0.5f, 
                "teleport"
            );
    }
}
