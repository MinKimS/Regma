using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public Animator cutSceneAppearence;
    public PlayableDirector[] pd;
    [HideInInspector]
    public int curPD = 0;

    SmartPhoneTimeline phone;
    DialogueTimeline dlg;


    private void Start()
    {
        TimelineManager.instance.timelineController = this;
        SetTimelineStart(0);
        phone = GetComponent<SmartPhoneTimeline>();
        dlg = GetComponent<DialogueTimeline>();
    }

    //public void SetPlayableDirector()
    //{
    //    pd = GetComponentsInChildren<PlayableDirector>();
    //}
    public void SetTimelineStart(string timelineName)
    {
        cutSceneAppearence.SetBool("isRunCutScene", true);
        if (!PlayerInfoData.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing"))
        {
            PlayerInfoData.instance.playerAnim.SetBool("walk", false);
            PlayerInfoData.instance.playerAnim.SetBool("jump", false);
        }

        int playTimelineIdx = -1;
        for (int i = 0; i < pd.Length; i++)
        {
            if (pd[i].name == timelineName)
            {
                playTimelineIdx = i;
                break;
            }
        }
        if (playTimelineIdx != -1) { curPD = playTimelineIdx; }
        pd[curPD].Play();
        TimelineManager.instance.tlstate = TimelineManager.TlState.Play;
    }

    public void SetTimelineStart(int playTimelineIdx)
    {
        cutSceneAppearence.SetBool("isRunCutScene", true);
        curPD = playTimelineIdx;
        pd[curPD].Play();
        TimelineManager.instance.tlstate = TimelineManager.TlState.Play;
    }

    public void SetTimelinePause()
    {
        if (TimelineManager.instance.tlstate != TimelineManager.TlState.Resume)
        {
            pd[curPD].Pause();
            TimelineManager.instance.tlstate = TimelineManager.TlState.Stop;
        }
        else
        {
            TimelineManager.instance.tlstate = TimelineManager.TlState.Play;
        }
    }

    public void SetTimelineResume()
    {
        pd[curPD].Resume();
        TimelineManager.instance.tlstate = TimelineManager.TlState.Play;
    }
    public void SetTimelineEnd()
    {
        cutSceneAppearence.SetBool("isRunCutScene", false);
        pd[curPD].Stop();
        TimelineManager.instance.tlstate = TimelineManager.TlState.End;
    }
}
