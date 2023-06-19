using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;
    private void Awake() {
        if(instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else { Destroy(gameObject); }
    }

    // 카메라 영역 제한
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
    public void LimitArea(Transform tr)
    {
        float possibleX = mapSize.x - camWidth;
        float clampX =  Mathf.Clamp(tr.position.x, center.x-possibleX, center.x+possibleX);

        float possibleY = mapSize.y - camHeight;
        float clampY = Mathf.Clamp(tr.position.y, center.y-possibleY, center.y+possibleY);

        tr.position = new Vector3(clampX, clampY, -10f);
    }

    //카메라가 움직일 수 있는 영역 표시
    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(center, mapSize*2);
    }
}
