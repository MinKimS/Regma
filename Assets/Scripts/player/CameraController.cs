using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 fixedPosition = new Vector3(-5.9f, -1.7f, -10f);

    private bool isShaking = false; // 흔들림 상태 확인
    private float shakeDuration = 5f; // 흔들림 지속 시간
    private float shakeMagnitude = 60f; // 흔들림의 세기
    private float shakeTimer = 0f; // 흔들림 타이머

    private Vector3 originalPosition; // 초기 카메라 위치

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (isShaking)
        {
            if (shakeTimer < shakeDuration)
            {
                // 흔들림 효과 계산
                float shakeX = Random.Range(-1f, 1f) * shakeMagnitude;
                float shakeY = Random.Range(-1f, 1f) * shakeMagnitude;

                Vector3 shakeOffset = new Vector3(shakeX, shakeY, 0f);
                transform.position = originalPosition + shakeOffset;

                shakeTimer += Time.deltaTime;
            }
            else
            {
                // 흔들림 종료
                isShaking = false;
                transform.position = originalPosition;
            }
        }
        else
        {
            // 일반적인 카메라 이동
            transform.position = target.position + fixedPosition;
        }
    }

    public void StartCameraShake()
    {
        shakeTimer = 0f; // 흔들림 타이머 초기화
        isShaking = true;
        originalPosition = transform.position; // 현재 위치를 초기 위치로 설정
    }
}
