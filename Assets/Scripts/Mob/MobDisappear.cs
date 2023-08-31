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
        TimelineManager.instance.SetTimelineStart("TalkAfterHideOnTable");
    }
    public void DisappearOnCabinetInKitchen()
    {
        mob[1].SetActive(false);
        TimelineManager.instance.SetTimelineStart("TalkAfterHideOnCabinet");
    }
    public void DisappearLastInKitchen()
    {
        mob[2].SetActive(false);
        TimelineManager.instance.SetTimelineStart("TalkAfterHideOnLast");
    }
}
