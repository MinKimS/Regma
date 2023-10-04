using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTimeline : MonoBehaviour
{
    public void PlayDlgTimeline()
    {
        DialogueManager.instance.PlayDlg();
    }

    public void PlayDlgSingleTimeline( Dialogue dlg)
    {
        DialogueManager.instance.PlayDlg(dlg);
    }
}
