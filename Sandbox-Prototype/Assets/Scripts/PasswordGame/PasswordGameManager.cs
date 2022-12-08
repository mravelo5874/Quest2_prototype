using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordGameManager : MonoBehaviour
{
    public static PasswordGameManager instance;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public TextMeshProUGUI numberPanelText;
    public Collider portalCollider;
    public MeshRenderer portalRenderer;

    public int passwordLength = 6;
    public float timeBetweenChars = 0.01f;

    private List<char> currentPassword;
    private int currentPasswordIndex = 0;
    private string passwordString = "";
    private float timer = 0f;

    void Start()
    {
        // play song 3
        float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
        AudioManager.instance.PlaySound(
                AudioManager.instance.database.song_3, 
                0.2f,
                true, 
                0.5f, 
                "song3"
            );

        // portal is closed until player gets the password correct
        portalCollider.enabled = false;
        portalRenderer.enabled = false;
        
        // generate new random password
        currentPassword = new List<char>();
        passwordString = "";
        for (int i = 0; i < passwordLength; i++)
        {
            // alternate between numbers and chars
            char randomVar = '.';
            if (i % 2 == 0)
            {
                int randomInt = Random.Range(0, 10);
                randomVar = randomInt.ToString()[0];
            }
            else
            {
                char randomChar = (char)Random.Range('A', 'J');
                randomVar = randomChar.ToString().ToUpper()[0];
            }
            currentPassword.Add(randomVar);
            passwordString += randomVar;
            print (i + " " + randomVar);
        }

        print ("current password: " + passwordString);
        currentPasswordIndex = 0;
    }

    void Update()
    {
        // set current time between chars
        float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
        timer += Time.deltaTime * currentTimeScale;

        // switch char
        if (timer >= timeBetweenChars)
        {
            timer = 0f;
            if (currentPasswordIndex == passwordLength)
            {
                numberPanelText.text = "";
            }
            else
            {
                numberPanelText.text = currentPassword[currentPasswordIndex].ToString();
            }
            currentPasswordIndex++;
            if (currentPasswordIndex > passwordLength)
            {
                currentPasswordIndex = 0;
            }
        }
    }

    public string GetPasswordString()
    {
        return passwordString;
    }
}
