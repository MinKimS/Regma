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

    public Dialogue needToCloseDlg;

    public BoxCollider2D[] bc;
    public GameEvent[] selectionEvents;

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
            if (CheckDistanceToPlayer())
            {
                SmartphoneManager.instance.DeleteSelectItem();
                if (isOnlyChild)
                {
                    sr.sprite = sp[0];
                    selectionEvents[0].Raise();
                }
                else
                {
                    sr.sprite = sp[1];
                    selectionEvents[1].Raise();
                }

                VibrationHouse();
            }
            else
            {
                DialogueManager.instance.PlayDlg(needToCloseDlg);
            }
        }
    }

    void VibrationHouse()
    {
        print("vibration");
        DialogueManager.instance.PlayDlg();
        bc[0].enabled = true;
        bc[1].enabled = true;
    }

    public void ActiveEvent()
    {
        gameEventListener.enabled = true;
        interactionObjData.IsOkInteracting = true;
    }

    //액자 근처에서 아이템 사용하는지 확인
    public bool CheckDistanceToPlayer()
    {
        float dir = Vector2.Distance(PlayerInfoData.instance.playerTr.position, transform.position);
        return dir < 5.5f ? true : false;
    }
}
