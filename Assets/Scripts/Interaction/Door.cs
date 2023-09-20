using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    //문 자세히 보기
    public Image doorDetailImg;
    Animator anim;
    public int checkWorkDo = 0;
    public bool isOpen = false;
    public GameObject diaryEvent;
    InteractionObjData interactionObjData;

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
        print("open door");
        TimelineManager.instance.SetTimelineStart("LT F");
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
        TimelineManager.instance.SetTimelineStart("DoorEvent 0");
        doorDetailImg.enabled = false;
        EventManager.instance.ActiveEvent(0);
        diaryEvent.SetActive(true);
        interactionObjData.IsOkInteracting = false;
    }

    //노크해서 두들겨지는 문 애니메이션 실행
    public void SetAnimKnock(bool value)
    {
        anim.SetBool("isKnock", value);
    }

    public void MoveNextStage()
    {
        if(checkWorkDo > 2)
        {
            print("move next stage");
            LoadingManager.LoadScene("Kitchen");
        }
    }
}
