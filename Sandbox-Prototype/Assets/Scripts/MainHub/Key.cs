using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public MyObject myObject;

    private bool isActivated = false;
    public bool GetIsActivated()
    {
        return isActivated;
    }

    void Start()
    {
        isActivated = false;
    }

    void Update()
    {
        OVRInput.Update();
        
        // get right controller trigger input
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            // send raycast from right controller
            RaycastHit hit;
            Ray rightRay = new Ray(RightHandController.instance.transform.position, RightHandController.instance.transform.forward);
            if (Physics.Raycast(rightRay, out hit))
            {
                if (hit.collider.tag == "Key")
                {
                    ActivateKey();
                }
            }
        }
        // gte left controller trigger input
        else if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            // send raycast from left controller
            RaycastHit hit;
            Ray leftRay = new Ray(LeftHandController.instance.transform.position, LeftHandController.instance.transform.forward);
            if (Physics.Raycast(leftRay, out hit))
            {
                if (hit.collider.tag == "Key")
                {
                    ActivateKey();
                }
            }
        }
    }

    private void ActivateKey()
    {
        if (!isActivated)
        {
            isActivated = true;
            myObject.SquishyChangeScale(400f, 0f, 0.2f, 0.2f);

            // play sound fx
            float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
            AudioManager.instance.PlaySound(
                    AudioManager.instance.database.sparkle, 
                    0.25f,
                    false, 
                    Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                    "key1"
                );
            AudioManager.instance.PlaySound(
                    AudioManager.instance.database.trumpet, 
                    0.25f,
                    false, 
                    Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                    "key2"
                );
        }
    }
}
