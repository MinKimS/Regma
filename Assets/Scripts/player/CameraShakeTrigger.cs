using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject canvasBook;

    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero;

    Quaternion m_originRot;

    GameObject camObj;

    void Start()
    {
        m_originRot = transform.rotation;

        // Get the CameraController component attached to the camera
        cameraController = Camera.main.GetComponent<CameraController>();
        camObj = cameraController.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("shake"))
        {
            StartCoroutine(ShakeCoroutine());
            print("dd");
        }
    }

    IEnumerator ShakeCoroutine()
    {
        Vector3 t_originEuler = camObj.transform.eulerAngles;
        while (true)
        {
            if (!canvasBook.activeSelf)
            {
                canvasBook.SetActive(true);
                StartCoroutine(Reset());
            }

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
        while (Quaternion.Angle(camObj.transform.rotation, m_originRot) > 0f)
        {
            camObj.transform.rotation = Quaternion.RotateTowards(camObj.transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;
        }
    }
}
