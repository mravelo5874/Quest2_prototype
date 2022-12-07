using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombKeypad : MonoBehaviour
{
    public TextMeshProUGUI numberPanel;
    public TextMeshProUGUI negativePanel;
    public float inputDelay = 0.25f;

    private string currentText = "";
    private bool isNeg = false;
    private bool canInput = true;
    private bool winGame = false;


    void Start()
    {
        numberPanel.text = currentText;
        negativePanel.text = "";
        isNeg = false;
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
        // get left controller trigger input
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
        //check if win game
        int num = int.Parse(currentText);
        if (isNeg)
        {
            num *= -1;
        }

        if (num == TimeBombGameManager.instance.BOMB_PASSCODE)
        {
            if (!winGame)
            {
                winGame = true;
                // play sound fx
                float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
                AudioManager.instance.PlaySound(
                        AudioManager.instance.database.win_tune, 
                        0.5f, 
                        false, 
                        Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                        "clock_tick"
                    );

                TimeBombGameManager.instance.stopTimer = true;
                TimeBombGameManager.instance.clock.isOn = false;
                StartCoroutine(RestartLevelDelay(5f));
            }
        }
        else
        {
            OnClearPressed();
            // play sound fx
            float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
            AudioManager.instance.PlaySound(
                    AudioManager.instance.database.sad_blip, 
                    0.5f, 
                    false, 
                    Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale), 
                    "clock_tick"
                );
        }
    }


    private IEnumerator RestartLevelDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.GoToScene("MainHubScene");
    }

    public void OnClearPressed()
    {
        currentText = "";
        numberPanel.text = currentText;
        negativePanel.text = "";
        isNeg = false;

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
                if (isNeg)
                {
                    isNeg = false;
                    negativePanel.text = "";
                }
                else if (!isNeg)
                {
                    isNeg = true;
                    negativePanel.text = "-";
                }
                break;
            case 11:
                int randomNum = Random.Range(0, 99999);
                int neg = Random.Range(0, 2);
                currentText = randomNum.ToString();
                numberPanel.text = currentText;
                if (neg == 0)
                {
                    isNeg = false;
                    negativePanel.text = "";
                }
                else if (neg == 1)
                {
                    isNeg = true;
                    negativePanel.text = "-";
                }
                return;
            case 12:
                OnEnterPressed();
                return;
            case 13:
                OnClearPressed();
                return;
        }

        if (currentText.Length >= 8)
        {
            return;
        }
        else
        {
            currentText += newChar;
            numberPanel.text = currentText;
        }
    }
}
