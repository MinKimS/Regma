using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMob : MonoBehaviour
{
    public GameObject mob;
    public Transform summonPos;

    public void Summon()
    {
        print("summon");
        GameObject summonedMob = Instantiate(mob, summonPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Summon();
        }
    }
}
