using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    //문 자세히 보기
    public Image doorDetailImg;
    Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
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
    }

    public void SetAnimKnock(bool value)
    {
        anim.SetBool("isKnock", value);
    }
}
