using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeDoorCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            print ("player enter large doorway!");
        }
    }
}
