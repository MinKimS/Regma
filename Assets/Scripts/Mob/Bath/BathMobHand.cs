using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobHand : MonoBehaviour
{
    BoxCollider2D bc;

    //Vector2 targetPos;
    //float targetPosY;
    public Transform dragPos;
    Vector2 dragPosY;
    public Transform playerPos;

    //손 초기 위치
    public Transform handOriginPos;

    //욕조의 장난감들
    public Transform[] toyList;
    [HideInInspector] public int toyIdx = 0;

    [HideInInspector]
    public bool isMoveHand = false;
    bool isBackOrigin = true;
    bool isCatchSomething = false;

    [HideInInspector] public BathToy bathToy;

    public float moveSpeed = 10f;
    public float dragSpeed = 10f;


    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    //손을 장난감을 향해 이동
    public void MoveHandToToy()
    {
        isMoveHand = true;
        isBackOrigin = false;
        StartCoroutine(GoCatchToy());
    }
    public void MoveHandToToyToAttack()
    {
        if(bathToy != null)
        {
            isMoveHand = true;
            isBackOrigin = false;
            StartCoroutine(GoCatchToyToAttack());
        }
    }
    public void MoveHandToToy(float speed = 20)
    {
        isMoveHand = true;
        isBackOrigin = false;
        StartCoroutine(GoCatchToy(speed));
    }
    public IEnumerator GoCatchToy()
    {
        //장난감으로 가기
        while (Vector2.Distance(transform.position, toyList[toyIdx].position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, toyList[toyIdx].position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        bc.enabled = true;

        yield return new WaitForSeconds(1f);
        if(!isBackOrigin && !isCatchSomething)
        {
            BackOriginPos();
        }
    }
    public IEnumerator GoCatchToyToAttack()
    {
        //장난감으로 가기
        while (Vector2.Distance(transform.position, bathToy.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, bathToy.transform.position, moveSpeed *1.5f * Time.deltaTime);
            yield return null;
        }

        bc.enabled = true;
    }
    public IEnumerator GoCatchToy(float speed = 20)
    {
        //장난감으로 가기
        while (Vector2.Distance(transform.position, toyList[toyIdx].position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, toyList[toyIdx].position, speed * Time.deltaTime);
            yield return null;
        }

        bc.enabled = true;
    }
    //손을 플레이어를 향해 이동
    public void MoveHandToPlayer(float speed = 30)
    {
        isMoveHand = true;
        StartCoroutine(GoCatchPlayer(speed));
    }

    public IEnumerator GoCatchPlayer(float speed)
    {
        //플레이어에게 가기
        while (Vector2.Distance(transform.position, PlayerInfoData.instance.playerTr.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerInfoData.instance.playerTr.position, speed * Time.deltaTime);
            yield return null;
        }

        bc.enabled = true;
    }
    //플레이어or장난감을 물 속으로 끌고 감
    void DragIntoTheWater(Transform tr)
    {
        isCatchSomething = true;
        StartCoroutine(MoveHandDown(tr));
    }
    IEnumerator MoveHandDown(Transform tr)
    {
        while (transform.position.y >= dragPos.position.y + 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, dragPosY, dragSpeed * Time.deltaTime);
            yield return null;
        }

        tr.SetParent(null);
        tr.gameObject.SetActive(false);

        if (!toyList[toyIdx].gameObject.activeSelf)
        {
            toyIdx++;
        }

        yield return new WaitForSeconds(0.1f);
        BackOriginPos();
    }

    //원래 손 위치로 이동
    void BackOriginPos()
    {
        isCatchSomething = false;
        StartCoroutine(BackHandOrigin());
    }
    IEnumerator BackHandOrigin()
    {
        while (Vector2.Distance(transform.position, handOriginPos.position) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, handOriginPos.position, 0.1f);
            yield return null;
        }
        isMoveHand = false;
        bathToy = null;
        isBackOrigin = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Toy")&& isMoveHand)
        {
            PolygonCollider2D col = collision.GetComponent<PolygonCollider2D>();
            col.enabled = false;
            bc.enabled = false;

            Transform tr = collision.GetComponentsInParent<Transform>()[1];
            tr.SetParent(gameObject.transform);
            dragPosY = new Vector2(transform.position.x, dragPos.position.y);

            DragIntoTheWater(tr);
        }
        if(toyIdx >= toyList.Length-1 && collision.CompareTag("Player"))
        {
            CapsuleCollider2D col = collision.GetComponent<CapsuleCollider2D>();
            col.enabled = false;
            bc.enabled = false;

            Transform tr = collision.GetComponent<Transform>();
            tr.SetParent(gameObject.transform);
            collision.GetComponent<Chmoving>().enabled = false;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0f;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            dragPosY = new Vector2(transform.position.x, dragPos.position.y);
        }
        //플레이어를 잡은 경우
        //if (collision.CompareTag("Player") && !bmc.IsMobInWater)
        //{
        //    bc.enabled = false;

        //    player = collision.gameObject;
        //    player.transform.SetParent(transform);

        //    //물 효과 영향 안받게
        //    player.GetComponent<CapsuleCollider2D>().enabled = false;
        //    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //    player.GetComponent<Rigidbody2D>().gravityScale = 0f;

        //    //targetPosY = transform.position.y-10f;
        //    //targetPos = new Vector2(transform.position.x, targetPosY);
        //    dragPosY = new Vector2(transform.position.x, dragPos.position.y);
        //    //플레이어 끌어내리기
        //    bmc.StopMoving();
        //    StartCoroutine(MoveHandDown());
        //    bmc.isCatchPlayer = true;
        //}
    }

    //---

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //플레이어를 잡은 경우
    //    if(collision.CompareTag("Player") && !bmc.IsMobInWater)
    //    {
    //        bc.enabled = false;

    //        player = collision.gameObject;
    //        player.transform.SetParent(transform);

    //        //물 효과 영향 안받게
    //        player.GetComponent<CapsuleCollider2D>().enabled = false;
    //        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //        player.GetComponent<Rigidbody2D>().gravityScale = 0f;

    //        //targetPosY = transform.position.y-10f;
    //        //targetPos = new Vector2(transform.position.x, targetPosY);
    //        dragPosY = new Vector2(transform.position.x, dragPos.position.y);
    //        //플레이어 끌어내리기
    //        bmc.StopMoving();
    //        StartCoroutine(MoveHandDown());
    //        bmc.isCatchPlayer = true;
    //    }

    //    //낚시대를 잡은 경우
    //    if(collision.CompareTag("FishingRod"))
    //    {
    //        bc.enabled = false;

    //        GameObject fRod = collision.gameObject;
    //        fRod.transform.SetParent(transform);

    //        //targetPosY = transform.position.y -8f;
    //        dragPosY = new Vector2(transform.position.x, dragPos.position.y);
    //        StartCoroutine(DropPlayer());
    //    }
    //}

    //플레이어 다음 이동시, 물 속에 몬스터 있을때
    //장난감을 향해 손 이동
    //public IEnumerator GoCatchToy()
    //{
    //    //장난감으로 가기
    //    while (Vector2.Distance(transform.position, ToyList[TargetToyIdx].position)>0.1f)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, ToyList[TargetToyIdx].position, 0.1f);
    //        yield return null;
    //    }

    //    bc.enabled = true;
    //    isMoveHand = false;
    //    TargetToyIdx++;
    //}

    //public IEnumerator GoCatchPlayer()
    //{
    //    while (Vector2.Distance(transform.position, playerPos.position) > 0.1f)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, 1f);
    //        yield return null;
    //    }

    //    bc.enabled = true;
    //    isMoveHand = false;
    //}

    ////낚시대를 향해 손 이동
    //public IEnumerator GoCatchFishingRod()
    //{
    //    bmc.fRod.gameObject.GetComponent<FishingRod>().IsCaught = true;

    //    while (Vector2.Distance(transform.position, bmc.fRod.position) > 0.1f)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, bmc.fRod.position, 0.1f);
    //        yield return null;
    //    }
    //    bc.enabled = true;

    //    isMoveHand = false;
    //}

    //물로 끌고가기
    //IEnumerator MoveHandDown()
    //{
    //    while (transform.position.y >= dragPos.position.y + 0.01f)
    //    {
    //        transform.position = Vector2.Lerp(transform.position, dragPosY, 0.1f);
    //        yield return null;
    //    }

    //    player.transform.SetParent(null);
    //    player.SetActive(false);

    //    yield return new WaitForSeconds(0.1f);
    //    BackOriginPos();
    //}

    //플레이어를 끌어내리기
    //IEnumerator DropPlayer()
    //{
    //    //아래로 빠르게 쭉 내림
    //    while (transform.position.y >= dragPos.position.y + 0.01f)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, dragPosY, 0.05f);
    //        yield return null;
    //    }

    //    //플레이어가 아래로 내려옴
    //    bmc.PlayerPos.position = transform.position + Vector3.left * 3;
    //    bmc.PlayerPos.rotation = Quaternion.Euler(0f, 0f, 180f);
    //    yield return new WaitForSeconds(1.5f);
    //    Rigidbody2D pRb = bmc.PlayerPos.GetComponent<Rigidbody2D>();
    //    pRb.gravityScale = 0f;
    //    pRb.velocity = Vector3.zero;
    //}

    //void BackOriginPos()
    //{
    //    StartCoroutine(BackHandOrigin());
    //    print("back");
    //}

    //원래 손 위치로 이동
    //IEnumerator BackHandOrigin()
    //{
    //    while(Vector2.Distance(transform.position, handOriginPos.position) > 0.1f)
    //    {
    //        transform.position = Vector2.Lerp(transform.position, handOriginPos.position, 0.1f);
    //        yield return null;
    //    }
    //    bmc.IsMobTryCatch = false;
    //}
}
