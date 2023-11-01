using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

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

    //ȿ���� ���
    public void SFXPlay(string sfxName)
    {
        AudioClip clip = FindPlaySFX(sfxName);
        
        if(clip != null)
        {
            SFX[sfxIdx].clip = clip;
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

    //ȿ���� ���� ���
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
        else
        {
            Debug.LogWarning(sfxName + "doesn't exist.");
        }
    }

    //ȿ���� ����
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
                    SFX[i].Stop();
                    SFX[i].clip = null;
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning(sfxName + "doesn't exist.");
        }

    }

    //ȿ���� ã��
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

    //������� ��� ȿ���� ����
    public void StopSFXAll()
    {
        for(int i = 0; i<SFX.Length; i++)
        {
            SFX[i].Stop();
            SFX[i].clip = null;
            SFX[i].loop = false;
        }
    }

}
