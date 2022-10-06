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

    public enum PositionControlMode
    {
        Keyboard,
        Headset
    }
    
    public PositionControlMode positionControlMode;

	public float Sensitivity 
    {
		get { return sensitivity; }
		set { sensitivity = value; }
	}
	[Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
	[Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
	[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

	Vector3 position;

    [Header("UI Options")]
    public TextMeshProUGUI positionText;

    void Awake()
    {
        // set instance
        if (instance == null)
        {
            positionControlMode = PositionControlMode.Keyboard;
            instance = this;
        }
    }

	void Update()
    {
        if (positionControlMode == PositionControlMode.Keyboard)
        {
            bool forward_input = Input.GetKey(KeyCode.W);
            bool left_input = Input.GetKey(KeyCode.A);
            bool right_input = Input.GetKey(KeyCode.D);
            bool backward_input = Input.GetKey(KeyCode.S);
            
            float forward_multiplier = (forward_input) ? 1.0f : (backward_input) ? -1.0f : 0.0f;
            float right_multiplier = (right_input) ? 1.0f : (backward_input) ? -1.0f : 0.0f;

            transform.Translate(Camera.main.transform.forward * forward_multiplier * 0.01f
                            +   Camera.main.transform.right * right_multiplier * 0.01f);
        }
	}
}