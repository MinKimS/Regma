using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobMovement : MonoBehaviour
{
    public Transform[] movePos;
    int movePosIdx = 0;
    Vector2 curMovePos;
    
    //patrol���� ���� �� �����̴� ��
    public float moveValueX = 5f;
    //mob �����̴� �ӵ�
    public float mobMoveSpeed = 4f;
    public float mobRunWildSpeed = 1f;
    float offset = 0.01f;


    //�� �ȹ����� �������� �̵� ��
    //public float moveOutOfWaterValue = 2f;
    public Transform moveInWaterPos;
    public Transform moveOutWaterPos;

    bool isMoving = false;
    [HideInInspector] public bool isStartRunWild = false;
    [HideInInspector] public bool isStartAttack = false;

    //������ ó�� ��ġ
    public Transform mobInitialPos;

    [HideInInspector] public IEnumerator moveOutOfTheWater;
    [HideInInspector] public IEnumerator moveIntoTheWater;

    //private void Start()
    //{
    //    transform.position = new Vector2(movePos[0].position.x, transform.position.y);
    //    curMovePos = transform.position;
    //}

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

    //���� ���� ��ġ ����
    public void SetMobPosInitialPos()
    {
        transform.position = mobInitialPos.position;
    }
    //���Ͱ� ����� ����
    public void ShowMob()
    {
        gameObject.SetActive(true);
    }
    //���Ͱ� ����� ����
    public void HideMob()
    {
        gameObject.SetActive(false);
    }

    //���Ͱ� �� ������ �̵�
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

    //���Ͱ� �� ������ �̵�
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

        curMovePos = movePos[movePosIdx].position;

        //���� �÷��̾ ������ �� �ִ� ��ġ�� �̵�
        while (transform.position.x <= curMovePos.x - offset)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(curMovePos.x, transform.position.y), mobMoveSpeed * Time.deltaTime);
            yield return null;
        }

        if (movePos.Length - 1 != movePosIdx)
        {
            movePosIdx++;
        }
        isMoving = false;
    }

    //�� ������ ������
    //public IEnumerator GoOutOfTheWater()
    //{
    //    Vector2 targetPos = new Vector2(transform.position.x, moveOutWaterPos.position.y + 1.5f);
    //    while (transform.position.y <= targetPos.y - 0.01f)
    //    {
    //        if (bmc.IsMobInWater) { StopCoroutine(GoOutOfTheWater());}
    //        transform.position = Vector2.MoveTowards(transform.position, targetPos, 17f*Time.deltaTime);
    //        yield return null;
    //    }

    //    targetPos = new Vector2(transform.position.x, transform.position.y - 1.5f);
    //    while (transform.position.y >= targetPos.y + 0.01f)
    //    {
    //        if (bmc.IsMobInWater) { StopCoroutine(GoOutOfTheWater());}
    //        transform.position = Vector2.Lerp(transform.position, targetPos, 0.01f);
    //        yield return null;
    //    }
    //}

    //�� ������ ����
    //public IEnumerator GoIntoTheWater()
    //{
    //    Vector2 targetPos = new Vector2(transform.position.x, moveInWaterPos.position.y);
    //    while (transform.position.y >= targetPos.y + 0.01f)
    //    {
    //        if (!bmc.IsMobInWater) { StopCoroutine(GoIntoTheWater()); }
    //        transform.position = Vector2.MoveTowards(transform.position, targetPos, 17f * Time.deltaTime);
    //        yield return null;
    //    }

    //    transform.position = new Vector2(curMovePos.x, transform.position.y);
    //}

    //���ô븦 ���� ��ġ�� �̵�
    //public IEnumerator SeeingFishingRod()
    //{
    //    yield return new WaitForSeconds(1f);
    //    transform.position = seeFishingRodPos.position;
    //    transform.rotation = Quaternion.Euler(180f, 0f, 0f);

    //    ////�ణ�� ������
    //    //while (!bmc.IsMobStuck && !bmc.IsMobTryCatch)
    //    //{
    //    //    while (Vector2.Distance(transform.position, seeFishingRodPos.position + Vector3.up) > 0.1f)
    //    //    {
    //    //        transform.position = Vector2.Lerp(transform.position, seeFishingRodPos.position + Vector3.up, 0.005f);
    //    //        print("up");
    //    //        yield return null;
    //    //    }

    //    //    while (Vector2.Distance(transform.position, seeFishingRodPos.position + Vector3.down) > 0.1f)
    //    //    {
    //    //        transform.position = Vector2.Lerp(transform.position, seeFishingRodPos.position + Vector3.down, 0.005f);
    //    //        print("down");
    //    //        yield return null;
    //    //    }
    //    //}
    //}

    //public void MoveDownDeepIntoWater()
    //{
    //    print("into water");
    //}
}
