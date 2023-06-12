using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager instance;
    public PlayableDirector[] pd;
    private int curPD = 0;

    public enum TlState
    {
        Play,
        Stop,
        Resume,
    }
    TlState tlstate = TlState.Stop;

    public TlState _Tlstate{
        get{
            return tlstate;
        }
        set{
            tlstate = value;
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
        SetPlayableDirector();
        SceneManager.sceneLoaded += LoadSceneEvent;
        
        //test
        SetTimelineStart(0);
    }

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "LoadingScene")
        {
            SetTimelineStart(0);
        }
    }

    public void SetPlayableDirector()
    {
        pd = GameObject.Find("Timeline").GetComponentsInChildren<PlayableDirector>();
    }

    public void SetTimelineStart(int playTimelineIdx=0)
    {
        curPD = playTimelineIdx;
        pd[curPD].Play();
        tlstate = TlState.Play;
    }

    public void SetTimelinePause()
    {
        if(tlstate != TlState.Resume)
        {
            pd[curPD].Pause();
            tlstate = TlState.Stop;
        }
        else
        {
            tlstate = TlState.Play;
        }
    }

    public void SetTimelineResume()
    {
        pd[curPD].Resume();
        tlstate = TlState.Play;
    }
}
