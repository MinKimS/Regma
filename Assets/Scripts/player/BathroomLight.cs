using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomLight : MonoBehaviour
{
    public GameObject flickeringLight;
    public float minTime;
    public float maxTime;
    private float timer;
    private bool isLightActive = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        LightFlickering();
    }

    void LightFlickering()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            isLightActive = !isLightActive;
            flickeringLight.SetActive(isLightActive);
            timer = Random.Range(minTime, maxTime);
        }
    }
}
