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

    //�� �ȹ����� ������ ��
    public float waterInWaitTime = 2f;
    public float waterOutWaitTime = 4f;
    //������ �������� ����
    public int toyIdxToRunWild = 4;

    public PlayerHide playerHide;

    [HideInInspector] public bool isTracingStart = false;

    //==========================

    public Animator animator;

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

    //��� ���̱�

    private void Update()
    {
        if (isTracingStart && data.IsMobTryCatch && !hand.isMoveHand && !hand.isCatchSomething && !water.isDrwon)
        {
            if(data.state == BathMobData.State.OutWater)
            {
                if(!playerHide.isHide)
                {
                    hand.SetTarget();
                    hand.AttackTarget(1f);
                }
            }
            else if(data.state == BathMobData.State.RuningWild)
            {
                if (!data.IsTryCatchPlayer)
                {
                    hand.SetTargetToy(hand.toyIdx);
                    hand.AttackTarget(1.5f);
                }
                else
                {
                    hand.MoveHandToPlayer(12);
                }
            }
        }
    }

    //�÷��̾� ���� ����
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

    //ó�� �����ϱ�
    public void Appearance()
    {
        movement.ShowMob();
        movement.MoveOutOfTheWater(0.7f);
    }
}
