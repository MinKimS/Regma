using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public float fRodSpeed = 0.1f;
    public Water water;
    bool isCaught = false;

    public bool IsCaught
    {
        set { isCaught = value; }
    }

    private void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        if(!isCaught && v != 0)
        {
            transform.Translate(Vector2.up*v*fRodSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isCaught && collision.CompareTag("DrainageHole"))
        {
            print("waterdown");
            water.IsDrainageHoleOpen = true;
        }
    }
}
