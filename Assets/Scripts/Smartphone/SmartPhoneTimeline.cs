using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPhoneTimeline : MonoBehaviour
{
    public void ShowPhoneInTimeline()
    {
        SmartphoneManager.instance.phone.ShowPhone();
    }
    public void HidePhoneInTimeline()
    {
        SmartphoneManager.instance.phone.HidePhone();
    }

    public void AddSendTalkInTimeline()
    {
        SmartphoneManager.instance.phone.AddSendTalk();
    }
    public void StartTalkInTimeline()
    {
        SmartphoneManager.instance.phone.StartTalk();
        print("starttalk");
    }
    public void AddVideoTalkInTimelin(Speaker speaker)
    {
        SmartphoneManager.instance.phone.AddVideoTalk(speaker);
    }
    public void SetNextTalkInTimeline()
    {
        SmartphoneManager.instance.phone.SetNextTalk();
    }
}
