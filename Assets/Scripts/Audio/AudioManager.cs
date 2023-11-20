using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] SFX;
    public AudioClip[] sfxList;
    int sfxIdx = 0;

    [HideInInspector] public BGMController bgm;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

        bgm = GetComponentInChildren<BGMController>();
    }

    //효과임 재생
    public void SFXPlay(string sfxName, float volume = 1, float pitch = 1)
    {
        AudioClip clip = FindPlaySFX(sfxName);
        
        if(clip != null)
        {
            SFX[sfxIdx].clip = clip;
            SFX[sfxIdx].volume = volume;
            SFX[sfxIdx].pitch = pitch;
            SFX[sfxIdx].PlayOneShot(clip);
            if(++sfxIdx >= SFX.Length)
            {
                sfxIdx = 0;
            }
        }
    }

    public void SFXPlayTime(string sfxName, float volume = 1, float pitch = 1, int length = 100)
    {
        SFXPlay(sfxName, volume, pitch);
        StartCoroutine(IESFXPlayTime(sfxName, length));
    }

    IEnumerator IESFXPlayTime(string sfxName, int length = 100)
    {
        AudioClip clip = FindPlaySFX(sfxName);
        if(clip != null)
        {
            float playTime = clip.length;
            if (length >= 100)
            {
                playTime = clip.length;
            }
            else if (length >= 90)
            {
                playTime = clip.length * 0.9f;
            }
            else if (length >= 80)
            {
                playTime = clip.length * 0.8f;
            }
            else if (length >= 70)
            {
                playTime = clip.length * 0.7f;
            }
            else if (length >= 60)
            {
                playTime = clip.length * 0.6f;
            }
            else if (length >= 50)
            {
                playTime = clip.length * 0.5f;
            }
            else if (length >= 40)
            {
                playTime = clip.length * 0.4f;
            }
            else if (length >= 30)
            {
                playTime = clip.length * 0.3f;
            }
            else if (length >= 20)
            {
                playTime = clip.length * 0.2f;
            }
            else
            {
                playTime = clip.length * 0.1f;
            }

            yield return new WaitForSeconds(playTime);
            StopSFX(sfxName);
        }
    }

    //효과음 루프 재생
    public void SFXPlayLoop(string sfxName)
    {
        AudioClip clip = FindPlaySFX(sfxName);

        if (clip != null)
        {
            SFX[sfxIdx].clip = clip;
            SFX[sfxIdx].Play();
            SFX[sfxIdx].loop = true;
            if (++sfxIdx >= SFX.Length)
            {
                sfxIdx = 0;
            }
        }
    }

    //효과음 정지
    public void StopSFX(string sfxName)
    {
        AudioClip clip = FindPlaySFX(sfxName);

        if (clip != null)
        {
            for (int i = 0; i < SFX.Length; ++i)
            {
                if (SFX[i].clip == clip)
                {
                    SFX[i].loop = false;
                    SFX[i].volume = 1f;
                    SFX[i].pitch = 1f;
                    SFX[i].Stop();
                    SFX[i].clip = null;
                    break;
                }
            }
        }

    }

    //효과음 찾기
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

    //재생중인 모든 효과음 정지
    public void StopSFXAll()
    {
        for(int i = 0; i<SFX.Length; i++)
        {
            SFX[i].Stop();
            SFX[i].clip = null;
            SFX[i].loop = false;
        }
    }

    public bool CheckAudioPlaying(string targetClip)
    {
        for (int i = 0; i < SFX.Length; ++i)
        {
            if (SFX[i].clip != null && SFX[i].clip.name == targetClip && SFX[i].isPlaying)
            {
                return true;
            }
        }
        return false;
    }
}
