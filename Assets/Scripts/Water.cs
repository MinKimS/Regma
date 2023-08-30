using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterDes;
    public float drainSpeed = 0.5f;
    bool isDrainageHoleOpen = false;
    bool isGameOverWaterLevel = false;

    public bool IsDrainageHoleOpen
    {
        get { return  isDrainageHoleOpen; }
        set { isDrainageHoleOpen = value; }
    }
    public bool IsGameOverWaterLevel
    {
        get { return isGameOverWaterLevel; }
    }
    public float gmOverWaterLevel;

    void Update()
    {
        //물 빠지기
        if(isDrainageHoleOpen)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, waterDes), drainSpeed*Time.deltaTime);
        }

        if(transform.position.y < gmOverWaterLevel)
        {
            print("over");
            isGameOverWaterLevel = true;

        }
    }
}
