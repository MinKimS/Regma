using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 fixedPosition = new Vector3(-5.9f, -1.7f, -10f);

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + fixedPosition;
        transform.position = new Vector3(desiredPosition.x, desiredPosition.y, fixedPosition.z);
        LimitArea();//카메라 영역 제한
    }

    //-------------카메라 영역 제한

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    float camWidth;
    float camHeight;

    private void Start() {
        camHeight = Camera.main.orthographicSize;
        camWidth = (camHeight * Screen.width) / Screen.height;
    }

    //정해진 영역에서만 카메라 이동
    public void LimitArea()
    {
        float possibleX = mapSize.x - camWidth;
        float clampX =  Mathf.Clamp(transform.position.x, center.x-possibleX, center.x+possibleX);

        float possibleY = mapSize.y - camHeight;
        float clampY = Mathf.Clamp(transform.position.y, center.y-possibleY, center.y+possibleY);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    //카메라가 움직일 수 있는 영역 표시
    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(center, mapSize*2);
    }
}
