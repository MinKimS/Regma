using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D lightComponent;

    [HideInInspector]
    public bool isLightOn = true;

    private void Start()
    {
        isLightOn = true;
    }

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
                isLightOn = false;
            }

            else
            {
                LightObj.SetActive(true);

                isLightOn = true;
            }
            AudioManager.instance.SFXPlay("Game Sound_headlight on-off");
        }
    } // Start is called before the first frame update
    
}
