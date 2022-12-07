using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    public TextMeshProUGUI keypadPanel;
    public float inputDelay = 0.1f;
    public MyObject doorObject;

    private string currentText = "";
    private bool canInput = true;
    private bool winGame = false;

    void Start()
    {
        keypadPanel.text = currentText;
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
                if (hit.collider.tag == "KeypadButton")
                {
                    int id = hit.transform.GetComponent<KeypadButton>().id;
                    hit.transform.GetComponent<KeypadButton>().myObject.SquishyChangeScale(0.6f, .7f, 0.1f, 0.1f);
                    OnButtonPressed(id);
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
                if (hit.collider.tag == "KeypadButton")
                {
                    int id = hit.transform.GetComponent<KeypadButton>().id;
                    hit.transform.GetComponent<KeypadButton>().myObject.SquishyChangeScale(0.6f, .7f, 0.1f, 0.1f);
                    OnButtonPressed(id);
                }
            }
        }
    }

    public void OnEnterPressed()
    {
        // check if win game
        if (PasswordGameManager.instance.GetPasswordString() == currentText)
        {
            if (!winGame)
            {
                winGame = true;
                // play sound fx
                float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
                AudioManager.instance.PlaySound(
                        AudioManager.instance.database.win_tune, 
                        0.25f, 
                        false, 
                        Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                        "clock_tick"
                    );

                doorObject.ChangeScale(0f, 2f);
            }
        }
    }

    public void OnClearPressed()
    {
        currentText = "";
        keypadPanel.text = currentText;

        // play sound fx
        float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
        AudioManager.instance.PlaySound(
                AudioManager.instance.database.sad_blip, 
                0.1f, 
                false, 
                Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                "clock_tick"
            );
    }

    private IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(inputDelay);
        canInput = true;
    }

    private void OnButtonPressed(int index)
    {
        if (canInput)
        {
            canInput = false;
            StartCoroutine(InputDelay());
        }
        else
        {
            return;
        }

        // play button blip sound
        float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
        AudioManager.instance.PlaySound(
                AudioManager.instance.database.wood_blip, 
                0.1f, 
                false, 
                Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                "clock_tick"
            );

        string newChar = "";
        switch (index)
        {
            default:
            case 0:
                newChar = "0";
                break;
            case 1:
                newChar = "1";
                break;
            case 2:
                newChar = "2";
                break;
            case 3:
                newChar = "3";
                break;
            case 4:
                newChar = "4";
                break;
            case 5:
                newChar = "5";
                break;
            case 6:
                newChar = "6";
                break;
            case 7:
                newChar = "7";
                break;
            case 8:
                newChar = "8";
                break;
            case 9:
                newChar = "9";
                break;
            case 10:
                newChar = "A";
                break;
            case 11:
                newChar = "B";
                break;
            case 12:
                newChar = "C";
                break;
            case 13:
                newChar = "D";
                break;
            case 14:
                newChar = "E";
                break;
            case 15:
                newChar = "F";
                break;
            case 16:
                newChar = "G";
                break;
            case 17:
                newChar = "H";
                break;
            case 18:
                newChar = "I";
                break;
            case 19:
                newChar = "J";
                break;
            case 20:
                OnEnterPressed();
                return;
            case 21:
                OnClearPressed();
                return;
        }

        if (currentText.Length >= PasswordGameManager.instance.passwordLength)
        {
            return;
        }
        else
        {
            currentText += newChar;
            keypadPanel.text = currentText;
        }
    }
}
