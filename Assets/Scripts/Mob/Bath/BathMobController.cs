using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
    [HideInInspector] public bool isStop = false;

    public Animator animator;
    //==========================


    private void Awake()
    {
        hand = GetComponentInChildren<BathMobHand>();
        movement = GetComponent<BathMobMovement>();
        data = GetComponent<BathMobData>();
        animator = GetComponent<Animator>();

        movement.data = this.data;
        hand.data = this.data;
    }

    private void Start()
    {
        movement.HideMob();
    }

    private void Update()
    {
        if (!isStop && isTracingStart && !hand.isMoveHand && !hand.isCatchSomething && !water.isDrwon)
        {
            if (data.state == BathMobData.State.OutWater)
            {
                if (!playerHide.isHide)
                {
                    hand.SetTarget();
                    hand.AttackTarget(0.4f, 0.5f);
                }
            }
            else if(data.state == BathMobData.State.RuningWild)
            {
                if(!hand.isCatchSomething && hand.toyIdx < hand.toyList.Length)
                {
                    print("runwilddddddd");
                    hand.SetTargetToy(hand.toyIdx);
                    hand.AttackTarget(0.4f, 0.5f);
                    data.canMove = false;
                }
            }
        }
    }

    //플레이어 추적 시작
    public void StartTracingPlayer()
    {
        movement.TracingPlayer();
        movement.MoveOutAndInWater();
        SetStartMovingAnim();
        isTracingStart = true;
    }

    void SetStartMovingAnim()
    {
        animator.SetTrigger("Moving");
    }

    //처음 등장하기
    public void Appearance()
    {
        movement.ShowMob();
        movement.MoveOutOfTheWater(0.7f);
    }
}
