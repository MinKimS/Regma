using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobEye : MonoBehaviour
{
    BathMobController bmc;

    Animator anim;
    const string ISLOOKRIGHT = "isLookRight";
    const string ISLOOKMIDDLE = "isLookMiddle";
    byte lookDirNum = 1; // 0 :left 1:middle 2:right
    bool isRolling = false;
    bool isFindPlayer = false;

    public HidingObj[] ho;

    public bool IsFindPlayer
    { get { return isFindPlayer; } }

    Transform playerPos;

    private void Awake()
    {
        bmc = GetComponentInParent<BathMobController>();
        anim = GetComponent<Animator>();
        playerPos = GameObject.FindWithTag("Player").transform;

        for (int i = 0; ho.Length > i; i++)
        {
            ho[i].eye = this;
        }
    }

    private void Update()
    {
        //´« ±¼¸®´Â µ¿¾È ÇÃ·¹ÀÌ¾î ÃßÀû
        if(isRolling && !bmc.IsMobStuck)
        {
            FindingPlayer();
        }
    }

    public void StopRolling()
    {
        anim.SetBool("isRolling", false);
        isRolling = false;
    }

    public IEnumerator RollEye()
    {
        var wait = new WaitForSeconds(2f);
        var sWait = new WaitForSeconds(1f);
        yield return new WaitForSeconds(3f);
        isRolling = true;
        anim.SetBool("isRolling", true);

        //ÁÂ¿ì·Î ´« µ¹¸²
        while ((!bmc.IsMobInWater || bmc.IsMobSeeFishingRod) && !bmc.IsMobStuck)
        {
            isFindPlayer = false;
            anim.SetBool(ISLOOKMIDDLE, false);
            anim.SetBool(ISLOOKRIGHT, true);
            lookDirNum = 2;
            yield return wait;

            isFindPlayer = false;
            anim.SetBool(ISLOOKMIDDLE, true);
            lookDirNum = 1;
            yield return sWait;

            isFindPlayer = false;
            anim.SetBool(ISLOOKMIDDLE, false);
            anim.SetBool(ISLOOKRIGHT, false);
            lookDirNum = 0;
            yield return wait;

            isFindPlayer = false;
            anim.SetBool(ISLOOKMIDDLE, true);
            lookDirNum = 1;
            yield return sWait;
        }
    }

    void FindingPlayer()
    {
        Vector2 playerDir = transform.position - playerPos.position;
        float checkSide = Vector2.Dot(playerDir, transform.right);

        //¿À¸¥ÂÊ
        if ((!bmc.IsMobSeeFishingRod && checkSide < 0) || (bmc.IsMobSeeFishingRod && checkSide > 0))
        {
            if (lookDirNum == 2)
            {
                isFindPlayer = true;
            }
        }
        //¿ÞÂÊ
        else if((!bmc.IsMobSeeFishingRod && checkSide > 0) || (bmc.IsMobSeeFishingRod && checkSide < 0))
        {
            if (lookDirNum == 0)
            {
                isFindPlayer = true;
            }
        }
        else
        {
            if(lookDirNum == 1)
            {
                isFindPlayer = true;
            }
        }
    }
}
