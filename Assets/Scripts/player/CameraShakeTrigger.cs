using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject canvasBook;

    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero; // 방향
    [SerializeField] GameObject shake;

    Quaternion m_originRot; // 초기값

    GameObject camObj;
    Collider2D collidingObject;
    private bool isActive = true;
    void Start()
    {
        gameObject.SetActive(isActive);
        // Get the CameraController component attached to the camera
        cameraController = Camera.main.GetComponent<CameraController>();
        camObj = cameraController.gameObject;

        m_originRot = camObj.transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && collidingObject != null && collidingObject.CompareTag("shake"))
        {
            canvasBook.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(Reset());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("shake"))
        {
            collidingObject = collision; // 충돌한 오브젝트 정보 저장
            StartCoroutine(ShakeCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("shake"))
        {
            collidingObject = null; // 충돌한 오브젝트 정보 초기화
            isActive = false;
            collision.gameObject.SetActive(isActive);
        }
    }

    IEnumerator ShakeCoroutine()
    {
        Vector3 t_originEuler = camObj.transform.eulerAngles;
        while (true)
        {
            float t_rotX = Random.Range(-m_offset.x, m_offset.x);
            float t_rotY = Random.Range(-m_offset.y, m_offset.y);
            float t_rotZ = Random.Range(-m_offset.z, m_offset.z);
            Vector3 t_randomRot = t_originEuler + new Vector3(t_rotX, t_rotY, t_rotZ);
            Quaternion t_rot = Quaternion.Euler(t_randomRot);

            while (Quaternion.Angle(camObj.transform.rotation, t_rot) > 0.1f)
            {
                camObj.transform.rotation = Quaternion.RotateTowards(camObj.transform.rotation, t_rot, m_force * Time.deltaTime);
                yield return null;
            }

            yield return null;
        }
    }

    IEnumerator Reset()
    {
        while (Quaternion.Angle(camObj.transform.rotation, m_originRot) > 0.1f)
        {
            camObj.transform.rotation = Quaternion.RotateTowards(camObj.transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;
        }

        // 정확한 리셋을 위해 강제로 초기 회전값으로 설정
        camObj.transform.rotation = m_originRot;
    }
}
