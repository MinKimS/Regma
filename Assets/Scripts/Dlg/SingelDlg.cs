using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingelDlg : MonoBehaviour
{
    public Dialogue dlg;
    public bool isCanDisapear = false;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !SmartphoneManager.instance.notification.isTalkIconShow && !SmartphoneManager.instance.phone.IsOpenPhone
            && !SmartphoneManager.instance.phone.isTalkNeedToBeSend)
        {
            DialogueManager.instance.PlayDlg(dlg);
            if(isCanDisapear) { gameObject.SetActive(false); }
        }
    }
}
