using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMapMenuController : MonoBehaviour
{
    public static SceneMapMenuController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }

    public GameObject btnObj;

    public void MoveScene(string sceneName)
    {
        btnObj.SetActive(false);
        GameManager.instance._isMeetBathMob = false;
        ReadyToSceneChange();
        SetInvenNeededItemAtCurrentScene(sceneName);

        LoadingManager.LoadScene(sceneName);
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
            ReadyToSceneChange();
            SetInvenNeededItemAtCurrentScene(scene.name);
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            btnObj.SetActive(!btnObj.activeSelf);
        }

        if(Input.GetKeyDown(KeyCode.F2))
        {
            MoveScene("SampleScene");
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            MoveScene("Kitchen");
        }
        else if(Input.GetKeyDown(KeyCode.F4))
        {
            MoveScene("Bathroom");
        }
        else if(Input.GetKeyDown(KeyCode.F5))
        {
            MoveScene("Bath");
        }
        else if(Input.GetKeyDown(KeyCode.F6))
        {
            MoveScene("SampleScene 2");
        }
        else if(Input.GetKeyDown(KeyCode.F7))
        {
            MoveScene("Veranda");
        }
    }

    public void ReadyToSceneChange()
    {
        if(TimelineManager.instance != null &&
            TimelineManager.instance.timelineController != null &&
            TimelineManager.instance._Tlstate != TimelineManager.TlState.End)
        {
            TimelineManager.instance.timelineController.SetTimelineEnd();
        }
        if(DialogueManager.instance != null && DialogueManager.instance._dlgState != DialogueManager.DlgState.End)
        {
            DialogueManager.instance.DialogueHide();
        }

        AudioManager.instance.StopSFXAll();
        SmartphoneManager.instance.inven.HideInven();
        SmartphoneManager.instance.phone.HidePhone();
        SmartphoneManager.instance.phone.HideSendTalk();
        SmartphoneManager.instance.phone.isOkStartTalk = true;
        TutorialController.instance.CloseTutorialScreen();
    }

    public ItemData[] itemList;

    public void SetEndingRoute(bool isTwo)
    {
        GameManager.instance.SetEndingRoute(isTwo);
    }

    public void SetInvenNeededItemAtCurrentScene(string sceneName)
    {
        //모든 인벤토리의 아이템 삭제
        SmartphoneManager.instance.DeleteAllItemInInven();

        //해당 씬에서 필요한 아이템만 저장
        //담요만 저장
        if (sceneName == "Kitchen" || sceneName == "Bathroom" || sceneName == "Veranda" || sceneName == "Bath")
        {
            SmartphoneManager.instance.SetInvenItem(itemList[0]);
        }
        //오징어, 유리조각 저장
        else if (sceneName == "SampleScene 2")
        {
            SmartphoneManager.instance.SetInvenItem(itemList[0]);
            SmartphoneManager.instance.SetInvenItem(itemList[1]);
            SmartphoneManager.instance.SetInvenItem(itemList[2]);
        }

        SmartphoneManager.instance.phone.DeleteTalkAll();

        //카톡, 대사 설정
        switch (sceneName)
        {
            case "SampleScene":
                SmartphoneManager.instance.phone.SetCurTalk(0);
                DialogueManager.instance.SetCurDlg(0);
                break;
            case "Kitchen":
                SmartphoneManager.instance.phone.SetCurTalk(1);
                DialogueManager.instance.SetCurDlg(1);
                SmartphoneManager.instance.phone.SetRemainTalks(0);
                break;
            case "Bathroom":
                DialogueManager.instance.SetCurDlg(2);
                SmartphoneManager.instance.phone.SetRemainTalks(1);
                break;
            case "Bath":
                DialogueManager.instance.SetCurDlg(3);
                SmartphoneManager.instance.phone.SetRemainTalks(1);
                break;
            case "SampleScene 2":
                SmartphoneManager.instance.phone.SetCurTalk(2);
                DialogueManager.instance.SetCurDlg(4);
                SmartphoneManager.instance.phone.SetRemainTalks(1);
                break;
            case "Veranda":
                SmartphoneManager.instance.phone.SetCurTalk(3);
                DialogueManager.instance.SetCurDlg(5);
                SmartphoneManager.instance.phone.SetRemainTalks(2);
                break;
            case "Ending":
                SmartphoneManager.instance.phone.SetRemainTalks(3);
                break;
            default:
                break;
        }

        GameManager.instance.SetLastScene(SceneManager.GetActiveScene().name);
    }
}
