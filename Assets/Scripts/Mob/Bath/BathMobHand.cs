using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    [HideInInspector] public bool isCatchPlayer = false;

    [HideInInspector] public BathToy bathToy;
    BathToy catchBathToy;

    public float moveSpeed = 10f;
    public float dragSpeed = 10f;

    [HideInInspector] public bool isCatchSomething = false;
    [HideInInspector] public bool isTargetPlayer = false;
    bool isOkAttackTarget = false;
    Transform targetPos;
    [HideInInspector] public BathToy targetToy = null;
    [HideInInspector] public BathMobData data;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTargetPlayer && collision.CompareTag("Toy"))
        {
            CatchSomething(false, collision);
        }
        else if (isTargetPlayer && collision.CompareTag("Player"))
        {
            if (data.state != BathMobData.State.RuningWild)
            {
                CatchSomething(true, collision);
            }
            else
            {
                if(data.IsTryCatchPlayer)
                {
                    BringWhatCaughtToMob();
                }
            }
        }
        isOkAttackTarget = false;
    }

    //타겟을 특정 장난감으로 설정
    public void SetTargetToy()
    {
        isTargetPlayer = false;
        targetToy = bathToy;
        if (targetToy != null)
        {
            targetPos = targetToy.transform;
            Debug.LogError(targetToy.name);
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
    //타겟을 플레이어로 설정
    public void SetTargetPlayer()
    {
        isTargetPlayer = true;
        targetPos = PlayerInfoData.instance.playerTr;
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

    //잡은 걸 몬스터에게 가져오기
    void BringWhatCaughtToMob()
    {
        targetPos = PlayerInfoData.instance.playerTr;
        data.IsTryCatchPlayer = false;
        isCatchSomething = true;
        targetPos.SetParent(transform);
        targetPos.GetComponentInParent<Collider2D>().enabled = false;
        ChMovingInBath playerMoving = targetPos.GetComponent<ChMovingInBath>();
        playerMoving.enabled = false;

        transform.position = new Vector2(data.transform.position.x, handOriginPos.position.y);
        MoveIntoWater(0.45f);
    }

    //공격
    public void AttackTarget(float moveHandSpeed)
    {
        isMoveHand = true;
        StartCoroutine(IEAttackTarget(moveHandSpeed));
    }
    IEnumerator IEAttackTarget(float moveHandSpeed)
    {
        //공격하기 위한 준비
        ReadyToAttack(moveHandSpeed);

        yield return new WaitUntil(() => isOkAttackTarget);
        //타겟으로 이동
        if (!isCatchSomething)
        {
            MoveToTarget(moveHandSpeed);
        }
    }

    //공격할 위치로 이동
    void ReadyToAttack(float moveHandSpeed)
    {
        //Vector2 attackPos = new Vector2(targetPos.position.x, handOriginPos.position.y + 5f);
        StartCoroutine(IEReadyToAttack(moveHandSpeed));
    }
    IEnumerator IEReadyToAttack(float moveHandSpeed)
    {
        while(Vector2.Distance(transform.position, new Vector2(targetPos.position.x, handOriginPos.position.y + 5f)) > 0.03f && !data.IsTryCatchPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(targetPos.position.x, handOriginPos.position.y + 5f), moveHandSpeed);
            yield return null;
        }

        bc.enabled = true;
        isOkAttackTarget = true;
    }
    
    //타겟으로 이동
    void MoveToTarget(float moveHandSpeed)
    {
        Vector3 _targetPos = targetPos.position;

        if (!isTargetPlayer)
        {
            _targetPos += Vector3.up * 2f;
        }

        StartCoroutine(IEMoveToTarget(_targetPos, moveHandSpeed));
    }
    IEnumerator IEMoveToTarget(Vector3 _targetPos, float moveHandSpeed)
    {
        while(Vector2.Distance(transform.position, _targetPos) > 0.02f && !isCatchSomething && !data.IsTryCatchPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPos, moveHandSpeed);
            yield return null;
        }
        transform.position = _targetPos;

        if(data.IsTryCatchPlayer)
        {
            BackToOriginHandPos(0.5f);
        }
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

        MoveIntoWater(0.45f);
    }

    //물 속으로 끌고 가기
    void MoveIntoWater(float moveHandSpeed)
    {
        bc.enabled = false;

        StartCoroutine(IEMoveIntoWater(moveHandSpeed));
    }
    IEnumerator IEMoveIntoWater(float moveHandSpeed)
    {
        while(Vector2.Distance(transform.position, new Vector3(transform.position.x, dragPos.position.y)) > 0.02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x, dragPos.position.y), moveHandSpeed);
            yield return null;
        }

        ReleaseWhatCaught();
        BackToOriginHandPos(0.5f);
    }
    
    //잡았던 타겟 놓기
    void ReleaseWhatCaught()
    {
        targetPos.SetParent(null);
        targetToy = null;
        isCatchSomething = false;
        targetPos.gameObject.SetActive(false);
        toyIdx++;
    }

    //원래 손이 있어야 할 위치로 이동
    void BackToOriginHandPos(float moveHandSpeed)
    {
        StartCoroutine(IEBackToOriginHandPos(moveHandSpeed));
    }
    IEnumerator IEBackToOriginHandPos(float moveHandSpeed)
    {
        while (Vector2.Distance(transform.position, handOriginPos.position) > 0.02f && !data.IsTryCatchPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, handOriginPos.position, moveHandSpeed);
            yield return null;
        }
        transform.position = handOriginPos.position;
        isMoveHand = false;
    }

    public void SetToyIdx(int idx)
    {
        toyIdx = idx;
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
            Debug.LogWarning("gocatchplayer");
            yield return null;
        }

        bc.enabled = true;
    }
}
