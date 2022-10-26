using UnityEngine;
using TMPro;

public class PlayerScaleController : MonoBehaviour
{
    public static PlayerScaleController instance;

    [Header("Scale Options")]
    public float startScale = 1f;
    public float maxScale;
    public float minScale;
    public float scaleRate;
    // private scale option variables
    private float currentScale = 1f;
    public float GetCurrentScale() { return currentScale; } // public getter

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
        // set instance
        if (instance == null)
        {
            instance = this;
        }
        // set staring scale
        currentScale = startScale;
        transform.localScale = new Vector3(1f, currentScale, 1f);
        // set scale text
        scaleText.text = "current scale: " + currentScale;
    }

    void FixedUpdate()
    {
        OVRInput.FixedUpdate();

        bool growInput = false;
        bool shrinkInput = false;

        // use keyboard controls if camera control mode is mouse
        if (GameManager.instance.playMode == GameManager.PlayMode.KeyboardAndMouse)
        {            
            // left shift for growing
            growInput = Input.GetKey(KeyCode.LeftShift);
            shrinkInput = Input.GetKey(KeyCode.LeftControl);
        }
        else if (GameManager.instance.playMode == GameManager.PlayMode.VRHeadset)
        {
            growInput = OVRInput.Get(OVRInput.Button.Two);
            shrinkInput = OVRInput.Get(OVRInput.Button.One);
        }


        // boolean XOR (either grow or shrink or neither)
        if (growInput ^ shrinkInput)
        {
            float currentScaleRate = scaleRate;
            // negate scale rate if shrinking
            if (shrinkInput)
            {
                currentScaleRate *= -1f;
            }

            if (currentScale >= 1f)
            {
                currentScale += currentScaleRate;
            }
            else if (currentScale < 1f)
            {
                currentScale += currentScaleRate * 0.25f;
            }

            if (currentScale > maxScale)
                currentScale = maxScale;
            if (currentScale < minScale)
                currentScale = minScale;

            // update transform scale
            transform.localScale = new Vector3(1f, currentScale, 1f);
            scaleText.text = "current scale: " + currentScale;
        }

        // update cameras
        if (changeFOV)
        {
            float scale01 = Mathf.InverseLerp(maxScale, minScale, currentScale);
            float newFOV = Mathf.Lerp(maxFOV, minFOV, scale01);
            Camera.main.fieldOfView = newFOV;
        }
    }
}
