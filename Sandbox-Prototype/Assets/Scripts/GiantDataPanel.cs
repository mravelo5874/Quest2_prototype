using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiantDataPanel : MonoBehaviour
{
    public static GiantDataPanel instance;

    public TextMeshProUGUI panelText;

    void Update()
    {
        string panelString = "";
        panelString += "player data:\n\n";
        panelString += "pos: " + PlayerPositionController.instance.transform.position + "\n";
        panelString += "scale: x " + PlayerScaleController.instance.GetCurrentScale() + "\n";
        panelString += "time scale: x " + TimePerceptionController.instance.GetGameTimeScale() + "\n";


        float elapsedTime = ClockController.instance.GetElapsedTime();
        float mins = (elapsedTime / 60f);
        if (mins >= 1f)
        {
            float seconds = elapsedTime - ((int)mins * 60f);
            panelString += "elapsed time: " + (int)mins + " mins " + (int)seconds + " sec";
        }
        else
        {
            panelString += "elapsed time: " + (int)elapsedTime + " sec";
        }

        
        panelText.text = panelString;
    }
}
