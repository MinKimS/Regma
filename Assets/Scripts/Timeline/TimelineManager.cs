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
    private void OnEnable()
    {
        //이거 켜져 있으면 각각 씬에서 대사 테스트 안됨
        SceneManager.sceneLoaded += LoadSceneEvent;
    }
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoadingScene" && scene.name != "Title" && scene.name != "Intro" && scene != null)
        {
            _Tlstate = TlState.End;
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }
}
