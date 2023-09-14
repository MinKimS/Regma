using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomLight : MonoBehaviour
{
    public GameObject flickeringLight; // 조명 오브젝트 연결
    //깜빡이는 간격 시간 조정
    public float minTime;
    public float maxTime;
    private float timer;
    private bool isLightActive = false; // 조명 활성화

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
        if (timer > 0) // 아직 다음 깜빡임 간격이 남아 있는 경우
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
