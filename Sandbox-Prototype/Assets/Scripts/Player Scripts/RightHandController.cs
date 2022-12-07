using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RightHandController : MonoBehaviour
{
    public static RightHandController instance;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public LineRenderer rightHandLine;
    public XRInteractorLineVisual XR_right_line;

    void Update()
    {
        OVRInput.Update();
        
        // get right controller trigger input
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Gradient color = GameManager.instance.triggerLineGrad;
            XR_right_line.invalidColorGradient = color;            
        }
        else
        {
            Gradient color = GameManager.instance.defaultLineGrad;
            XR_right_line.invalidColorGradient = color;
        }
    }
}
