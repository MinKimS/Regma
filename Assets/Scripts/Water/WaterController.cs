using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WaterController : MonoBehaviour
{
    SpriteShapeController ssc;

    private void Awake()
    {
        ssc = GetComponent<SpriteShapeController>();
        Vector3 wavePoint = ssc.spline.GetPosition(0);
        print(wavePoint);
        wavePoint = ssc.spline.GetPosition(1);
        print(wavePoint);
        wavePoint = ssc.spline.GetPosition(2);
        print(wavePoint);
    }
}
