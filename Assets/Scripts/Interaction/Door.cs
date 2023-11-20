using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Door : MonoBehaviour
{
    //문 자세히 보기
    public Image doorDetailImg;
    Animator anim;
    public int checkWorkDo = 0;
    public bool isOpen = false;
    public GameObject diaryEvent;
    InteractionObjData interactionObjData;
    public EventManager eventManager;

    private void Start() {
        anim = GetComponent<Animator>();
        interactionObjData = GetComponent<InteractionObjData>();
    }
    private void Update() {
        if(DialogueManager.instance._dlgState == DialogueManager.DlgState.End && checkWorkDo > 2 && isOpen)
        {SetDoorOpen();}
    }

    public void SetDoorOpen() {
        isOpen = false;
        SetAnimKnock(false);
        TimelineManager.instance.timelineController.SetTimelineStart("LT F");
        interactionObjData.IsOkInteracting = true;
        interactionObjData.GmEventIdx++;
    }

    public void ShowDoorImg()
    {
        doorDetailImg.enabled = true;
        
        Invoke("StartCurDoorEvent", 1.0f);
    }
    public void HideDoorImg()
    {
        doorDetailImg.enabled = false;
    }
    
    public void StartCurDoorEvent()
    {
        TimelineManager.instance.timelineController.SetTimelineStart("DoorEvent 0");
        doorDetailImg.enabled = false;
        eventManager.ActiveObj(0);
        diaryEvent.SetActive(true);
        interactionObjData.IsOkInteracting = false;
    }

    //노크해서 두들겨지는 문 애니메이션 실행
    public void SetAnimKnock(bool value)
    {
        anim.SetBool("isKnock", value);
        if(value)
        {
            AudioManager.instance.SFXPlayLoop("Game Sound_Door konck");
        }
        else
        {
            AudioManager.instance.StopSFX("Game Sound_Door konck");
        }
    }

    public void MoveNextStage()
    {
        if(checkWorkDo > 2)
        {
            print("move next stage");
            LoadingManager.LoadScene("Kitchen");
            anim.SetBool("isKnock", false);
            AudioManager.instance.StopSFXAll();
            AudioManager.instance.SFXPlay("Game Sound_Door open2");
        }
    }

    public void MoveDoor(string sceneName)
    {
        LoadingManager.LoadScene(sceneName);
    }
}
