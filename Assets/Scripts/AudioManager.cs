using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource BGM;
    public AudioSource[] SFX;
    public AudioClip[] sfxList;
    int sfxIdx = 0;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }

    public void SFXPlay(string sfxName)
    {
        AudioClip clip = FindPlaySFX(sfxName);
        
        if(clip != null)
        {
            SFX[sfxIdx].PlayOneShot(clip);
            if(++sfxIdx >= SFX.Length)
            {
                sfxIdx = 0;
            }
        }
        else
        {
            Debug.LogWarning(sfxName + "doesn't exist.");
        }
    }

    private AudioClip FindPlaySFX(string targetClip)
    {
        foreach(AudioClip clip in sfxList)
        {
            if(clip.name == targetClip)
            {
                return clip;
            }
        }
        return null;
    }

}
