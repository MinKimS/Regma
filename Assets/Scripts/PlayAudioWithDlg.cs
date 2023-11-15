using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAudioWithDlg : MonoBehaviour
{
    public void PlayAudio(string audio)
    {
        AudioManager.instance.SFXPlay(audio);
    }

    public void StopAudio(string audio)
    {
        AudioManager.instance.StopSFX(audio);
    }
}
