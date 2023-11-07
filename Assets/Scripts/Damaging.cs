using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static RespawnManager;

public class Damaging : MonoBehaviour
{
    Image img;

     int damageCount = 0;
    public int maxDamaging = 3;

    bool isGetDamaging = false;

    public float decreasingTime;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Update()
    {
        //데미지를 입은 상태이면
        if(damageCount>0)
        {
            if (!isGetDamaging)
            {
                isGetDamaging = true;
                StartCoroutine(DecreaseDamage());
            }
        }
    }

    public void Damage()
    {
        Color c = img.color;
        c.a += 0.35f;
        img.color = c;
        damageCount++;

        if(damageCount == maxDamaging)
        {
            RespawnManager.Instance.ChangeUpdatingMethod(ChangeMethod.DamageBased);

            if (RespawnManager.Instance != null && RespawnManager.Instance.currentMethod == RespawnManager.ChangeMethod.DamageBased)
            {

                //RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(transform);
                print("데미지로 전환");
                RespawnManager.Instance.OnGameOver.Invoke();
            }
            


            //if (RespawnManager.Instance != null && RespawnManager.Instance.currentMethod == RespawnManager.ChangeMethod.DamageBased)
            //{
            //    
            //}
        }

    }

    public int GetDamage()
    {
        return damageCount;
    }

    IEnumerator DecreaseDamage()
    {
        while(damageCount > 0)
        {
            float checkTime = 0;
            float startTime = Time.time;

            while (checkTime < decreasingTime)
            {
                checkTime = Time.time - startTime;
                yield return null;
            }

            Color c = img.color;
            c.a -= 0.35f;
            img.color = c;
            damageCount--;
        }
        isGetDamaging = false;
    }
}
