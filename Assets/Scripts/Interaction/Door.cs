using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    //문 자세히 보기
    public Image doorDetailImg;
    bool isEventActive=false;

    public void ShowDoorImg()
    {
        doorDetailImg.enabled = true;
        
        Invoke("StartCurDoorEvent", 1.0f);
    }
    public void HideDoorImg()
    {
        doorDetailImg.enabled = false;
    }
    void StartCurDoorEvent()
    {
        if(!isEventActive)
        {
            TimelineManager.instance.SetTimelineStart(4);
            isEventActive = true;
            doorDetailImg.enabled = false;
            EventManager.instance.ActiveEvent(2);
        }
    }
}