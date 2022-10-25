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

    public float playerSpeed = 1.0f;

    [Header("UI Options")]
    public TextMeshProUGUI positionText;

	void Update()
    {
        bool forward_input = false;
        bool backward_input = false;
        bool left_input = false;
        bool right_input = false;
        
        if (GameManager.instance.playMode == GameManager.PlayMode.KeyboardAndMouse)
        {
            forward_input = Input.GetKey(KeyCode.W);
            backward_input = Input.GetKey(KeyCode.S);
            left_input = Input.GetKey(KeyCode.A);
            right_input = Input.GetKey(KeyCode.D);
        }
        else if (GameManager.instance.playMode == GameManager.PlayMode.VRHeadset)
        {
            forward_input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > 0f;
            backward_input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y < 0f;
            left_input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < 0f;
            right_input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > 0f;
        }
        
        float forward_multiplier = (forward_input) ? 1.0f : (backward_input) ? -1.0f : 0.0f;
        float right_multiplier = (right_input) ? 1.0f : (left_input) ? -1.0f : 0.0f;

        float currentScale = * PlayerScaleController.instance.GetCurrentScale();

        transform.Translate(Camera.main.transform.forward * forward_multiplier * 0.01f * playerSpeed * currentScale
                        +   Camera.main.transform.right * right_multiplier * 0.01f * playerSpeed * currentScale);
        positionText.text = "current pos: " + transform.position;
	}
}