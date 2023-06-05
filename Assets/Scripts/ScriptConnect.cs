using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptConnect : MonoBehaviour
{
    public void ShowDlgWithPause()
    {
        DialogueManager.instance.PlayDlg();
        TimelineManager.instance.SetTimelinePause();
    }
    public void ShowDlgWithoutPause()
    {
        DialogueManager.instance.PlayDlg();
    }

    public void SetCurTimelineEnd()
    {
        TimelineManager.instance._Tlstate = TimelineManager.TlState.Stop;
    }

    public void PauseTimeline()
    {
        TimelineManager.instance.SetTimelinePause();
    }
    public void LoadingInTimeline(string nextScene)
    {
        LoadingManager.LoadScene(nextScene);
    }

    public void ShowTalk()
    {
        SmartphoneManager.instance.ShowPhone();
    }

    public void AddTalkPlayer(int count)
    {
        SmartphoneManager.instance.SetSendTalk(count);
        SmartphoneManager.instance.isOKSendTalk = true;
    }

    public void AddTalkOther(Speaker user)
    {
        SmartphoneManager.instance.AddTalk(false, user, SmartphoneManager.instance.receiveTalkContentList[SmartphoneManager.instance.receiveTalkIdx][1]);
        SmartphoneManager.instance.receiveTalkIdx++;
    }

    public void AddInOutTalk(string name)
    {
        SmartphoneManager.instance.AddInOutTalk(true, name);
    }

    public void HidePhone()
    {
        SmartphoneManager.instance.HidePhone();
    }

    public void ReduceReadNum()
    {
        SmartphoneManager.instance.lastPlayerTalk.readNum--;
        SmartphoneManager.instance.lastPlayerTalk.readNumText.text = SmartphoneManager.instance.lastPlayerTalk.readNum.ToString();
    }

    public void StartNextTimeline(int num)
    {
        TimelineManager.instance.SetTimelineStart(num);
    }

    public void TestPoint()
    {
        print("timeline");
    }
}
