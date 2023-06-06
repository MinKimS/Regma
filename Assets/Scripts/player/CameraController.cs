using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 fixedPosition = new Vector3(-5.9f, -1.7f, -10f);

    private bool isShaking = false; // ��鸲 ���� Ȯ��
    private float shakeDuration = 5f; // ��鸲 ���� �ð�
    private float shakeMagnitude = 60f; // ��鸲�� ����
    private float shakeTimer = 0f; // ��鸲 Ÿ�̸�

    private Vector3 originalPosition; // �ʱ� ī�޶� ��ġ

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
                // ��鸲 ȿ�� ���
                float shakeX = Random.Range(-1f, 1f) * shakeMagnitude;
                float shakeY = Random.Range(-1f, 1f) * shakeMagnitude;

                Vector3 shakeOffset = new Vector3(shakeX, shakeY, 0f);
                transform.position = originalPosition + shakeOffset;

                shakeTimer += Time.deltaTime;
            }
            else
            {
                // ��鸲 ����
                isShaking = false;
                transform.position = originalPosition;
            }
        }
        else
        {
            // �Ϲ����� ī�޶� �̵�
            transform.position = target.position + fixedPosition;
        }
    }

    public void StartCameraShake()
    {
        shakeTimer = 0f; // ��鸲 Ÿ�̸� �ʱ�ȭ
        isShaking = true;
        originalPosition = transform.position; // ���� ��ġ�� �ʱ� ��ġ�� ����
    }
}
