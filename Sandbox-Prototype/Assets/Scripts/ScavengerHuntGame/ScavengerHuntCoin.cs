using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerHuntCoin : MonoBehaviour
{
    public MyObject myObject;
    public MeshRenderer meshRenderer;
    public Material notActivatedMat;
    public Material ActivatedMat;

    private bool isActivated = false;
    public bool GetIsActivated()
    {
        return isActivated;
    }

    void Start()
    {
        isActivated = false;
        meshRenderer.material = notActivatedMat;
    }

    public void ActivateCoin()
    {
        if (!isActivated)
        {
            isActivated = true;
            meshRenderer.material = ActivatedMat;
            myObject.SquishyChangeScale(200f, 0f, 0.2f, 0.2f);

            // play sound fx
            float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
            AudioManager.instance.PlaySound(
                    AudioManager.instance.database.sparkle, 
                    0.25f,
                    false, 
                    Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                    "clock_tick"
                );
        }
    }
}
