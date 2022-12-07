using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerHuntManager : MonoBehaviour
{
    public List<ScavengerHuntCoin> allGameCoins;
    public Collider portalCollider;
    public MeshRenderer portalRenderer;
    private bool winGame = false;

    void Start()
    {
        // portal is closed until player finds all the coins
        portalCollider.enabled = false;
        portalRenderer.enabled = false;
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
                if (hit.collider.tag == "Coin")
                {
                    hit.transform.GetComponent<ScavengerHuntCoin>().ActivateCoin();
                    CheckIfAllCoinsActivated();
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
                if (hit.collider.tag == "Coin")
                {
                    hit.transform.GetComponent<ScavengerHuntCoin>().ActivateCoin();
                    CheckIfAllCoinsActivated();
                }
            }
        }
    }

    private void CheckIfAllCoinsActivated()
    {
        // return if already won game
        if (winGame)
        {
            return;
        }
        // check each coin to see if it is activated
        foreach(ScavengerHuntCoin coin in allGameCoins)
        {
            if (!coin.GetIsActivated())
            {
                return;
            }
        }

        winGame = true;
        portalCollider.enabled = true;
        portalRenderer.enabled = true;
        // play sound fx
        float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
        AudioManager.instance.PlaySound(
                AudioManager.instance.database.win_tune, 
                0.5f, 
                false, 
                Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                "clock_tick"
            );
    }
}
