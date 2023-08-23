using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public float fRodSpeed = 0.1f;
    public Water water;
    private void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        if(v != 0)
        {
            transform.Translate(Vector2.up*v*fRodSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DrainageHole"))
        {
            print("waterdown");
            water.IsDrainageHoleOpen = true;
        }
    }
}
