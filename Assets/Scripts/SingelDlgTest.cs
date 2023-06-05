using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingelDlgTest : MonoBehaviour
{
    public Dialogue dlg;
    public bool iscanDisapear = false;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            DialogueManager.instance.PlayDlg(dlg);
            if(iscanDisapear) { gameObject.SetActive(false); }
        }
    }
}
