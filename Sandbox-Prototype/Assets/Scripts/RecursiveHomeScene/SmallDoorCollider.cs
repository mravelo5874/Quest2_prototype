using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDoorCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            print ("player enter small doorway!");
        }
    }
}
