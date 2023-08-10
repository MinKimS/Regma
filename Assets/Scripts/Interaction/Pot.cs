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
    private bool isPushing = false;
    bool isPushAtLeft = false;
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

    private void Start() {
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
                TimelineManager.instance.SetTimelineStart("PotT");
            }
            if(potTr.localPosition.x < -xDestination)
            {
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
                TimelineManager.instance.SetTimelineStart("PotT");
            }
        }   
    }

    private void Update() {
        if(isPushing && (isPushAtLeft&&Input.GetKeyDown(KeyCode.LeftArrow)) || (!isPushAtLeft&&Input.GetKeyDown(KeyCode.RightArrow)))
        {
            interactionData.IsInteracting = false;
            CancelPush(playerPos);
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
    }

    //화분 밀기 취소
    public void CancelPush(Transform chPos)
    {
        playerAnim.SetBool("isPush",false);
        isPushing = false;
        potColl.isTrigger = true;
    }

    //장애물이 안되게 설정
    //타임라인에서 사용
    public void EndPushPot()
    {
        inven.potEvent[0].SetActive(false);
        inven.potEvent[1].SetActive(false);
        isPushing = false;
        playerAnim.SetBool("isPush",false);
        potColl.isTrigger = true;
        potColl.enabled = false;
        blacketColl.enabled = true;
        PotDlg(1);
        door.checkWorkDo++;
        door.isOpen = true;
        if(door.checkWorkDo > 2 && door.isOpen){door.SetDoorOpen();}
    }
}
