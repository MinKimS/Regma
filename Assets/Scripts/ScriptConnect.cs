using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptConnect : MonoBehaviour
{
    public void ShowDlgWithPause()
    {
        DialogueManager.instance.PlayDlg();
        TimelineManager.instance.timelineController.SetTimelinePause();
    }
    public void ShowDlgWithoutPause()
    {
        DialogueManager.instance.PlayDlg();
    }

    public void LoadingInTimeline(string nextScene)
    {
        LoadingManager.LoadScene(nextScene);
    }

    public void ShowTalk()
    {
        SmartphoneManager.instance.phone.ShowPhone();
    }

    public void AddInOutTalk(string name)
    {
        SmartphoneManager.instance.phone.AddInOutTalk(true, name);
    }

    public void StartNextTimeline(int num)
    {
        TimelineManager.instance.timelineController.SetTimelineStart(num);
    }

    public void TestPoint(string text)
    {
        print(text);
    }
}
