using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Pot : MonoBehaviour
{
    Transform potTr;
    public Transform potAxisTr;
    public BoxCollider2D potColl;
    public BoxCollider2D blacketColl;
    bool isPushing = false;
    bool isPushAtLeft = false;
    bool isOkPush = false;
    public Animator potAnim;
    public GameObject[] bloods;
    //밀어서 도달해야하는 곳
    float xDestination = 2.4f;
    public Fade fade;
    Transform playerPos;
    //화분 미는 모션 애니 적용
    Animator playerAnim;
    public GameObject player;
    InteractionObjData interactionData;
    public Dialogue[] dlg;
    public Door door;
    public Inventory inven;

    private void Start()
    {
        potTr = GetComponent<Transform>();
        interactionData = GetComponent<InteractionObjData>();

        playerAnim = player.GetComponent<Animator>();
        playerPos = player.GetComponent<Transform>();

        interactionData.IsOkInteracting = true;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(isPushing)
        {
            // 목표위치와의 거리에따른 흔들림정도 설정
            if(potTr.localPosition.x > -xDestination*0.4 && potTr.localPosition.x < xDestination*0.4)
            {
                potAnim.SetBool("isShake", false);
            }
            if((potTr.localPosition.x > -xDestination*0.8 && potTr.localPosition.x < -xDestination*0.4) || (potTr.localPosition.x < xDestination*0.8 && potTr.localPosition.x > xDestination*0.4))
            {
                potAnim.SetBool("isShake", true);
                potAnim.SetBool("isShakeHard", false);
            }
            if(potTr.localPosition.x > xDestination*0.8 || potTr.localPosition.x < -xDestination*0.8)
            {
                potAnim.SetBool("isShakeHard", true);
            }
            
            //끝까지 밀어서 화분이 넘거가는 경우
            if(potTr.localPosition.x > xDestination)
            {
                AudioManager.instance.StopSFX("Game Sound_Pot");
                potColl.isTrigger = true;
                potAnim.SetBool("isFallLeft", false);
                potAnim.SetBool("isFall", true);
                playerAnim.SetBool("isPush",false);
                potTr.localPosition = new Vector2(0f, potTr.localPosition.y);
                potAxisTr.localPosition = new Vector2(17f, potAxisTr.localPosition.y);
                fade.Invoke("SetBlack", 0.5f);
                for(int i =0; i<bloods.Length; i++)
                {        
                    bloods[i].GetComponent<SpriteRenderer>().enabled = true;
                    bloods[i].SetActive(false);
                }
                TimelineManager.instance.timelineController.SetTimelineStart("PotT");
            }
            if(potTr.localPosition.x < -xDestination)
            {
                AudioManager.instance.StopSFX("Game Sound_Pot");
                potColl.isTrigger = true;
                potAnim.SetBool("isFallLeft", true);
                potAnim.SetBool("isFall", true);
                playerAnim.SetBool("isPush",false);
                potTr.localPosition = new Vector2(0f, potTr.localPosition.y);
                potAxisTr.localPosition = new Vector2(12.2f, potAxisTr.localPosition.y);
                fade.Invoke("SetBlack", 0.5f);

                //위치 변경
                for(int i =0; i<bloods.Length; i++)
                {        
                    Transform bloodTr = bloods[i].GetComponent<Transform>();
                    float tempX = bloodTr.localPosition.x;
                    tempX = -tempX;
                    bloodTr.localPosition = new Vector2(tempX, bloodTr.position.y);
                    bloods[i].GetComponent<SpriteRenderer>().enabled = true;
                    bloods[i].SetActive(false);
                }
                TimelineManager.instance.timelineController.SetTimelineStart("PotT");
            }
        }   
    }

    private void Update()
    {
        if (isPushing 
            && ((isPushAtLeft && Input.GetAxisRaw("Horizontal")<0) || (!isPushAtLeft && Input.GetAxisRaw("Horizontal")>0)))
        {
            CancelPush(playerPos);
        }

        if(!isOkPush && SmartphoneManager.instance.diary.isAfterDlg)
        {
            isOkPush = true;
            interactionData.GmEventIdx=1;
        }
    }

    //타임라인에서도 사용
    public void PotDlg(int value)
    {
        DialogueManager.instance.PlayDlg(dlg[value]);
    }

    //화분 밀기
    public void PushPot(Transform chPos)
    {
        interactionData.IsRunInteraction = true;
        isPushing = true;
        //화분 오른쪽에 플레이어가 있는 경우
        if(transform.position.x < chPos.position.x)
        {
            chPos.position = new Vector3((potTr.position.x + 1.5f), chPos.position.y);
            isPushAtLeft = false;
        }
        //화분 왼쪽에 플레이어가 있는 경우
        else
        {
            chPos.position = new Vector3((potTr.position.x - 1.5f), chPos.position.y);
            isPushAtLeft = true;
        }
        potColl.isTrigger = false;
        playerAnim.SetBool("isPush",true);
        AudioManager.instance.SFXPlayLoop("Game Sound_Pot");
    }

    //화분 밀기 취소
    public void CancelPush(Transform chPos)
    {
        interactionData.IsRunInteraction = false;
        playerAnim.SetBool("isPush",false);
        isPushing = false;
        potColl.isTrigger = true;
        AudioManager.instance.StopSFX("Game Sound_Pot");
    }

    //장애물이 안되게 설정
    //타임라인에서 사용
    public void EndPushPot()
    {
        Camera.main.GetComponent<CameraController>().target = playerPos;
        isPushing = false;
        playerAnim.SetBool("isPush",false);
        potColl.isTrigger = true;
        potColl.enabled = false;
        blacketColl.enabled = true;
        PotDlg(1);
    }

    public void SetCameraPot()
    {
        Camera.main.GetComponent<CameraController>().target = transform;
    }

    public void GetBlanket(ItemData item)
    {
        AudioManager.instance.SFXPlay("Game Sound_Item get");
        SmartphoneManager.instance.SetInvenItem(item);
        DialogueManager.instance.PlayDlg(dlg[2]);
        door.checkWorkDo++;
        door.isOpen = true;
        blacketColl.gameObject.SetActive(false);
    }
}
