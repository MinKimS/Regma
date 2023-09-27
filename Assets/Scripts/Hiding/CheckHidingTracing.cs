using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHidingTracing : MonoBehaviour
{
    //¸÷
    public Transform[] mob;
    [HideInInspector]
    public Transform blockObj;

    int activeMobIdx = 0;

    bool isFindTracingMob = false;

    private void Update()
    {
        //ÇÃ·¹ÀÌ¾î¸¦ ÂÑ´Â ¸÷ È®ÀÎ
        if(!isFindTracingMob)
        {
            for (int i = 0; i < mob.Length; i++)
            {
                if (mob[i] != null && mob[i].gameObject.activeSelf)
                {
                    activeMobIdx = i;
                    isFindTracingMob=true;
                    break;
                }
            }
        }

        if(isFindTracingMob && !mob[activeMobIdx].gameObject.activeSelf)
        {
            blockObj.gameObject.SetActive(true);
            isFindTracingMob = false;
        }
    }
}
