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

        //���� �÷��̾� ���� �ʹ� �����̿��� ���� ������ ���� �ߴ�->���� ����
        if (disToTarget < traceRange && disToTarget > traceStopRange)
        {
            if(!isTrace) { isTrace  = true; }
            transform.position = Vector3.MoveTowards(transform.position, traceTarget.position, traceSpeed * Time.deltaTime);
        }
        else
        {
            if(isTrace)
            {
                //�ٽ� ���� ��Ʈ���� �����̱� �� ����
                Invoke("StopTrace", 1.0f);
            }
        }
    }

    void StopTrace()
    {
        isTrace = false;
    }
}
