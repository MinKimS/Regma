using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobRecognizingPlayer : MonoBehaviour
{
    float disToPlayer = float.MaxValue;

    public PlayerHide pHide;

    MoveAlongThePath matp;

    bool isExiting = false;

    private void Awake()
    {
        matp = GetComponent<MoveAlongThePath>();
    }

    private void Update()
    {
        disToPlayer = Vector2.Distance(transform.position, PlayerInfoData.instance.playerTr.position);

        float mapWidth = Camera.main.orthographicSize * Camera.main.aspect;

        //몬스터가 보고있는 상태에서 플레이어가 숨으면 계속 인식
        //몬스터가 보고있지 않은 상태에서 플레이어가 숨으면 인식 불가
        if(pHide != null && pHide.isTryHiding)
        {
            //화면 안에 있을 때 숨으면 계속 인식
            if (disToPlayer < mapWidth)
            {
                if(!pHide.isHide)
                {
                    print("Recog!!!");
                    pHide.isHide = false;
                }
            }
            else
            {
                print("NoRecog!!");
                pHide.isHide = true;
            }
        }

        if(disToPlayer < mapWidth*0.5f)
        {
            //숨어서 
            if (pHide.isHide)
            {
                print("stop tracing");

                matp.IsTrace = false;
                isExiting = true;
            }
            else
            {
                print("tracing");

                matp.IsTrace = true;
                isExiting = false;
            }
        }

        //플레이어를 발견 못해서 다른 곳으로 감
        if(isExiting)
        {
            print("exiting");
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, matp.traceSpeed*Time.deltaTime);

            if(disToPlayer > mapWidth+0.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
