using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BathMobMovement : MonoBehaviour
{
    public Transform[] movePos;
    
    //mob �����̴� �ӵ�
    public float mobMoveSpeed = 4f;
    public Transform moveInWaterPos;
    public Transform moveOutWaterPos;

    //������ ó�� ��ġ
    public Transform mobInitialPos;

    [HideInInspector] public BathMobData data;

    public Transform lastMovingPos;
    //�� �ȹ����� ������ ��
    public float waterInWaitTime = 2f;
    public float waterOutWaitTime = 4f;

    public Water water;
    //===============================================

    bool isTrace = false;

    private void FixedUpdate()
    {
        if (data.canMove && isTrace && transform.position.x < 32f && !water.isDrwon)
        {
            if (data.state != BathMobData.State.RuningWild)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(PlayerInfoData.instance.playerTr.position.x, transform.position.y), mobMoveSpeed * 0.2f);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(PlayerInfoData.instance.playerTr.position.x, transform.position.y), mobMoveSpeed);
            }
        }
    }

    //===============================================

    //�÷��̾� �����ϱ�
    public void TracingPlayer()
    {
        isTrace = true;
    }

    //�� �۰� ������ �̵�
    public void MoveOutAndInWater()
    {
        StartCoroutine(IEMoveOutAndInWater());
    }
    IEnumerator IEMoveOutAndInWater()
    {
        WaitForSeconds waitAfterInWater = new WaitForSeconds(waterInWaitTime);
        WaitForSeconds waitAfteroutWater = new WaitForSeconds(waterOutWaitTime);

        while (data.state != BathMobData.State.RuningWild)
        {
            //data.IsMobTryCatch = false;
            if (data.state != BathMobData.State.RuningWild)
            {
                MoveIntoTheWater();
                yield return waitAfterInWater;
            }
            if(data.state != BathMobData.State.RuningWild)
            {
                MoveOutOfTheWater(0.4f);
                yield return waitAfteroutWater;
            }
        }
        MoveOutOfTheWater(1);
    }

    //�� ������ �̵�
    void MoveIntoTheWater()
    {
        StartCoroutine(IEMoveIntoTheWater());
    }
    IEnumerator IEMoveIntoTheWater()
    {
        while (Mathf.Abs(transform.position.y - moveInWaterPos.position.y) > 0.02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, moveInWaterPos.position.y), 0.2f);
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, moveInWaterPos.position.y);
        data.state = BathMobData.State.InWater;
    }

    //�� ������ �̵�
    public void MoveOutOfTheWater(float speed)
    {
        AudioManager.instance.SFXPlay("�ֹ�_������ ����", 0.05f, 0.7f);
        StartCoroutine(IEMoveOutOfTheWater(speed));
    }
    IEnumerator IEMoveOutOfTheWater(float speed)
    {
        while (Mathf.Abs(transform.position.y - moveOutWaterPos.position.y) > 0.02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, moveOutWaterPos.position.y), speed);
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, moveOutWaterPos.position.y);

        if (data.state != BathMobData.State.RuningWild)
        {
            data.state = BathMobData.State.OutWater;
        }
        //data.IsMobTryCatch = true;
    }

    //�����ؼ� �÷��̾� �ѱ� ����
    public void StartRunningWild()
    {
        data.state = BathMobData.State.RuningWild;
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
}
