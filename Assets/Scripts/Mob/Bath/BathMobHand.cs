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
    [HideInInspector] public bool isCatchPlayer = false;

    [HideInInspector] public BathToy bathToy;
    BathToy catchBathToy;

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
        if(bathToy != null && !bathToy.isDrawning)
        {
            bathToy.isBeingTarget = true;
            isMoveHand = true;
            isBackOrigin = false;
            StartCoroutine(GoCatchToyToAttack());
        }
    }
    public void MoveHandToToyToAttack(float speed = 20f)
    {
        if(bathToy != null)
        {
            bathToy.isBeingTarget = true;
            isMoveHand = true;
            isBackOrigin = false;
            StartCoroutine(GoCatchToyToAttack(speed));
        }
    }
    public void MoveHandToToy(float speed = 20)
    {
        isMoveHand = true;
        isBackOrigin = false;
        catchBathToy = toyList[toyIdx].GetComponent<BathToy>();
        catchBathToy.isBeingTarget = true;
        StartCoroutine(GoCatchToy(speed));
    }
    public IEnumerator GoCatchToy()
    {
        //장난감으로 가기
        while (Vector2.Distance(transform.position, toyList[toyIdx].position) > 0.1f)
        {
            transform.localPosition = Vector2.MoveTowards(transform.position, toyList[toyIdx].position, moveSpeed * Time.deltaTime);
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
    public IEnumerator GoCatchToyToAttack(float speed = 20f)
    {
        //장난감으로 가기
        while (Vector2.Distance(transform.position, bathToy.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, bathToy.transform.position, speed * Time.deltaTime);
            yield return null;
        }

        bc.enabled = true;
    }
    public IEnumerator GoCatchToy(float speed = 20)
    {
        //장난감으로 가기
        while (Vector2.Distance(transform.position, catchBathToy.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, catchBathToy.transform.position, speed * Time.deltaTime);
            if (isCatchSomething)
            {
                break;
            }
            yield return null;
        }

        if (!isCatchSomething)
        {
            bc.enabled = true;
        }
        else
        {
            isCatchSomething = false;
            catchBathToy.gameObject.SetActive(false);
            toyIdx++;
            BackOriginPos();
        }
    }
    //손을 플레이어를 향해 이동
    public void MoveHandToPlayer(float speed = 30)
    {
        isMoveHand = true;
        StartCoroutine(GoCatchPlayer(speed));
    }

    public IEnumerator GoCatchPlayer(float speed)
    {
        yield return new WaitForSeconds(0.1f);
        //플레이어에게 가기
        while (Vector2.Distance(transform.position, PlayerInfoData.instance.playerTr.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerInfoData.instance.playerTr.position, speed * Time.deltaTime);
            yield return null;
        }

        bc.enabled = true;
    }
    
    void SetHandPos(Vector3 pos)
    {
        transform.position = pos;
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

        if (catchBathToy != null && !catchBathToy.gameObject.activeSelf)
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
        catchBathToy = null;
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
            collision.GetComponent<ChMovingInBath>().enabled = false;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0f;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            isCatchPlayer = true;

            StartCoroutine(BringToMob(tr));
        }
    }

    IEnumerator BringToMob(Transform tr)
    {
        bool isBringToOrigin = false;
        Vector2 pos = new Vector2(handOriginPos.position.x - 3f, PlayerInfoData.instance.playerTr.position.y);
        while (Vector2.Distance(transform.position, pos) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, pos, 0.1f);
            yield return null;
        }
        isMoveHand = false;
        isBackOrigin = true;
        isBringToOrigin = true;

        yield return new WaitUntil(() => isBringToOrigin);

        dragPosY = new Vector2(transform.position.x, dragPos.position.y);
        while (transform.position.y >= dragPos.position.y + 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, dragPosY, dragSpeed * Time.deltaTime);
            yield return null;
        }

        tr.SetParent(null);
        tr.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        BackOriginPos();
    }

    private void FixedUpdate()
    {
        if (isMoveHand && Vector2.Distance(transform.position, PlayerInfoData.instance.playerTr.position) > Camera.main.orthographicSize * Camera.main.aspect)
        {
            isCatchSomething = true;
        }
    }
}
