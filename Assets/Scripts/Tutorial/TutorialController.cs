using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController instance;

    Animator animator;

    bool isTutorialShowing = false;
    public bool IsTutorialShowing
    {
        get { return isTutorialShowing; }
    }

    public TextMeshProUGUI tutorialText;

    Transform TutorialArea;

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

        TutorialArea = GetComponentInChildren<Transform>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        CloseTutorialScreen();
    }

    private void Update()
    {
        if(IsTutorialShowing && Input.GetKeyDown(KeyCode.E))
        {
            CloseTutorialScreen();
        }
    }

    public void OpenTutorialScreen(string str)
    {
        if (!IsTutorialShowing)
        {
            animator.SetBool("isShow", true);
            //TutorialArea.gameObject.SetActive(true);
            isTutorialShowing = true;
            SetTutorialText(str);
        }
    }

    void SetTutorialText(string str)
    {
        tutorialText.text = "";

        tutorialText.text = str;
    }

    void CloseTutorialScreen()
    {
        animator.SetBool("isShow", false);
        //TutorialArea.gameObject.SetActive(false);
        isTutorialShowing = false;
        tutorialText.text = "";
        if(TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
        {
            TimelineManager.instance.timelineController.SetTimelineResume();
        }
    }
}
