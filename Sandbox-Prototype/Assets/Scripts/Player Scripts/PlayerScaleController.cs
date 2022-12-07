using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScaleController : MonoBehaviour
{
    public static PlayerScaleController instance;

    [Header("Scale Options")]
    public float startScale = 1f;
    public float maxScale;
    public float minScale;
    public float scaleRate;
    public float timeBetweenScaleSoundFX = 3f;
    // private scale option variables
    private float currentScale = 1f;
    public float GetCurrentScale() { return currentScale; } // public getter

    [Header("Camera Options")]
    public bool changeFOV;
    public float minFOV;
    public float maxFOV;
    // private camera option variables
    private float currentFOV;

    public enum ScaleChangeMode
    {
        None, 
        Growing,
        Shrinking
    }
    private ScaleChangeMode scaleMode = ScaleChangeMode.None;

    void Awake()
    {
        // set instance
        if (instance == null)
        {
            instance = this;
        }
        // set staring scale
        currentScale = startScale;
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    private IEnumerator EndScaleFX(float delay)
    {
        yield return new WaitForSeconds(delay);
        scaleMode = ScaleChangeMode.None;
    }

    void FixedUpdate()
    {
        bool growInput = false;
        bool shrinkInput = false;

        // use keyboard controls if camera control mode is mouse
        if (GameManager.instance.playMode == GameManager.PlayMode.KeyboardAndMouse)
        {            
            // left shift for growing
            growInput = Input.GetKey(KeyCode.LeftShift);
            shrinkInput = Input.GetKey(KeyCode.LeftControl);

            // change scale mode and play FX
            if (growInput && scaleMode != ScaleChangeMode.Growing)
            {
                print ("grow!");
                scaleMode = ScaleChangeMode.Growing;
                AudioManager.instance.StopSound("change_scale_FX");
                AudioManager.instance.PlaySound(AudioManager.instance.database.grow_fx, 0.25f, false, 1f, "change_scale_FX");
                StartCoroutine(EndScaleFX(timeBetweenScaleSoundFX));
            }
            else if (shrinkInput && scaleMode != ScaleChangeMode.Shrinking)
            {
                print ("shrink!");
                scaleMode = ScaleChangeMode.Shrinking;
                AudioManager.instance.StopSound("change_scale_FX");
                AudioManager.instance.PlaySound(AudioManager.instance.database.shrink_fx, 0.25f, false, 1f, "change_scale_FX");
                StartCoroutine(EndScaleFX(timeBetweenScaleSoundFX));
            }
        }
        else if (GameManager.instance.playMode == GameManager.PlayMode.VRHeadset)
        {
            OVRInput.FixedUpdate();
            growInput = OVRInput.Get(OVRInput.Button.Two);
            shrinkInput = OVRInput.Get(OVRInput.Button.One);

            // change scale mode and play FX
            if (growInput && scaleMode != ScaleChangeMode.Growing)
            {
                print ("grow!");
                scaleMode = ScaleChangeMode.Growing;
                AudioManager.instance.StopSound("change_scale_FX");
                AudioManager.instance.PlaySound(AudioManager.instance.database.grow_fx, 0.5f, false, 1f, "change_scale_FX");
                StartCoroutine(EndScaleFX(timeBetweenScaleSoundFX));
            }
            else if (shrinkInput && scaleMode != ScaleChangeMode.Shrinking)
            {
                print ("shrink!");
                scaleMode = ScaleChangeMode.Shrinking;
                AudioManager.instance.StopSound("change_scale_FX");
                AudioManager.instance.PlaySound(AudioManager.instance.database.shrink_fx, 0.5f, false, 1f, "change_scale_FX");
                StartCoroutine(EndScaleFX(timeBetweenScaleSoundFX));
            }
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
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
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
