using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public Dialogue dlg;

    bool isSee;
    Vector3 origin;

    InteractionObjData interactionObjData;
    SpriteRenderer sr;
    public Sprite[] sp;
    [HideInInspector] public bool isOnlyChild = false;
    GameEventListener gameEventListener;
    [HideInInspector] public bool isOkErase = false;

    private void Awake()
    {
        interactionObjData = GetComponent<InteractionObjData>();
        sr = GetComponent<SpriteRenderer>();
        gameEventListener = GetComponent<GameEventListener>();
    }

    public void FirstSee()
    {
        CameraController cam = Camera.main.GetComponent<CameraController>();
        cam.target = transform;
        origin = cam.fixedPosition;
        cam.fixedPosition = Vector3.zero + (Vector3.back*10);
        Camera.main.orthographicSize -= 1f;

        DialogueManager.instance.PlayDlg(dlg);
        isSee = true;
    }

    private void Update()
    {
        if(isSee && DialogueManager.instance._dlgState == DialogueManager.DlgState.End)
        {
            CameraController cam = Camera.main.GetComponent<CameraController>();
            cam.target = PlayerInfoData.instance.playerTr;
            cam.fixedPosition = origin;
            Camera.main.orthographicSize += 1f;
            isSee = false;
        }
    }

    public void LookClose()
    {
        StartCoroutine(CameraClose());
    }

    IEnumerator CameraClose()
    {
        CameraController cam = Camera.main.GetComponent<CameraController>();
        cam.target = transform;
        origin = cam.fixedPosition;
        cam.fixedPosition = Vector3.zero + (Vector3.back * 10);
        Camera.main.orthographicSize -= 1f;

        yield return new WaitForSeconds(2f);

        cam.target = PlayerInfoData.instance.playerTr;
        cam.fixedPosition = origin;
        Camera.main.orthographicSize += 1f;

        interactionObjData.GmEventIdx++;
    }

    public void EraseFrame()
    {
        if (isOkErase)
        {
            if (isOnlyChild)
            {
                sr.sprite = sp[0];
            }
            else
            {
                sr.sprite = sp[1];
            }

            VibrationHouse();
        }
    }

    void VibrationHouse()
    {
        print("vibration");
        DialogueManager.instance.PlayDlg();
    }

    public void ActiveEvent()
    {
        gameEventListener.enabled = true;
    }
}
