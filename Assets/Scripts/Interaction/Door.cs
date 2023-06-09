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

    private void Start() {
        anim = GetComponent<Animator>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return) && checkWorkDo > 2 && isOpen)
        {SetDoorOpen();}
    }

    public void SetDoorOpen() {
        isOpen = false;
        print("open door");
        TimelineManager.instance.SetTimelineStart("LT F");
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
        EventManager.instance.ActiveEvent(2);
        diaryEvent.SetActive(true);
    }

    public void SetAnimKnock(bool value)
    {
        anim.SetBool("isKnock", value);
    }
}
