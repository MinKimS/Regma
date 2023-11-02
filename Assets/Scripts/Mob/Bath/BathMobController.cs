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

    //��� ���̱�

    private void Update()
    {
        if (!hand.isMoveHand && isTracingStart && !hand.isCatchPlayer)
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
