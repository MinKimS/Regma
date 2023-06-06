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
    //얻게 되는 아이템의 콜라이더
    public BoxCollider2D itemColl;

    private void Start() {
        potTr = GetComponent<Transform>();
        potColl = GetComponent<BoxCollider2D>();
        potOriginPos = potTr.position;
        playerPushPos = potTr.position.x - 1.5f;
    }

    public void PushPot(Transform chPos)
    {
        chPos.position = new Vector3(playerPushPos, chPos.position.y);
        potColl.isTrigger = false;
        Event.SetActive(true);
    }

    public void CancelPush(Transform chPos)
    {
        chPos.position = new Vector3(potTr.position.x - 1.5f, chPos.position.y);
        potTr.position = potOriginPos;
        potColl.isTrigger = true;
        Event.SetActive(false);
    }

    public void EndPushPot()
    {
        potColl.isTrigger = true;
    }

    public void SetItemCanGet()
    {
        itemColl.enabled = true;
    }
}
