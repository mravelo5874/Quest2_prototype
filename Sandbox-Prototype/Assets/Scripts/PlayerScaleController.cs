using UnityEngine;
using TMPro;

public class PlayerScaleController : MonoBehaviour
{
    [Header("Scale Options")]
    public float startScale = 2.5f;
    public float maxScale;
    public float minScale;
    public float scaleRate = 1.0f;
    // private scale option variables
    private float scaleMultiplier = 1000f;
    private float currentScale = 1.0f;

    [Header("Camera Options")]
    public bool changeFOV;
    public float minFOV;
    public float maxFOV;
    // private camera option variables
    private float currentFOV;

    [Header("UI Options")]
    public TextMeshProUGUI scaleText;

    void Awake()
    {
        // set staring scale
        currentScale = startScale;
        transform.localScale = new Vector3(1f, currentScale, 1f);
        // set scale text
        scaleText.text = "current scale: " + currentScale;
    }

    void Update()
    {
        // use keyboard controls if camera control mode is mouse
        if (PlayerCameraController.instance.cameraControlMode == PlayerCameraController.CameraControlMode.Mouse)
        {            
            // left shift for growing
            bool growInput = Input.GetKey(KeyCode.LeftShift);
            bool shrinkInput = Input.GetKey(KeyCode.LeftControl);

            // boolean XOR (either grow or shrink or neither)
            if (growInput ^ shrinkInput)
            {
                float currentScaleRate = scaleRate / scaleMultiplier;
                // negate scale rate if shrinking\
                if (shrinkInput)
                {
                    currentScaleRate *= -1f;
                }

                // set current scale and clamp at min/max
                currentScale += currentScaleRate;
                if (currentScale > maxScale)
                    currentScale = maxScale;
                else if (currentScale < minScale)
                    currentScale = minScale;

                // update transform scale
                transform.localScale = new Vector3(1f, currentScale, 1f);
                scaleText.text = "current scale: " + currentScale;
            }
        }

        // update camera
        if (changeFOV)
        {
            float scale01 = Mathf.InverseLerp(maxScale, minScale, currentScale);
            float newFOV = Mathf.Lerp(maxFOV, minFOV, scale01);
            Camera.main.fieldOfView = newFOV;
        }
    }
}
