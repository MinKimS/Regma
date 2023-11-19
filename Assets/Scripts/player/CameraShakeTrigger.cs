using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject canvasBook;

    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero; 
    [SerializeField] GameObject shake;

    Quaternion m_originRot; 

    GameObject camObj;
    Collider2D collidingObject;
    private bool isActive = true;

    public Dialogue[] dlg;
    public Fade fade;

    void Start()
    {
        gameObject.SetActive(isActive);
        // Get the CameraController component attached to the camera
        cameraController = Camera.main.GetComponent<CameraController>();
        camObj = cameraController.gameObject;

        m_originRot = camObj.transform.rotation;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E) && collidingObject != null && collidingObject.CompareTag("shake") && SmartphoneManager.instance.diary.isAfterDlg) // 꽃 관련 일기장을 읽고 나서 흔들리기
    //    {
    //        canvasBook.SetActive(true);
    //        StopAllCoroutines();
    //        StartCoroutine(Reset());

    //        collidingObject = null;
    //        isActive = false;
    //        //collidingObject.CompareTag("shake").SetActive(isActive);
    //    }
    //}

    bool isShaking = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("shake") && SmartphoneManager.instance.diary.isAfterDlg)
        {
            print("충돌");
            collidingObject = collision; 
            StartCoroutine(ShakeCoroutine());

            isActive = false;
            collision.gameObject.SetActive(isActive);

            if(!isShaking)
            {
                isShaking = true;
                StartCoroutine(IEShakingEvent());
            }
        }

        //collidingObject = null; 
        //isActive = false;
        //collidingObject.CompareTag("shake").SetActive(isActive);
    }

    //추가된 대사 실행
    IEnumerator IEShakingEvent()
    {
        DialogueManager.instance.PlayDlg(dlg[0]);

        yield return new WaitUntil(() => DialogueManager.instance._dlgState == DialogueManager.DlgState.End);

        fade.SetBlack();

        DialogueManager.instance.PlayDlg(dlg[1]);

        yield return new WaitUntil(() => DialogueManager.instance._dlgState == DialogueManager.DlgState.End);

        //흔들림 멈춤
        fade.SetVisible();
        canvasBook.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Reset());

        collidingObject = null;
        isActive = false;

        DialogueManager.instance.PlayDlg(dlg[2]);
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("shake"))
    //    {
    //        collidingObject = null; 
    //        isActive = false;
    //        collision.gameObject.SetActive(isActive);
    //    }
    //}

    IEnumerator ShakeCoroutine()
    {
        Vector3 t_originEuler = camObj.transform.eulerAngles;
        while (true)
        {
            float t_rotX = Random.Range(-m_offset.x, m_offset.x);
            float t_rotY = Random.Range(-m_offset.y, m_offset.y);
            float t_rotZ = Random.Range(-m_offset.z, m_offset.z);
            Vector3 t_randomRot = t_originEuler + new Vector3(t_rotX, t_rotY, t_rotZ);
            //Vector3 t_randomRot = new Vector3(t_rotX, t_rotY, t_rotZ);
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

        
        camObj.transform.rotation = m_originRot;

        if (collidingObject != null && collidingObject.CompareTag("shake"))
        {
            print("충돌");
            collidingObject.gameObject.SetActive(false);
            collidingObject = null;
            isActive = false;
        }
    }
}