using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlickLight : MonoBehaviour
{
    Light2D lampLight;
    public float lightRetentionTime = 2f;
    float originLightIntensity;

    public bool isKitchenLight = false;

    public BathroomLight bathroomLight;
    
    private void Awake()
    {
        lampLight = GetComponent<Light2D>();
        originLightIntensity = lampLight.intensity;
    }

    private void Start()
    {
        if (isKitchenLight)
            StartCoroutine(BlinkLight());
    }
    IEnumerator BlinkLight()
    {
        WaitForSeconds wait = new WaitForSeconds(lightRetentionTime);
        while(true)
        {
            while (lampLight.intensity > 0)
            {
                lampLight.intensity -= 0.01f;
                yield return null;
            }

            yield return wait;

            while (lampLight.intensity < originLightIntensity)
            {
                lampLight.intensity += 0.01f;
                yield return null;
            }
            yield return wait;
        }
    }

    private void Update()
    {
        if (!isKitchenLight)
        {
            if (bathroomLight.isLightActive)
            {
                lampLight.intensity = originLightIntensity;
            }
            else
            {
                lampLight.intensity = 0;
            }
        }
    }
}
