using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkController : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            StartTalk();
        }
    }

    //톡 시작
    public void StartTalk()
    {
        StartCoroutine(OutputOtherUserTalk());
    }

    //톡 출력
    IEnumerator OutputOtherUserTalk()
    {
        int talkIdx = SmartphoneManager.instance.talkIdx;
        Talk curTalk = SmartphoneManager.instance.curTalk;
        float delayTime = 0;
        SmartphoneManager.instance.notification.SetShowTalkIconState();
        while(curTalk.TalkContexts.Count > talkIdx)
        {
            delayTime = curTalk.TalkContexts[talkIdx].TalkSendDelay;
            yield return new WaitForSeconds(delayTime);
            SmartphoneManager.instance.AddTalk(false, curTalk.TalkContexts[talkIdx].user, curTalk.TalkContexts[talkIdx++].talkText);
        }
        
        yield return new WaitForSeconds(1.0f);
        
        if(curTalk.afterEndTalk == Talk.AfterEndTalk.StartTimeline)
        {
            delayTime = curTalk.TalkContexts[talkIdx-1].TalkSendDelay;
            yield return new WaitForSeconds(1.0f);
            TimelineManager.instance.SetTimelineStart(curTalk.timelineName);
        }
        if(curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalk || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndStartTimeline || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndRunEvent)
        {
            if(curTalk.isInTimeline) {TimelineManager.instance.SetTimelinePause();}
            yield return new WaitForSeconds(1.0f);
            SmartphoneManager.instance.SetSendTalk(curTalk.answerTalk);
        }
        if(curTalk.afterEndTalk == Talk.AfterEndTalk.ContinueTimeline)
        {
            TimelineManager.instance.SetTimelineResume();
        }
        
        if(curTalk.answerTalk.Length > 1)
            SmartphoneManager.instance.SetNextTalk();
    }

}
