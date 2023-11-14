using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAudioWithDlg : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] clips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(int num)
    {
        audioSource.clip = clips[num];
        audioSource.Play();
    }
}
