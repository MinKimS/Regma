using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkController : MonoBehaviour
{

    //톡 시작
    public void StartTalk()
    {

        StartCoroutine(OutputOtherUserTalk());
    }

    //톡 출력
    IEnumerator OutputOtherUserTalk()
    {
        int talkIdx = SmartphoneManager.instance.phone.talkIdx;
        Talk curTalk = SmartphoneManager.instance.phone.curTalk;
        float delayTime = 0;

        SmartphoneManager.instance.notification.SetShowTalkIconState();

        //상대방 대화 출력
        while(curTalk.TalkContexts.Count > talkIdx)
        {
            delayTime = curTalk.TalkContexts[talkIdx].TalkSendDelay;
            yield return new WaitForSeconds(delayTime);
            SmartphoneManager.instance.phone.AddTalk(false, curTalk.TalkContexts[talkIdx].user, curTalk.TalkContexts[talkIdx++].talkText);
        }
        
        yield return new WaitForSeconds(1.0f);
        
        //모든 상대방의 대화가 나오고 난 뒤 수행되는 것
        if(curTalk.afterEndTalk == Talk.AfterEndTalk.StartTimeline)
        {
            delayTime = curTalk.TalkContexts[talkIdx-1].TalkSendDelay;
            yield return new WaitForSeconds(1.0f);
            TimelineManager.instance.timelineController.SetTimelineStart(curTalk.timelineName);
        }
        if(curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalk || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndStartTimeline || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndRunEvent)
        {
            if(curTalk.isInTimeline) {TimelineManager.instance.timelineController.SetTimelinePause();}
            yield return new WaitForSeconds(1.0f);
            SmartphoneManager.instance.phone.SetSendTalk(curTalk.answerTalk);
        }
        if(curTalk.afterEndTalk == Talk.AfterEndTalk.ContinueTimeline)
        {
            TimelineManager.instance.timelineController.SetTimelineResume();
        }
        
        if(curTalk.answerTalk.Length > 1)
            SmartphoneManager.instance.phone.SetNextTalk();
    }

}
