using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REFRIG : MonoBehaviour
{
    InteractionObjData iod;
    public REFRIGPower rPower;
    public ItemData squid;
    public Transform mobAppear;
    public Transform respawnPoint_Refrig;
    public Transform talkStarting;
    public MoveAlongThePath mob;
    public MobAppear appear;

    bool isOpened = false;
    [HideInInspector] public bool isActivateEvent = false;

    public BoxCollider2D powerBc;

    public GameObject trigger;
    public GameObject dlgTrigger;

    private void Awake()
    {
        iod = GetComponent<InteractionObjData>();
        if (rPower == null)
        {
            rPower = FindObjectOfType<REFRIGPower>();
        }
    }

    //냉장고 문 열기
    public void OpenREFRIG()
    {
        if(iod == null)
        {
            iod = GetComponent<InteractionObjData>();
        }
        if(!isOpened)
        {
            //어댑터 고장내기 전
            if (!rPower.isBroken)
            {
                if(!isActivateEvent)
                {
                    isActivateEvent = true;
                    powerBc.enabled = true;
                    dlgTrigger.SetActive(true);
                    StartCoroutine(ReadyToMobAppear());
                }
                else
                {
                    mob.AppearMob();
                    respawnPoint_Refrig.gameObject.SetActive(true);
                }
            }
            //어댑터 고장낸 후
            else
            {
                DialogueManager.instance.PlayDlg(iod.objDlg[1]);
                SmartphoneManager.instance.SetInvenItem(squid);
                AudioManager.instance.SFXPlay("Kitchen_refrigerator open");
                isOpened = true;
                iod.IsOkInteracting = false;
                StartCoroutine(GetSquid());
                respawnPoint_Refrig.gameObject.SetActive(true);
            }
        }
    }

    public void DlgStart()
    {
        SmartphoneManager.instance.phone.StartTalk();
    }

    IEnumerator ReadyToMobAppear()
    {
        DialogueManager.instance.PlayDlg(iod.objDlg[0]);
        trigger.SetActive(false);
        yield return new WaitUntil(() => DialogueManager.instance._dlgState == DialogueManager.DlgState.End);
        AudioManager.instance.SFXPlay("주방_괴생명체 등장");
        AudioManager.instance.SFXPlay("주방_괴생명체1 음성");
        mobAppear.gameObject.SetActive(true);
        respawnPoint_Refrig.gameObject.SetActive(true);
    }
    IEnumerator GetSquid()
    {
        yield return new WaitWhile(() => DialogueManager.instance._dlgState != DialogueManager.DlgState.End);
        TimelineManager.instance.timelineController.SetTimelineStart("GetSquid");
        AudioManager.instance.SFXPlay("Game Sound_Item get");
        yield return new WaitWhile(() => TimelineManager.instance._Tlstate != TimelineManager.TlState.End);
        talkStarting.gameObject.SetActive(true);
        talkStarting.position = PlayerInfoData.instance.playerTr.position;
    }
    private void Update()
    {
        if (appear.isLastMob && !RespawnManager.isGameOver && !appear.mob[2].gameObject.activeSelf && !appear.isReadySpawn && Vector2.Distance(PlayerInfoData.instance.playerTr.position, RespawnManager.Instance.respawnPositionByType[RespawnManager.ChangeMethod.MobBased].position) > 0.02f)
        {
            respawnPoint_Refrig.gameObject.SetActive(true);
            appear.isReadySpawn = true;
            talkStarting.position = RespawnManager.Instance.respawnPositionByType[RespawnManager.ChangeMethod.MobBased].position;
            talkStarting.gameObject.SetActive(true);
            appear.isMobAppear = false;
        }
    }
}
