using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorway : MonoBehaviour
{
    public string targetSceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.GoToScene(targetSceneName);
        }
    }
}
