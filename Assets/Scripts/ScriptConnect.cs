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

    public void LoadingInTimeline(string nextScene)
    {
        LoadingManager.LoadScene(nextScene);
    }

    public void ShowTalk()
    {
        SmartphoneManager.instance.ShowPhone();
    }

    public void AddInOutTalk(string name)
    {
        SmartphoneManager.instance.AddInOutTalk(true, name);
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

    public void TestPoint(string text)
    {
        print(text);
    }
}
