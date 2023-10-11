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
    public float waterInOutWaitTime = 2f;
    //������ �������� ����
    public int toyIdxToRunWild = 4;

    PlayerHide playerHide;

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
        playerHide = PlayerInfoData.instance.playerTr.GetComponent<PlayerHide>();
        movement.HideMob();
    }

    //��� ���̱�

    private void Update()
    {
        if (!hand.isMoveHand && isTracingStart)
        {
            //���Ͱ� �� �ۿ� ���� �� ����
            if (data.state == BathMobData.State.OutWater)
            {
                if (!playerHide.isHide)
                {
                    hand.MoveHandToToyToAttack();
                }
            }

            //�����ؼ� �÷��̾� ������ ����
            if (data.state == BathMobData.State.RuningWild && movement.isStartAttack)
            {
                if (isTryCatchPlayer)
                {
                    hand.MoveHandToPlayer(8);
                }
                else if (hand.toyList[hand.toyList.Length - 1].gameObject.activeSelf)
                {
                    hand.MoveHandToToy(hand.moveSpeed * 0.8f);
                }
            }
        }
    }

    //�����ؼ� �Ѿư���
    public void StartRunWild()
    {
        hand.toyIdx = 4;
        movement.isStartRunWild = true;
        data.state = BathMobData.State.RuningWild;
    }

    //ó�� �����ϱ�
    public void Appearance()
    {
        data.state = BathMobData.State.OutWater;
        movement.ShowMob();
        movement.MoveOutWater();
    }

    //��� �����
    void Hiding()
    {
        data.state = BathMobData.State.InWater;
        movement.HideMob();
        movement.MoveInWater();
    }

    //�����̱� ����
    public void StartMoving()
    {
        isTracingStart = true;
        StartCoroutine(MoveInOutWater());
    }

    //�� �� ������ �̵�
    IEnumerator MoveInOutWater()
    {
        WaitForSeconds wait = new WaitForSeconds(waterInOutWaitTime);

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
            yield return wait;
            StopCoroutine(movement.moveIntoTheWater);

            movement.MoveOutWater();
            if (!movement.isStartRunWild)
                data.state = BathMobData.State.OutWater;
            else
            {
                data.state = BathMobData.State.RuningWild;
                break;
            }
            yield return wait;
            StopCoroutine(movement.moveOutOfTheWater);
        }
        movement.SetMoveOutWater();
    }
}
