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

    public Camera playerCamera;
    public Rigidbody rb;
    public float playerSpeed = 1.0f;
    public float minSpeed = 0.1f;
    public float maxSpeed = 1f;

    [Header("UI Options")]
    public TextMeshProUGUI positionText;

	void FixedUpdate()
    {
        OVRInput.FixedUpdate();
        
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

        // get forward vector
        Vector3 forwardVector = playerCamera.transform.forward;
        forwardVector.y = 0f;
        forwardVector.Normalize();
        // get right vector
        Vector3 rightVector = playerCamera.transform.right;
        rightVector.y = 0f;
        rightVector.Normalize();
        // calculate speed regulator
        float speedRegulator = Mathf.Clamp(Mathf.Log(PlayerScaleController.instance.GetCurrentScale()), minSpeed, maxSpeed) * playerSpeed * 0.1f;
        // translate player position
        transform.Translate(forwardVector * forward_multiplier * speedRegulator + rightVector * right_multiplier * speedRegulator);
        positionText.text = "current pos: " + transform.position;
	}
}