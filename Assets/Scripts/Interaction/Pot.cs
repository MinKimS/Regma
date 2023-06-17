using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    Transform potTr;
    BoxCollider2D potColl;
    public GameObject Event;
    public Diary diary;
    public BoxCollider2D blacketColl;
    private bool isPushing = false;

    private void Start() {
        potTr = GetComponent<Transform>();
        potColl = GetComponent<BoxCollider2D>();
    }
    private void Update() {
        if(isPushing)
        {
            
        }
    }

    //화분 밀기
    public void PushPot(Transform chPos)
    {
        isPushing = true;
        //화분 오른쪽에 플레이어가 있는 경우
        if(transform.position.x < chPos.position.x)
        {
            chPos.position = new Vector3((potTr.position.x + 1.5f), chPos.position.y);
        }
        //화분 왼쪽에 플레이어가 있는 경우
        else
        {
            chPos.position = new Vector3((potTr.position.x - 1.5f), chPos.position.y);
        }
        potColl.isTrigger = false;
        Event.SetActive(true);
    }

    //화분 밀기 취소
    public void CancelPush(Transform chPos)
    {
        isPushing = false;
        potColl.isTrigger = true;
        Event.SetActive(false);
    }

    //장애물이 안되게 설정
    public void EndPushPot()
    {
        isPushing = false;
        potColl.isTrigger = true;
        potColl.enabled = false;
        blacketColl.enabled = true;
    }
}
