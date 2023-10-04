using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobRecognizingPlayer : MonoBehaviour
{
    float disToPlayer = float.MaxValue;

    [SerializeField] float stopTraceTime;

    public PlayerHide pHide;

    MoveAlongThePath matp;

    bool isExiting = false;

    [SerializeField]
    GameEvent eventWhenMobDisappear;

    private void Awake()
    {
        matp = GetComponent<MoveAlongThePath>();
    }

    private void Update()
    {
        disToPlayer = Vector2.Distance(transform.position, PlayerInfoData.instance.playerTr.position);

        float mapWidth = Camera.main.orthographicSize * Camera.main.aspect;

        //���Ͱ� �����ִ� ���¿��� �÷��̾ ������ ��� �ν�
        //���Ͱ� �������� ���� ���¿��� �÷��̾ ������ �ν� �Ұ�
        if(pHide != null && pHide.isTryHiding)
        {
            //ȭ�� �ȿ� ���� �� ������ ��� �ν�
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
            //��� 
            if (pHide.isHide)
            {
                print("stop tracing");

                matp.IsTrace = false;

                StartCoroutine(WaitBeforeLeaving());
            }
            else
            {
                print("tracing");

                matp.IsTrace = true;
                isExiting = false;
            }
        }

        //�÷��̾ �߰� ���ؼ� �ٸ� ������ ��
        if(isExiting)
        {
            print("exiting");
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, matp.traceSpeed*Time.deltaTime);

            if(disToPlayer > mapWidth+0.1f)
            {
                gameObject.SetActive(false);
            }

            if(eventWhenMobDisappear != null)
            {
                eventWhenMobDisappear.Raise();
            }
        }
    }

    //�÷��̾� ��ó���� ��� ���
    IEnumerator WaitBeforeLeaving()
    {
        float checkTime = 0;
        float startTime = Time.time;

        while(checkTime < stopTraceTime)
        {
            checkTime = Time.time - startTime;
            if (!pHide.isHide)
            {
                break;
            }
            yield return null;
        }

        isExiting = true;
    }
}
