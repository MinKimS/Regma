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
    }
    [HideInInspector] public State state = State.InWater;

    bool isMobAppear = false;
    bool isMobInWater = true;
    bool isMobTryCatch = false;
    bool isMobSeeFishingRod = false;
    bool isMobStuck = false;
    [HideInInspector]
    public bool isCatchPlayer = false;
    bool isPlayerBeDrawn = false;

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
    public bool IsMobSeeFishingRod
    {
        get { return isMobSeeFishingRod; }
    }
    public bool IsMobStuck
    {
        get { return isMobStuck; }
    }
}
