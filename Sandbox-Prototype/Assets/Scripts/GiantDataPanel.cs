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
            panelString += "elapsed time: " + (int)mins + " mins " + (int)seconds + " sec\n";
        }
        else
        {
            panelString += "elapsed time: " + (int)elapsedTime + " sec\n";
        }
        
        panelString += TriggerDebug();
        panelText.text = panelString;
    }
    
    
    string TriggerDebug()
    {
    	var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
    	
		UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
		
		if(leftHandDevices.Count == 1)
		{
			UnityEngine.XR.InputDevice device = leftHandDevices[0];
			Debug.Log(string.Format("Device name '{0}' with role '{1}'", device.name, device.characteristics.ToString()));
			bool triggerValue;
			if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
			{
				Debug.Log("Trigger button is pressed.");
				return "Trigger button on left hand is pressed.\n";
			}
		} else {
			Debug.Log("more than one left hand devices found");
		}
		
		return "";
    }
}
