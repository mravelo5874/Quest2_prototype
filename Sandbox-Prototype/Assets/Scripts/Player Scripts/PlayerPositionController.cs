using UnityEngine;
using System;
using TMPro;

/// <summary>
/// https://gist.github.com/KarlRamstedt/407d50725c7b6abeaf43aee802fdd88e
/// A simple FPP (First Person Perspective) camera rotation script.
/// Like those found in most FPS (First Person Shooter) games.
/// </summary>
public class PlayerPositionController : MonoBehaviour 
{
    public static PlayerPositionController instance;
    void Awake()
    {
        // set instance
        if (instance == null)
        {
            instance = this;
        }
    }

    [Header("UI Options")]
    public TextMeshProUGUI positionText;

	void Update()
    {
        if (GameManager.instance.playMode == GameManager.PlayMode.KeyboardAndMouse)
        {
            bool forward_input = Input.GetKey(KeyCode.W);
            bool left_input = Input.GetKey(KeyCode.A);
            bool right_input = Input.GetKey(KeyCode.D);
            bool backward_input = Input.GetKey(KeyCode.S);
            
            float forward_multiplier = (forward_input) ? 1.0f : (backward_input) ? -1.0f : 0.0f;
            float right_multiplier = (right_input) ? 1.0f : (left_input) ? -1.0f : 0.0f;

            transform.Translate(Camera.main.transform.forward * forward_multiplier * 0.01f
                            +   Camera.main.transform.right * right_multiplier * 0.01f);
        }

        positionText.text = "current pos: " + transform.position;
	}
}