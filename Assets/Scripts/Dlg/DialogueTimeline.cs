using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTimeline : MonoBehaviour
{
    public void PlayDlgTimeline()
    {
        DialogueManager.instance.PlayDlg();
        TimelineManager.instance.timelineController.SetTimelinePause();
    }

    public void PlayDlgSingleTimeline( Dialogue dlg)
    {
        DialogueManager.instance.PlayDlg(dlg);
        TimelineManager.instance.timelineController.SetTimelinePause();
    }
}
