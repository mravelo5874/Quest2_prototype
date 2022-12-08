using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class HandDisplayConsole : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _hand;

    public GameObject cubeObj;
    void Start()
    {
        cubeObj.transform.localScale = new Vector3(1,1,1);
        cubeObj.transform.parent = _hand.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //cubeObj.transform.position = _hand.transform.position;
        Debug.Log("cube pos: " + cubeObj.transform.position.x +", "+ cubeObj.transform.position.y+", "+ cubeObj.transform.position.z);
    }
}
