using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobTrace : MonoBehaviour
{
    MoveAlongThePath matp;
    PathFinder pf;
    public float traceRange = 5f;
    public float traceStopRange = 10f;
    float disToTarget;
    Transform traceTarget;
    public enum MoveWay{
        None,
        RangeTrace,
        Trace,
        Follow
    }
    public MoveWay way = MoveWay.None;

    private void Start()
    {
        matp = GetComponent<MoveAlongThePath>();
        pf = GetComponent<PathFinder>();
        traceTarget = pf.targetPos;
    }

    private void Update()
    {
        //trace player
        if (way == MoveWay.RangeTrace)
        {
            disToTarget = (transform.position - traceTarget.position).magnitude;

            if (disToTarget < traceRange)
            {
                matp.IsTrace = true;
            }
            else
            {
                if (matp.IsTrace && disToTarget > traceStopRange)
                {
                    matp.IsTrace = false;
                }
            }
        }
        ////follow path and go destination
        //if(way == MoveWay.Follow)
        //{
        //    matp.isFollow = true;
        //}
    }
}
