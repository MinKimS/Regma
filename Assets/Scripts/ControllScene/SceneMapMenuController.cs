using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMapMenuController : MonoBehaviour
{

    private void Awake()
    {
        var obj = FindObjectsOfType<SceneMapMenuController>();
        if (obj.Length==1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject btnObj;

    public void MoveScene(string sceneName)
    {
        btnObj.SetActive(false);

        ReadyToSceneChange();
        SetInvenNeededItemAtCurrentScene(sceneName);

        LoadingManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            btnObj.SetActive(!btnObj.activeSelf);
        }
    }

    void ReadyToSceneChange()
    {
        if(TimelineManager.instance._Tlstate != TimelineManager.TlState.End)
        {
            TimelineManager.instance.timelineController.SetTimelineEnd();
        }
        if(DialogueManager.instance._dlgState != DialogueManager.DlgState.End)
        {
            DialogueManager.instance.DialogueHide();
        }

        SmartphoneManager.instance.inven.HideInven();
        SmartphoneManager.instance.phone.HidePhone();
    }

    public ItemData[] itemList;

    void SetInvenNeededItemAtCurrentScene(string sceneName)
    {
        //��� �κ��丮�� ������ ����
        SmartphoneManager.instance.DeleteAllItemInInven();

        //�ش� ������ �ʿ��� �����۸� ����
        //��丸 ����
        if (sceneName == "Kitchen" || sceneName == "Bathroom" || sceneName == "Veranda" || sceneName == "Bath")
        {
            SmartphoneManager.instance.SetInvenItem(itemList[0]);
        }
        //��¡��, �������� ����
        else if (sceneName == "SampleScene 2")
        {
            SmartphoneManager.instance.SetInvenItem(itemList[1]);
            SmartphoneManager.instance.SetInvenItem(itemList[2]);
        }

        //ī��, ��� ����
        switch(sceneName)
        {
            case "SampleScene":
                SmartphoneManager.instance.phone.SetCurTalk(0);
                DialogueManager.instance.SetCurDlg(0);
                break;
            case "Kitchen":
                SmartphoneManager.instance.phone.SetCurTalk(1);
                DialogueManager.instance.SetCurDlg(1);
                break;
            case "Bathroom":
                DialogueManager.instance.SetCurDlg(2);
                break;
            case "Bath":
                DialogueManager.instance.SetCurDlg(3);
                break;
            case "SampleScene 2":
                SmartphoneManager.instance.phone.SetCurTalk(2);
                DialogueManager.instance.SetCurDlg(4);
                break;
            case "Veranda":
                SmartphoneManager.instance.phone.SetCurTalk(3);
                DialogueManager.instance.SetCurDlg(5);
                break;
            default:
                break;
        }
    }
}
