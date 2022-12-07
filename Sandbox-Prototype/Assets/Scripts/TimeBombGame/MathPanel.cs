using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathPanel : MonoBehaviour
{
    public TextMeshProUGUI a_text;
    public TextMeshProUGUI b_text;
    public TextMeshProUGUI c_text;
    public TextMeshProUGUI d_text;
    public TextMeshProUGUI op1_text;
    public TextMeshProUGUI op2_text;
    public TextMeshProUGUI op3_text;


    public void SetTextValues(int a, int b, int c, int d, 
        TimeBombGameManager.Operation op1,
        TimeBombGameManager.Operation op2,
        TimeBombGameManager.Operation op3)
    {
        a_text.text = "(" + a.ToString() + ")";
        b_text.text = "(" + b.ToString() + ")";
        c_text.text = "(" + c.ToString() + ")";
        d_text.text = "(" + d.ToString() + ")";
        op1_text.text = OperationToString(op1);
        op2_text.text = OperationToString(op2);
        op3_text.text = OperationToString(op3);
    }

    private string OperationToString(TimeBombGameManager.Operation op)
    {
        switch (op)
        {
            default:
            case TimeBombGameManager.Operation.ADD:
                return "+";
            case TimeBombGameManager.Operation.SUB:
                return "-";
            case TimeBombGameManager.Operation.MULT:
                return "*";
        }
    }
}
