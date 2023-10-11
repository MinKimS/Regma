using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDisappear : MonoBehaviour
{
    public GameObject[] mob;
    public MoveAlongThePath[] matp;
    public Dialogue[] dlg;

    public void DisappearOnTalbeInKitchen()
    {
        mob[0].SetActive(false);
        AudioManager.instance.StopSFX("주방_괴생명체1 도원 추격");
        TimelineManager.instance.timelineController.SetTimelineStart("TalkAfterHideOnTable");
    }
    public void DisappearOnCabinetInKitchen()
    {
        mob[1].SetActive(false);
        AudioManager.instance.StopSFX("주방_괴생명체1 도원 추격");
        //TimelineManager.instance.timelineController.SetTimelineStart("TalkAfterHideOnCabinet");
    }
    public void DisappearLastInKitchen()
    {
        mob[2].SetActive(false);
        AudioManager.instance.StopSFX("주방_괴생명체1 도원 추격");
        //TimelineManager.instance.timelineController.SetTimelineStart("TalkAfterHideOnLast");
    }
}
