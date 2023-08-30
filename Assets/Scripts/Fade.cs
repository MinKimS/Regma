using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image blackImg;

    public void SetFadeOut(float time)
    {
        StartCoroutine(FadeOut(time));
    }

    public void SetFadeIn(float time)
    {
        StartCoroutine(FadeIn(time));
    }

    public void SetBlack()
    {
        blackImg.color = new Color(0, 0, 0, 1);
    }

    IEnumerator FadeOut(float time)
    {
        TimelineManager.instance.SetTimelinePause();
        float fadeValue = 0;
        while(fadeValue < 1.0f)
        {
            fadeValue+=0.01f;
            yield return new WaitForSeconds(time);
            blackImg.color = new Color(0,0,0, fadeValue);
        }
        TimelineManager.instance.SetTimelineResume();
    }

    IEnumerator FadeIn(float time)
    {
        TimelineManager.instance.SetTimelinePause();
        float fadeValue = 1;
        while(fadeValue > 0.0f)
        {
            fadeValue-=0.01f;
            yield return new WaitForSeconds(time);
            blackImg.color = new Color(0,0,0, fadeValue);
        }
        TimelineManager.instance.SetTimelineResume();
    }
}
