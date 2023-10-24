using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPhoneTalk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SmartphoneManager.instance.phone.StartTalkInTrigger();

            if (SmartphoneManager.instance.phone.isOkStartTalk)
            {
                SmartphoneManager.instance.phone.isOkStartTalk = false;
                Destroy(gameObject);
            }
        }
    }
}
