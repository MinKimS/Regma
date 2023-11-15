using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class BathMobHand : MonoBehaviour
{
    BoxCollider2D bc;

    public Transform dragPos;
    public Transform playerPos;

    //손 초기 위치
    public Transform handOriginPos;

    //욕조의 장난감들
    public Transform[] toyList;
    [HideInInspector] public int toyIdx = 0;

    [HideInInspector]
    public bool isMoveHand = false;
    [HideInInspector] public bool isCatchPlayer = false;
    [HideInInspector] public bool isTargetPlayer = false;
    bool isReadyAttack = false;
    [HideInInspector] public bool isTryCatchSomething = false;
    [HideInInspector] public bool isCatchSomething = false;
    [HideInInspector] public bool isMoveIntoWater = false;
    bool isAnimationDone = false;

    //[HideInInspector] public bool isCatchSomething = false;
    Transform targetPos;
    [HideInInspector] public BathMobData data;

    Animator animator;

    //==================================================================
    [HideInInspector] public BathToy targetToy = null;
    [HideInInspector] public BathToy bathToy;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        bc.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTargetPlayer && collision.CompareTag("Toy"))
        {
            CatchSomething(false, collision);
        }
        else if (isTargetPlayer && collision.CompareTag("Player"))
        {
            CatchSomething(true, collision);
        }
        isReadyAttack = false;
    }

    public void SetTarget()
    {
        targetToy = bathToy;
        if (targetToy != null)
        {
            SetTargetToy();
        }
        else
        {
            SetTargetPlayer();
        }
    }
    //타겟을 특정 장난감으로 설정
    public void SetTargetToy()
    {
        isTargetPlayer = false;
        targetToy = bathToy;
        if (targetToy != null)
        {
            targetPos = targetToy.transform;
            Debug.LogError("target : " + targetToy.name);
        }
    }
    public void SetTargetToy(int idx)
    {
        isTargetPlayer = false;
        targetToy = toyList[idx].GetComponent<BathToy>();
        if (targetToy != null)
        {
            targetPos = targetToy.transform;
        }
    }
    public void SetTargetToy(BathToy toy)
    {
        isTargetPlayer = false;
        targetToy = toy;
        if (targetToy != null)
        {
            targetPos = targetToy.transform;
        }
    }
    //타겟을 플레이어로 설정
    public void SetTargetPlayer()
    {
        isTargetPlayer = true;
        targetPos = PlayerInfoData.instance.playerTr;
    }

    //공격
    public void AttackTarget(float readyHandSpeed)
    {
        isMoveHand = true;
        StartCoroutine(IEAttackTarget(readyHandSpeed));
    }
    IEnumerator IEAttackTarget(float readyHandSpeed)
    {
        //공격하기 위한 준비
        ReadyToAttack(readyHandSpeed);

        yield return new WaitUntil(() => isReadyAttack);
        //타겟으로 이동
        MoveToTargetAndAttack();
    }

    //공격할 위치로 이동
    void ReadyToAttack(float moveHandSpeed)
    {
        print("ReadyToAttack");
        StartCoroutine(IEReadyToAttack(moveHandSpeed));
    }
    IEnumerator IEReadyToAttack(float moveHandSpeed)
    {
        while (Vector2.Distance(transform.position, new Vector2(targetPos.position.x, handOriginPos.position.y)) > 0.03f) //&& !data.IsTryCatchPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(targetPos.position.x, handOriginPos.position.y), moveHandSpeed);
            yield return null;
        }

        transform.position = new Vector2(targetPos.position.x, handOriginPos.position.y);

        isReadyAttack = true;
    }

    void MoveToTargetAndAttack()
    {
        StartCoroutine(IEMoveToTargetAndAttack());
    }

    IEnumerator IEMoveToTargetAndAttack()
    {
        animator.SetBool("isCatching", true);

        yield return new WaitForSeconds(0.1f);
        bc.enabled = true;
        isTryCatchSomething = true;

        yield return new WaitUntil(() => isAnimationDone);
        isAnimationDone = false;

        if (!isCatchSomething)
        {
            isCatchSomething = false;
            BackToOriginHandPos(0.4f);
        }
    }

    public void SetAnim()
    {
        isAnimationDone = true;
    }

    //원래 손이 있어야 할 위치로 이동
    void BackToOriginHandPos(float moveHandSpeed)
    {
        StartCoroutine(IEBackToOriginHandPos(moveHandSpeed));
    }
    IEnumerator IEBackToOriginHandPos(float moveHandSpeed)
    {
        while (Vector2.Distance(transform.position, handOriginPos.position) > 0.02f) //&& !data.IsTryCatchPlayer)
        {
            print("backtoOriginHandPos");
            transform.position = Vector2.MoveTowards(transform.position, handOriginPos.position, moveHandSpeed);
            yield return null;
        }
        transform.position = handOriginPos.position;
        animator.SetBool("isCatching", false);
        isMoveHand = false;
        isMoveIntoWater = false;
    }

    //무언가 잡기
    void CatchSomething(bool isPlayer, Collider2D target)
    {
        isCatchSomething = true;

        targetPos.SetParent(transform);
        Debug.LogWarning(targetPos.name + " CatchSomething");
        target.GetComponentInParent<Collider2D>().enabled = false;

        if (isPlayer)
        {
            ChMovingInBath playerMoving = target.transform.GetComponent<ChMovingInBath>();
            playerMoving.enabled = false;
        }
        if(data.state != BathMobData.State.RuningWild)
        {
            MoveIntoWater(1.45f);
        }
        else
        {
            MoveIntoWater(1.9f);
        }
    }

    //물 속으로 끌고 가기
    public void MoveIntoWater(float moveHandSpeed)
    {
        if(!isMoveIntoWater)
        {
            isMoveIntoWater = true;
            isTryCatchSomething = false;
            bc.enabled = false;

            float curX = transform.position.x;

            StartCoroutine(IEMoveIntoWater(moveHandSpeed, curX));
        }
    }
    IEnumerator IEMoveIntoWater(float moveHandSpeed, float curX)
    {
        while (Vector2.Distance(transform.position, new Vector3(curX, dragPos.position.y)) > 0.04f)
         {
            print("MoveIntoWater");
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(curX, dragPos.position.y), moveHandSpeed);
            yield return null;
        }

        data.canMove = true;
        ReleaseWhatCaught();
        isCatchSomething = false;
        BackToOriginHandPos(0.5f);
    }

    //잡았던 타겟 놓기
    void ReleaseWhatCaught()
    {
        print("ReleaseWhatCaught");
        targetPos.SetParent(null);
        if(gameObject.GetComponentsInChildren<BathToy>().Length > 0)
        {
            for(int i = 0; i< gameObject.GetComponentsInChildren<BathToy>().Length; i++)
            {
                gameObject.GetComponentsInChildren<BathToy>()[i].transform.SetParent(null);
            }
        }
        targetToy = null;
        targetPos.gameObject.SetActive(false);
        if(toyIdx < toyList.Length-1)
        {
            toyIdx++;
        }
    }

    public void SetToyIdx(int idx)
    {
        toyIdx = idx;
    }

    //public IEnumerator GoCatchToyToAttack()
    //{
    //    //장난감으로 가기
    //    while (Vector2.Distance(transform.position, bathToy.transform.position) > 0.1f)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, bathToy.transform.position, moveSpeed *1.5f * Time.deltaTime);
    //        yield return null;
    //    }

    //    bc.enabled = true;
    //}
    //public IEnumerator GoCatchToyToAttack(float speed = 20f)
    //{
    //    //장난감으로 가기
    //    while (Vector2.Distance(transform.position, bathToy.transform.position) > 0.1f)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, bathToy.transform.position, speed * Time.deltaTime);
    //        yield return null;
    //    }

    //    bc.enabled = true;
    //}
    ////손을 플레이어를 향해 이동
    //public void MoveHandToPlayer(float speed = 30)
    //{
    //    isMoveHand = true;
    //    StartCoroutine(GoCatchPlayer(speed));
    //}

    //public IEnumerator GoCatchPlayer(float speed)
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    //플레이어에게 가기
    //    while (Vector2.Distance(transform.position, PlayerInfoData.instance.playerTr.position) > 0.1f)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, PlayerInfoData.instance.playerTr.position, speed * Time.deltaTime);
    //        Debug.LogWarning("gocatchplayer");
    //        yield return null;
    //    }

    //    bc.enabled = true;
    //}
}
