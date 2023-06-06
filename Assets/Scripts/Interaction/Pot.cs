using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    Transform potTr;
    Vector3 potOriginPos;
    float playerPushPos;
    BoxCollider2D potColl;
    public GameObject Event;
    public Diary diary;

    private void Start() {
        potTr = GetComponent<Transform>();
        potColl = GetComponent<BoxCollider2D>();
        potOriginPos = potTr.position;
        playerPushPos = potTr.position.x - 1.5f;
    }


    //화분 밀기
    public void PushPot(Transform chPos)
    {
        chPos.position = new Vector3(playerPushPos, chPos.position.y);
        potColl.isTrigger = false;
        Event.SetActive(true);
    }

    //화분 밀기 취소
    public void CancelPush(Transform chPos)
    {
        chPos.position = new Vector3(potTr.position.x - 1.5f, chPos.position.y);
        potTr.position = potOriginPos;
        potColl.isTrigger = true;
        Event.SetActive(false);
    }

    //장애물이 안되게 설정
    public void EndPushPot()
    {
        potColl.isTrigger = true;
    }
}
