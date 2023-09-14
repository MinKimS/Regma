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
    //mob �����̴� �ӵ�
    public float mobMoveSpeed = 4f;
    float offset = 0.01f;


    //�� �ȹ����� �������� �̵� ��
    //public float moveOutOfWaterValue = 2f;
    public Transform moveInWaterPos;
    public Transform moveOutWaterPos;

    bool isMoving = false;

    //���ô븦 ���� ��ġ
    public Transform seeFishingRodPos;

    private void Awake()
    {
        bmc = GetComponentInParent<BathMobController>();
    }

    private void Start()
    {
        transform.position = new Vector2(movePos[0].position.x, transform.position.y);
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
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(curMovePos.x, transform.position.y), mobMoveSpeed * Time.deltaTime);

            //���ô븦 ���� ��� ������ ����
            if(bmc.IsMobSeeFishingRod)
            {
                StopCoroutine(MoveNextPos());
            }
            yield return null;
        }

        if(movePos.Length-1 != movePosIdx)
        {
            movePosIdx++;
        }
        isMoving = false;
    }

    //�� ������ ������
    public IEnumerator GoOutOfTheWater()
    {
        Vector2 targetPos = new Vector2(transform.position.x, moveOutWaterPos.position.y + 1.5f);
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
        Vector2 targetPos = new Vector2(transform.position.x, moveInWaterPos.position.y);
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
        yield return new WaitForSeconds(1f);
        transform.position = seeFishingRodPos.position;
        transform.rotation = Quaternion.Euler(180f, 0f, 0f);

        ////�ణ�� ������
        //while (!bmc.IsMobStuck && !bmc.IsMobTryCatch)
        //{
        //    while (Vector2.Distance(transform.position, seeFishingRodPos.position + Vector3.up) > 0.1f)
        //    {
        //        transform.position = Vector2.Lerp(transform.position, seeFishingRodPos.position + Vector3.up, 0.005f);
        //        print("up");
        //        yield return null;
        //    }

        //    while (Vector2.Distance(transform.position, seeFishingRodPos.position + Vector3.down) > 0.1f)
        //    {
        //        transform.position = Vector2.Lerp(transform.position, seeFishingRodPos.position + Vector3.down, 0.005f);
        //        print("down");
        //        yield return null;
        //    }
        //}
    }

    public void MoveDownDeepIntoWater()
    {
        print("into water");
    }
}
