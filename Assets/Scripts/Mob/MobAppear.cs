using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAppear : MonoBehaviour
{
    public GameObject[] mob;
    public MoveAlongThePath[] matp;
    public Dialogue[] dlg;
    Transform playerPos;
    public CinemachineVirtualCamera vCam;

    public bool isMobAppear = false;
    [HideInInspector] public bool isLastMob = false;
    [HideInInspector] public bool isReadySpawn = true;

    private void Awake()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void OnTable()
    {
        isMobAppear = true;
        StartCoroutine(AppearOnTalbeInKitchen());
    }

    IEnumerator AppearOnTalbeInKitchen()
    {
        mob[0].SetActive(true);
        Camera.main.GetComponent<CameraController>().target = mob[0].transform;
        vCam.Follow = mob[0].transform;
        DialogueManager.instance.PlayDlg(dlg[0]);
        AudioManager.instance.SFXPlay("�ֹ�_������ü ����");
        AudioManager.instance.SFXPlay("�ֹ�_������ü1 ����");
        yield return new WaitWhile(() => DialogueManager.instance._dlgState != DialogueManager.DlgState.End);
        ActivateTrace(0);
    }

    public void OnCabinet()
    {
        isMobAppear = true;
        mob[1].SetActive(true);
        ActivateTrace(1);
    }

    public void LastInKitchen()
    {
        isLastMob = true;
        isMobAppear = true;
        mob[2].SetActive(true);
        mob[2].transform.position = PlayerInfoData.instance.playerTr.position + Vector3.right * 6f;
        isReadySpawn = false;
        ActivateTrace(2);
    }

    //���� ���۽�Ű��
    public void ActivateTrace(int num)
    {
        matp[num].IsTrace = true;
        Camera.main.GetComponent<CameraController>().target = playerPos;
        vCam.Follow = playerPos;
        AudioManager.instance.StopSFX("�ֹ�_������ü ����");
        AudioManager.instance.StopSFX("�ֹ�_������ü1 ����");
        AudioManager.instance.SFXPlayLoop("�ֹ�_������ü1 ���� �߰�");
    }
}
