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

    private void Awake()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void OnTable()
    {
        StartCoroutine(AppearOnTalbeInKitchen());
    }

    IEnumerator AppearOnTalbeInKitchen()
    {
        mob[0].SetActive(true);
        Camera.main.GetComponent<CameraController>().target = mob[0].transform;
        AudioManager.instance.SFXPlay("주방_괴생명체 등장");
        AudioManager.instance.SFXPlay("주방_괴생명체1 음성");
        vCam.Follow = mob[0].transform;
        DialogueManager.instance.PlayDlg(dlg[0]);
        while(!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        ActivateTrace(0);
    }

    public void OnCabinet()
    {
        StartCoroutine(AppearOnCabinetInKitchen());
    }

    IEnumerator AppearOnCabinetInKitchen()
    {
        mob[1].SetActive(true);
        Camera.main.GetComponent<CameraController>().target = mob[1].transform;
        vCam.Follow = mob[1].transform;
        AudioManager.instance.SFXPlay("주방_괴생명체 등장");
        AudioManager.instance.SFXPlay("주방_괴생명체1 음성");
        DialogueManager.instance.PlayDlg(dlg[0]);
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        ActivateTrace(1);
    }

    public void LastInKitchen()
    {
        StartCoroutine(AppearLastInKitchen());
    }

    IEnumerator AppearLastInKitchen()
    {
        mob[2].SetActive(true);
        Camera.main.GetComponent<CameraController>().target = mob[2].transform;
        vCam.Follow = mob[2].transform;
        AudioManager.instance.SFXPlay("주방_괴생명체 등장");
        AudioManager.instance.SFXPlay("주방_괴생명체1 음성");
        DialogueManager.instance.PlayDlg(dlg[0]);
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        ActivateTrace(2);
    }

    //추적 시작시키기
    public void ActivateTrace(int num)
    {
        matp[num].IsTrace = true;
        Camera.main.GetComponent<CameraController>().target = playerPos;
        vCam.Follow = playerPos;
        AudioManager.instance.StopSFX("주방_괴생명체 등장");
        AudioManager.instance.StopSFX("주방_괴생명체1 음성");
        AudioManager.instance.SFXPlayLoop("주방_괴생명체1 도원 추격");
    }
}
