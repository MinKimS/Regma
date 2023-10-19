using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager instance;
    public TimelineController timelineController;
    //Animator playerAnim;
    //public Animator cutSceneAppearence;

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
    }

    //For test
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            LoadingManager.LoadScene("SampleScene");
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            LoadingManager.LoadScene("Kitchen");
        }
        if(Input.GetKeyDown(KeyCode.F4))
        {
            LoadingManager.LoadScene("Bathroom");
        }
        if(Input.GetKeyDown(KeyCode.F5))
        {
            LoadingManager.LoadScene("Bath");
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            LoadingManager.LoadScene("SampleScene 2");
        }
    }
}
