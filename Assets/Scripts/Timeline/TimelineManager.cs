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
    Animator playerAnim;
    public Animator cutSceneAppearence;

    public enum TlState
    {
        Play,
        Stop,
        Resume,
        End,
    }
    TlState tlstate = TlState.End;

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
    private void Start() {
        SceneManager.sceneLoaded += LoadSceneEvent;

        playerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();

        SetPlayableDirector();
        SetTimelineStart(0);
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
    }

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "LoadingScene")
        {
            SetPlayableDirector();
            SetTimelineStart(0);
        }
    }

    public void SetPlayableDirector()
    {
        pd = GameObject.Find("Timeline").GetComponentsInChildren<PlayableDirector>();
    }

    public void SetTimelineStart(string timelineName)
    {
        cutSceneAppearence.SetBool("isRunCutScene", true);
        if(!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing"))
        {
            playerAnim.SetBool("walk", false);
            playerAnim.SetBool("jump", false);
        }

        int playTimelineIdx = -1;
        for(int i = 0; i<pd.Length; i++)
        {
            if(pd[i].name == timelineName)
            {
                playTimelineIdx = i;
                break;
            }
        }
        if(playTimelineIdx != -1) { curPD = playTimelineIdx; }
        pd[curPD].Play();
        tlstate = TlState.Play;
    }

    public void SetTimelineStart(int playTimelineIdx)
    {
        cutSceneAppearence.SetBool("isRunCutScene", true);
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
    public void SetTimelineEnd()
    {
        cutSceneAppearence.SetBool("isRunCutScene", false);
        pd[curPD].Stop();
        tlstate = TlState.End;
        print(tlstate);
    }
}
