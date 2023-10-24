using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RecogEvent : MonoBehaviour
{
    public UnityEvent ActiveRecogEvent;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !SmartphoneManager.instance.notification.isTalkIconShow && !SmartphoneManager.instance.phone.IsOpenPhone 
            && !SmartphoneManager.instance.phone.isTalkNeedToBeSend)
        {
            ActiveEvent();
        }
    }

    private void ActiveEvent()
    {
        ActiveRecogEvent.Invoke();
        gameObject.SetActive(false);
    }
}
