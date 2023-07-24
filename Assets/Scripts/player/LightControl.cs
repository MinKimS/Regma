using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D lightComponent;

    //private void Start()
    //{
    //    lightComponent = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    //}

    //private void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        lightComponent.enabled = !lightComponent.enabled;
    //    }
    //}

    public GameObject LightObj;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {

            if (LightObj.activeSelf)
            {
                LightObj.SetActive(false);
            }

            else
            {
                LightObj.SetActive(true);
            }
        }
    } // Start is called before the first frame update
    
}
