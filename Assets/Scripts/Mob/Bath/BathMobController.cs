using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobController : MonoBehaviour
{
    [HideInInspector] public BathMobHand hand;
    [HideInInspector] public BathMobMovement movement;
    [HideInInspector] public BathMobData data;

    public Water water;

    public Transform fRod;
    public Transform drawnPos;
    
    //---

    //물 안밖으로 나가는 텀
    public float waterInWaitTime = 2f;
    public float waterOutWaitTime = 4f;
    //빠르게 지나가는 순간
    public int toyIdxToRunWild = 4;

    public PlayerHide playerHide;

    [HideInInspector] public bool isTracingStart = false;
    [HideInInspector] public bool isTryCatchPlayer = false;

    private void Awake()
    {
        hand = GetComponentInChildren<BathMobHand>();
        movement = GetComponent<BathMobMovement>();
        data = GetComponent<BathMobData>();

    }

    private void Start()
    {
        movement.HideMob();
    }

    //모습 보이기

    private void Update()
    {
        if (!hand.isMoveHand && isTracingStart && !hand.isCatchPlayer)
        {
            //몬스터가 물 밖에 있을 때 공격
            if (data.state == BathMobData.State.OutWater)
            {
                if (!playerHide.isHide)
                {
                    hand.MoveHandToToyToAttack();
                }
            }

            //폭주해서 플레이어 잡으러 가기
            if (data.state == BathMobData.State.RuningWild && movement.isStartAttack)
            {
                if (hand.toyList[hand.toyList.Length - 1].gameObject.activeSelf)
                {
                    Debug.LogWarning("runwild");
                    hand.MoveHandToToyWhenRunWild(20);
                }
                else if (isTryCatchPlayer)
                {
                    hand.MoveHandToPlayer(12);
                }
            }
        }
    }

    //폭주해서 쫓아가기
    public void StartRunWild()
    {
        hand.toyIdx = 4;
        movement.isStartRunWild = true;
        data.state = BathMobData.State.RuningWild;
    }

    //처음 등장하기
    public void Appearance()
    {
        data.state = BathMobData.State.OutWater;
        movement.ShowMob();
        movement.MoveOutWater();
    }

    //모습 숨기기
    void Hiding()
    {
        data.state = BathMobData.State.InWater;
        movement.HideMob();
        movement.MoveInWater();
    }

    //움직이기 시작
    public void StartMoving()
    {
        isTracingStart = true;
        StartCoroutine(MoveInOutWater());
    }

    //물 안 밖으로 이동
    IEnumerator MoveInOutWater()
    {
        WaitForSeconds afterInWait = new WaitForSeconds(waterInWaitTime);
        WaitForSeconds afterOutWait = new WaitForSeconds(waterOutWaitTime);

        while (data.state != BathMobData.State.RuningWild)
        {
            movement.MoveInWater();
            if(!movement.isStartRunWild)
                data.state = BathMobData.State.InWater;
            else
            {
                data.state = BathMobData.State.RuningWild;
                break;
            }
            yield return afterInWait;
            StopCoroutine(movement.moveIntoTheWater);

            movement.MoveOutWater();
            if (!movement.isStartRunWild)
                data.state = BathMobData.State.OutWater;
            else
            {
                data.state = BathMobData.State.RuningWild;
                break;
            }
            yield return afterOutWait;
            StopCoroutine(movement.moveOutOfTheWater);
        }
        movement.SetMoveOutWater();
    }
}
