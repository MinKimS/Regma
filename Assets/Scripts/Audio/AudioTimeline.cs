using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTimeline : MonoBehaviour
{
    public void SFXPlay(string str)
    {
        AudioManager.instance.SFXPlay(str);
    }

    public void StopSFX(string str)
    {
        AudioManager.instance.StopSFX(str);
    }
}
