using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobMovement : MonoBehaviour
{
    BathMobController bmc;

    public Transform[] movePos;
    int movePosIdx = 0;
    Vector2 curMovePos;
    
    //patrol���� ���� �� �����̴� ��
    public float moveValueX = 5f;

    float offset = 0.01f;


    //�� �ȹ����� �������� �̵� ��
    public float moveOutOfWaterValue = 8f;


    bool isMoving = false;

    //���ô븦 ���� ��ġ
    public Vector2 seeFishingRodPos;

    private void Awake()
    {
        bmc = GetComponentInParent<BathMobController>();
    }

    private void Start()
    {
        curMovePos = transform.position;
    }

    private void Update()
    {
        if (!bmc.IsMobSeeFishingRod && !isMoving && bmc.PlayerPos.position.x > movePos[movePosIdx].position.x)
        {
            StartCoroutine(MoveNextPos());
        }
    }

    public IEnumerator MoveNextPos()
    {
        isMoving = true;

        curMovePos = movePos[movePosIdx].position;

        //���� �÷��̾ ������ �� �ִ� ��ġ�� �̵�
        while (transform.position.x <= curMovePos.x - offset)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(curMovePos.x, transform.position.y), 2f*Time.deltaTime);
            yield return null;
        }

        movePosIdx++;
        isMoving = false;
    }

    //�� ������ ������
    public IEnumerator GoOutOfTheWater()
    {
        Vector2 targetPos = new Vector2(transform.position.x, transform.position.y + moveOutOfWaterValue + 1.5f);
        while (transform.position.y <= targetPos.y - 0.01f)
        {
            if (bmc.IsMobInWater) { StopCoroutine(GoOutOfTheWater());}
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 17f*Time.deltaTime);
            yield return null;
        }

        targetPos = new Vector2(transform.position.x, transform.position.y - 1.5f);
        while (transform.position.y >= targetPos.y + 0.01f)
        {
            if (bmc.IsMobInWater) { StopCoroutine(GoOutOfTheWater());}
            transform.position = Vector2.Lerp(transform.position, targetPos, 0.01f);
            yield return null;
        }
    }

    //�� ������ ����
    public IEnumerator GoIntoTheWater()
    {
        Vector2 targetPos = new Vector2(transform.position.x, transform.position.y - moveOutOfWaterValue);
        while (transform.position.y >= targetPos.y + 0.01f)
        {
            if (!bmc.IsMobInWater) { StopCoroutine(GoIntoTheWater()); }
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 17f * Time.deltaTime);
            yield return null;
        }

        transform.position = new Vector2(curMovePos.x, transform.position.y);
    }

    //���ô븦 ���� ��ġ�� �̵�
    public IEnumerator SeeingFishingRod()
    {
        transform.position = seeFishingRodPos;

        //�ణ�� ������
        while (!bmc.IsMobStuck && !bmc.IsMobTryCatch)
        {
            while (Vector2.Distance(transform.position, seeFishingRodPos + Vector2.up) > 0.1f)
            {
                transform.position = Vector2.Lerp(transform.position, seeFishingRodPos + Vector2.up, 0.005f);
                yield return null;
            }

            while (Vector2.Distance(transform.position, seeFishingRodPos + Vector2.down) > 0.1f)
            {
                transform.position = Vector2.Lerp(transform.position, seeFishingRodPos + Vector2.down, 0.005f);
                yield return null;
            }
        }
    }
}
