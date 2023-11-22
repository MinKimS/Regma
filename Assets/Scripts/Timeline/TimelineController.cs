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


    private void Start()
    {
        TimelineManager.instance.timelineController = this;
        StartCoroutine(IESetTimeline());
    }

    IEnumerator IESetTimeline()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name != "LoadingScene");

        if (SceneManager.GetActiveScene().name != "Veranda" && SceneManager.GetActiveScene().name != "Bath")
        {
            SetTimelineStart(0);
        }
    }

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

    public void SetCutScene(bool value)
    {
        cutSceneAppearence.SetBool("isRunCutScene", value);
    }

    public void SetTimelineStart(int playTimelineIdx)
    {
        cutSceneAppearence.SetBool("isRunCutScene", true);
        if (pd[curPD].state != PlayState.Playing)
        {
            curPD = playTimelineIdx;
            pd[curPD].Play();

            if (pd[curPD].state == PlayState.Playing)
                TimelineManager.instance.tlstate = TimelineManager.TlState.Play;
        }
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

    public void MoveScene(string sceneName)
    {
        LoadingManager.LoadScene(sceneName);
    }

    public void SetTimelineView(bool value)
    {
        cutSceneAppearence.SetBool("isRunCutScene", value);
    }
}
