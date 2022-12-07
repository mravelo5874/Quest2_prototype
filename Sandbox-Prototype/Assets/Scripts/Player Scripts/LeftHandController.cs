using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeftHandController : MonoBehaviour
{
    public static LeftHandController instance;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public LineRenderer leftHandLine;
    public XRInteractorLineVisual XR_left_line;

    void Update()
    {
        OVRInput.Update();
        
        // get left controller trigger input
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Gradient color = GameManager.instance.triggerLineGrad;
            XR_left_line.invalidColorGradient = color;
        }
        else
        {
            Gradient color = GameManager.instance.defaultLineGrad;
            XR_left_line.invalidColorGradient = color;
        }
    }
}
