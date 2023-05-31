using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager instance;
    public PlayableDirector[] pd;
    private int curPD = 0;

    public enum TlState
    {
        Play,
        Stop,
    }
    TlState tlstate = TlState.Stop;

    public TlState _Tlstate{
        get{
            return tlstate;
        }
    }
    public int CurPD{
        get{
            return curPD;
        }
    }

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }
    private void Start() {
        pd = GameObject.Find("Timeline").GetComponentsInChildren<PlayableDirector>();

        //test
        SetTimelineStart(0);
    }

    public void SetTimelineStart(int playTimelineIdx=0)
    {
        curPD = playTimelineIdx;
        pd[curPD].Play();
        tlstate = TlState.Play;
    }

    public void SetTimelinePause()
    {
        pd[curPD].Pause();
        tlstate = TlState.Stop;
    }

    public void SetTimelineResume()
    {
        pd[curPD].Resume();
        tlstate = TlState.Play;
    }

    //---------for test
    public void TestTimeline()
    {
        print("ahhhhh");
    }
}
