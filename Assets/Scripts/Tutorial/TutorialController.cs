using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public static TutorialController instance;

    Animator animator;

    bool isTutorialShowing = false;

    public GameObject backgroundPanel;
    public bool IsTutorialShowing
    {
        get { return isTutorialShowing; }
    }

    public TextMeshProUGUI tutorialText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        if (LoadingManager.nextScene == "Ending")
        {
            Destroy(gameObject);
        }

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        CloseTutorialScreen();
    }
    private void OnEnable()
    {
        //이거 켜져 있으면 각각 씬에서 대사 테스트 안됨
        SceneManager.sceneLoaded += LoadSceneEvent;
    }
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        CloseTutorialScreen();
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    private void Update()
    {
        if(IsTutorialShowing && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("isShow", false);
            backgroundPanel.SetActive(false);
            isTutorialShowing = false;
            tutorialText.text = "";
        }
    }

    public void OpenTutorialScreen(string str)
    {
        if (!IsTutorialShowing)
        {
            animator.SetBool("isShow", true);
            isTutorialShowing = true;
            backgroundPanel.SetActive(true);
            SetTutorialText(str);
        }
    }

    void SetTutorialText(string str)
    {
        tutorialText.text = "";

        tutorialText.text = str;
    }

    public void CloseTutorialScreen()
    {
        animator.SetBool("isShow", false);
        backgroundPanel.SetActive(false);
        isTutorialShowing = false;
        tutorialText.text = "";
        if(TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
        {
            TimelineManager.instance.timelineController.SetTimelineResume();
        }
    }
}
