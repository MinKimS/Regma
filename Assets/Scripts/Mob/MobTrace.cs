using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTrace : MonoBehaviour
{
    public Transform traceTarget;
    float disToTarget;
    public float traceRange = 5f;
    public float traceStopRange = 1f;
    public float traceSpeed = 3f;
    bool isTrace = false;

    public bool IsTrace
    {
        get { return isTrace; }
    }

    private void Update()
    {
        disToTarget = (transform.position - traceTarget.position).magnitude;

        //만약 플레이어 숨고 너무 가까이에서 숨지 않으면 추적 중단->아직 안함
        if (disToTarget < traceRange && disToTarget > traceStopRange)
        {
            if(!isTrace) { isTrace  = true; }
            transform.position = Vector3.MoveTowards(transform.position, traceTarget.position, traceSpeed * Time.deltaTime);
        }
        else
        {
            if(isTrace)
            {
                //다시 원래 루트데로 움직이기 전 멈춤
                Invoke("StopTrace", 1.0f);
            }
        }
    }

    void StopTrace()
    {
        isTrace = false;
    }
}
