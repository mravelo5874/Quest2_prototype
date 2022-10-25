using UnityEngine;
using TMPro;

public class ObjectScaleController : MonoBehaviour
{
    public static ObjectScaleController instance;

    float counter;
    float scaleRate = 1.0f;

    void Awake()
    {
        counter = 1.0f;
        // set instance
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        bool growInput = false;
        bool shrinkInput = false;

        if (GameManager.instance.playMode == GameManager.PlayMode.KeyboardAndMouse)
        {            
            // left shift for growing
            growInput = Input.GetMouseButton(0);
            shrinkInput = Input.GetMouseButton(1);
        }

        if (growInput ^ shrinkInput) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100)) {
                if (growInput) {
                    hit.transform.localScale = hit.transform.localScale + Time.deltaTime * scaleRate * new Vector3(1,1,1);
                }
                else if (shrinkInput) {
                    hit.transform.localScale = hit.transform.localScale - Time.deltaTime * scaleRate * new Vector3(1,1,1);
                }
            }
        }
    }
}
