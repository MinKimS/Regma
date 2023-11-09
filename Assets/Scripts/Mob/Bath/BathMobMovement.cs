using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BathMobMovement : MonoBehaviour
{
    public Transform[] movePos;
    int movePosIdx = 0;
    Vector2 curMovePos;
    
    //patrol에서 조금 씩 움직이는 양
    public float moveValueX = 5f;
    //mob 움직이는 속도
    public float mobMoveSpeed = 4f;
    public float mobRunWildSpeed = 1f;
    float offset = 0.01f;


    //물 안밖으로 나갈때의 이동 값
    //public float moveOutOfWaterValue = 2f;
    public Transform moveInWaterPos;
    public Transform moveOutWaterPos;

    bool isMoving = false;
    [HideInInspector] public bool isStartAttack = false;

    //몬스터의 처음 위치
    public Transform mobInitialPos;

    [HideInInspector] public IEnumerator moveOutOfTheWater;
    [HideInInspector] public IEnumerator moveIntoTheWater;

    [HideInInspector] public BathMobData data;
    private void Start()
    {
        moveOutOfTheWater = GoOutOfTheWater();
        moveIntoTheWater = GoIntoTheWater();
    }

    public Transform lastMovingPos;
    //물 안밖으로 나가는 텀
    public float waterInWaitTime = 2f;
    public float waterOutWaitTime = 4f;

    //플레이어 추적하기
    public void TracingPlayer()
    {
        StartCoroutine(IETracingPlayer());
    }
    IEnumerator IETracingPlayer()
    {
        while (transform.position.x < lastMovingPos.position.x)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerInfoData.instance.playerTr.position.x, transform.position.y), mobMoveSpeed);
            yield return null;
        }
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
            data.IsMobTryCatch = false;
            MoveIntoTheWater();
            yield return waitAfterInWater;
            MoveOutOfTheWater();
            yield return waitAfteroutWater;
        }
        MoveOutOfTheWater();
    }

    //물 안으로 이동
    void MoveIntoTheWater()
    {
        data.state = BathMobData.State.InWater;
        StartCoroutine(IEMoveIntoTheWater());
    }
    IEnumerator IEMoveIntoTheWater()
    {
        while (Mathf.Abs(transform.position.y - moveInWaterPos.position.y) > 0.02f && data.state == BathMobData.State.InWater)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, moveInWaterPos.position.y), 0.2f);
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, moveInWaterPos.position.y);
    }

    //물 밖으로 이동
    public void MoveOutOfTheWater()
    {
        AudioManager.instance.SFXPlay("주방_개수대 입장", 0.05f, 0.7f);
        if (data.state != BathMobData.State.RuningWild)
        {
            data.state = BathMobData.State.OutWater;
        }
        StartCoroutine(IEMoveOutOfTheWater());
    }
    IEnumerator IEMoveOutOfTheWater()
    {
        while (Mathf.Abs(transform.position.y - moveOutWaterPos.position.y) > 0.02f && data.state == BathMobData.State.OutWater)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, moveOutWaterPos.position.y), 0.2f);
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, moveOutWaterPos.position.y);
        data.IsMobTryCatch = true;
    }

    //폭주해서 플레이어 쫓기 시작
    public void StartRunningWild()
    {
        data.state = BathMobData.State.RuningWild;
    }

    //==================================================================

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

    //몬스터가 물 밖으로 이동
    public void MoveOutWater()
    {
        data.state = BathMobData.State.OutWater;
        StartCoroutine(GoOutOfTheWater());

    }
    public IEnumerator GoOutOfTheWater()
    {
        while (transform.position.y <= moveOutWaterPos.position.y - 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, moveOutWaterPos.position.y + 1.5f), 20f * Time.deltaTime);

            if (data.state == BathMobData.State.RuningWild)
            {
                break;
            }
            yield return null;
        }

        float targetPosY = transform.position.y - 1.5f;
        while (transform.position.y >= targetPosY + 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, targetPosY), 0.01f);

            if (data.state == BathMobData.State.RuningWild)
            {
                break;
            }
            yield return null;
        }
    }
    public void SetMoveOutWater()
    {
        StartCoroutine(SetOutOfTheWater());
    }
    public IEnumerator SetOutOfTheWater()
    {
        while (transform.position.y <= moveOutWaterPos.position.y - 0.03f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, moveOutWaterPos.position.y), 35f * Time.deltaTime);
            Debug.LogError("SETOUTOFTHEWATER");
            yield return null;
        }

        isStartAttack = true;
    }

    //몬스터가 물 안으로 이동
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
            if(data.state == BathMobData.State.RuningWild)
            {
                break;
            }
            yield return null;
        }
    }
}
