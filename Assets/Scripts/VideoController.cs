using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer vPlayer;
    private void Start() {
        //�׽�Ʈ�� ����
        //TimelineManager.instance.SetTimelineStart(0);
        //-------------------------
        //vPlayer.loopPointReached+=EndReached;
    }
    void EndReached(VideoPlayer vp)
    {
        gameObject.SetActive(false);
        TimelineManager.instance.SetTimelineStart(0);
    }

    private void OnDisable() {
        vPlayer.loopPointReached-=EndReached;
    }
}
