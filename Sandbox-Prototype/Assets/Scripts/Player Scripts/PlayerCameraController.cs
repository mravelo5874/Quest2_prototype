using UnityEngine;

/// <summary>
/// https://gist.github.com/KarlRamstedt/407d50725c7b6abeaf43aee802fdd88e
/// A simple FPP (First Person Perspective) camera rotation script.
/// Like those found in most FPS (First Person Shooter) games.
/// </summary>
public class PlayerCameraController : MonoBehaviour 
{
    public static PlayerCameraController instance;

	public float Sensitivity 
    {
		get { return sensitivity; }
		set { sensitivity = value; }
	}
	[Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
	[Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
	[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

	Vector2 rotation = Vector2.zero;
    //Strings in direct code generate garbage, storing and re-using them creates no garbage
	const string xAxis = "Mouse X"; 
	const string yAxis = "Mouse Y";

    void Start()
    {
        // set instance
        if (instance == null)
        {
            instance = this;
        }
        // hide and lock mouse cursor
        if (GameManager.instance.playMode == GameManager.PlayMode.KeyboardAndMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

	void Update()
    {
        // use mouse to control player camera
        if (GameManager.instance.playMode == GameManager.PlayMode.KeyboardAndMouse)
        {
            rotation.x += Input.GetAxis(xAxis) * sensitivity;
            rotation.y += Input.GetAxis(yAxis) * sensitivity;
            rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            // transform.localRotation = xQuat * yQuat;
            // Quaternions seem to rotate more consistently than EulerAngles. 
            // Sensitivity seemed to change slightly at certain degrees using Euler. 
            transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        }
	}
}