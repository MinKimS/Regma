using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager instance;
    public TimelineController timelineController;

    public enum TlState
    {
        Play,
        Stop,
        Resume,
        End,
    }
    [HideInInspector]
    public TlState tlstate = TlState.End;

    public TlState _Tlstate{
        get{
            return tlstate;
        }
        set{
            tlstate = value;
        }
    }

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

        if(LoadingManager.nextScene == "Ending")
        {
            Destroy(gameObject);
        }
    }
}
