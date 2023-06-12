using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D houseLight;
    private float onLight;
    private float offLight = 0f;

    // //빛이 깜빡이는 속도
    // public float flashSpeed = 1f;
    //빛의 밝기가 변하는 시간
    public float chgTime = 0.5f;

    private void Start() {
        onLight = houseLight.intensity;
        SetLightIntensity(false);    
    }

    //방 밝기 설정
    public void SetLightIntensity(bool isOn)
    {
        if(isOn)
        {
            houseLight.intensity = onLight;
        }
        else
        {
            houseLight.intensity = offLight;
        }
    }

    public void SetLightChgGradually(bool isOn)
    {
        if(isOn)
        {
            StartCoroutine(LightChg(onLight));
        }
        else
        {
            StartCoroutine(LightChg(offLight));
        }
    }

    IEnumerator LightChg(float targetIntensity)
    {
        float curIntensity = houseLight.intensity;
        float elapsedTime = 0f;

        while(elapsedTime < chgTime)
        {
            float t = elapsedTime / chgTime;
            houseLight.intensity = Mathf.Lerp(curIntensity, targetIntensity, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        houseLight.intensity = targetIntensity;
    }

    // public void SetLightBlinkAndTurnOn(float goalBrightness)
    // {
    //     float curBrightness = houseLight.intensity;

    //     StartCoroutine(FlashLight(curBrightness, goalBrightness));
    // }

    // IEnumerator FlashLight(float curB, float goal)
    // {
    //     curB = Mathf.Lerp(curB, goal, flashSpeed * Time.deltaTime);

    //     yield return null;
    // }
}
