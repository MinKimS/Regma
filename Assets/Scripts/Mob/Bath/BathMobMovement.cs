using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BathMobMovement : MonoBehaviour
{
    public Transform[] movePos;
    
    //mob 움직이는 속도
    public float mobMoveSpeed = 4f;
    public Transform moveInWaterPos;
    public Transform moveOutWaterPos;

    //몬스터의 처음 위치
    public Transform mobInitialPos;

    [HideInInspector] public BathMobData data;

    public Transform lastMovingPos;
    //물 안밖으로 나가는 텀
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

    //플레이어 추적하기
    public void TracingPlayer()
    {
        isTrace = true;
    }

    //물 밖과 안으로 이동
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

    //물 안으로 이동
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

    //물 밖으로 이동
    public void MoveOutOfTheWater(float speed)
    {
        AudioManager.instance.SFXPlay("주방_개수대 입장", 0.05f, 0.7f);
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

    //폭주해서 플레이어 쫓기 시작
    public void StartRunningWild()
    {
        data.state = BathMobData.State.RuningWild;
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
}
