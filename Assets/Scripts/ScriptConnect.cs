using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptConnect : MonoBehaviour
{
    private void Start() {
        //TimelineManager.instance.SetTimelineStart(0);
    }
    public void ShowDlgWithPause()
    {
        DialogueManager.instance.PlayDlg();
        TimelineManager.instance.SetTimelinePause();
    }
    public void ShowDlgWithoutPause()
    {
        DialogueManager.instance.PlayDlg();
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

    public void AddTalkOther()
    {
        SmartphoneManager.instance.AddTalk(false, SmartphoneManager.instance.receiveTalkContentList[SmartphoneManager.instance.receiveTalkIdx][0], SmartphoneManager.instance.receiveTalkContentList[SmartphoneManager.instance.receiveTalkIdx][1], null);
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
        print("reduce");
    }

    public void StartNextTimeline(int num)
    {
        print("test");
        TimelineManager.instance.SetTimelineStart(num);
    }
    public void AddTalksWithDelay(int talkCount)
    {
        StartCoroutine(AddTalks(talkCount));
    }

    IEnumerator AddTalks(int count)
    {
        for(int i = 0; i<count; i++)
        {
            SmartphoneManager.instance.AddTalk(false, SmartphoneManager.instance.receiveTalkContentList[SmartphoneManager.instance.receiveTalkIdx][0], SmartphoneManager.instance.receiveTalkContentList[SmartphoneManager.instance.receiveTalkIdx][1], null);
            SmartphoneManager.instance.receiveTalkIdx++;
            yield return new WaitForSeconds(1f);
        }

        AddTalkPlayer(1);
    }

    public void TestPoint()
    {
        print("timeline");
    }
}
