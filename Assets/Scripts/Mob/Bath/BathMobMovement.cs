using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobMovement : MonoBehaviour
{
    public Transform[] movePos;
    int movePosIdx = 0;
    Vector2 curMovePos;
    
    //patrol에서 조금 씩 움직이는 양
    public float moveValueX = 5f;
    //mob 움직이는 속도
    public float mobMoveSpeed = 4f;
    public float mobRunWildSpeed = 1f;
    float offset = 0.01f;


    //물 안밖으로 나갈때의 이동 값
    //public float moveOutOfWaterValue = 2f;
    public Transform moveInWaterPos;
    public Transform moveOutWaterPos;

    bool isMoving = false;
    [HideInInspector] public bool isStartRunWild = false;
    [HideInInspector] public bool isStartAttack = false;

    //몬스터의 처음 위치
    public Transform mobInitialPos;

    [HideInInspector] public IEnumerator moveOutOfTheWater;
    [HideInInspector] public IEnumerator moveIntoTheWater;

    private void Start()
    {
        moveOutOfTheWater = GoOutOfTheWater();
        moveIntoTheWater = GoIntoTheWater();
    }

    private void Update()
    {
        if (!isMoving && PlayerInfoData.instance.playerTr.position.x > movePos[movePosIdx].position.x)
        {
            StartCoroutine(MoveNextPos());
        }

        if(isStartRunWild)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(movePos[movePos.Length-1].position.x, transform.position.y), mobRunWildSpeed * Time.deltaTime);
        }
    }

    //몬스터 시작 위치 설정
    public void SetMobPosInitialPos()
    {
        transform.position = mobInitialPos.position;
    }
    //몬스터가 모습을 보임
    public void ShowMob()
    {
        gameObject.SetActive(true);
    }
    //몬스터가 모습을 감춤
    public void HideMob()
    {
        gameObject.SetActive(false);
    }

    //몬스터가 물 밖으로 이동
    public void MoveOutWater()
    {
        StartCoroutine(GoOutOfTheWater());

    }
    public IEnumerator GoOutOfTheWater()
    {
        while (transform.position.y <= moveOutWaterPos.position.y - 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, moveOutWaterPos.position.y + 1.5f), 20f * Time.deltaTime);
            yield return null;
        }

        float targetPosY = transform.position.y - 1.5f;
        while (transform.position.y >= targetPosY + 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, targetPosY), 0.01f);
            yield return null;
        }
    }
    public void SetMoveOutWater()
    {
        StartCoroutine(SetOutOfTheWater());
    }
    public IEnumerator SetOutOfTheWater()
    {
        while (transform.position.y <= moveOutWaterPos.position.y - 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, moveOutWaterPos.position.y), 17f * Time.deltaTime);
            yield return null;
        }
        isStartAttack = true;
    }

    //몬스터가 물 안으로 이동
    public void MoveInWater()
    {
        StartCoroutine(GoIntoTheWater());
    }
    public IEnumerator GoIntoTheWater()
    {
        Vector2 targetPos = new Vector2(transform.position.x, moveInWaterPos.position.y);
        while (transform.position.y >= targetPos.y + 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 17f * Time.deltaTime);
            yield return null;
        }
    }

    //---

    public IEnumerator MoveNextPos()
    {
        isMoving = true;

        //다음 플레이어를 공격할 수 있는 위치로 이동
        if(!isStartAttack)
        {
            while (transform.position.x <= (PlayerInfoData.instance.playerTr.position.x - 3f) - offset)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(PlayerInfoData.instance.playerTr.position.x - 3f, transform.position.y), mobMoveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            curMovePos = movePos[movePosIdx].position;

            while (transform.position.x <= curMovePos.x - offset)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(curMovePos.x, transform.position.y), mobMoveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        if (movePos.Length - 1 != movePosIdx)
        {
            movePosIdx++;
        }
        isMoving = false;
    }
}
