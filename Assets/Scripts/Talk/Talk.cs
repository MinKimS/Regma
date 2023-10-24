using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Talk", menuName = "Smartphone/Talk")]
public class Talk : ScriptableObject {
    public enum AfterEndTalk{
        None,
        StartTimeline,
        SendTalk,
        SendTalkAndRunNextTalk,
        SendTalkAndStartTimeline,
        SendTalkAndResumeTimeline,
        SendTalkAndRunEvent,
        ContinueTimeline,
        RunEvent
    }
    //톡이 끝나고 수행되는 것
    public AfterEndTalk afterEndTalk = AfterEndTalk.None;

    //톡이 끝나고 타임라인이 시작되는 경우 필요한 정보-----
    //시작될 타임라인 이름
    public string timelineName;

    //톡 끝나고 이벤트가 시작되는 경우 필요한 정보-----
    //실행될 이벤트
    public GameEvent runEvent;

    public List<TalkContext> TalkContexts;
    public SendTalk[] answerTalk;
    public Talk[] nextTalk;
    public bool isInTimeline = false;

    [System.Serializable]
    public struct TalkContext{
        public Speaker user;
        [TextArea]
        public string talkText;
        public float TalkSendDelay;
    }
}