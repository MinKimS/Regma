using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterDes;
    public float drainSpeed = 0.5f;
    bool isDrainageHoleOpen = false;

    public bool IsDrainageHoleOpen
    {
        set { isDrainageHoleOpen = value; }
    }
    public float gmOverWaterLevel;

    void Update()
    {
        //물 빠지기
        if(isDrainageHoleOpen)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, waterDes), drainSpeed);
        }

        if(transform.position.y < gmOverWaterLevel)
        {
            print("over");
        }
    }
}
