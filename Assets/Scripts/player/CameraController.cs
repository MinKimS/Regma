using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 fixedPosition = new Vector3(-5.9f, -1.7f, -10f);

    CameraManager cmManager;

    private void Start()
    {
        cmManager = GetComponent<CameraManager>();
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + fixedPosition;
        transform.position = new Vector3(desiredPosition.x, desiredPosition.y, fixedPosition.z);

        cmManager.LimitArea(transform);//카메라 영역 제한
    }

    //private void Sizecontrol()
    //{


    //}


}
