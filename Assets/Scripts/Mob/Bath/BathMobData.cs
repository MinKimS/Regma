using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobData : MonoBehaviour
{
    public enum State
    {
        None,
        InWater,
        OutWater,
        RuningWild,
        Moving,
    }
    [HideInInspector] public State state = State.InWater;

    bool isMobInWater = true;
    bool isMobTryCatch = false;
    bool isTryCatchPlayer = false;
    bool isMobStuck = false;

    [HideInInspector] public bool canMove = true;
    [HideInInspector]
    public bool isCatchPlayer = false;

    public bool IsMobInWater
    {
        get { return isMobInWater; }
        set { isMobInWater = value; }
    }

    public bool IsMobTryCatch
    {
        get { return isMobTryCatch; }
        set { isMobTryCatch = value; }
    }
    public bool IsTryCatchPlayer
    {
        get { return isTryCatchPlayer; }
        set { isTryCatchPlayer = value; }
    }
    public bool IsMobStuck
    {
        get { return isMobStuck; }
    }
}
