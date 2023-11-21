using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    AudioSource BGM;
    public AudioClip[] bgmList;
    bool isOkChgBGM = false;
    private void Awake()
    {
        BGM = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadSceneEvent;
    }

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ChangeBGM());
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    //배경음악 변경
    IEnumerator ChangeBGM()
    {
        Scene sc = SceneManager.GetActiveScene();
        yield return new WaitWhile(() => sc.name == "LoadingScene");

        switch (sc.name)
        {
            case "Title":
            case "SampleScene":
            case "SampleScene 2":
                SetBGM(0, 0.4f);
                break;
            case "Kitchen":
                SetBGM(1, 0.1f);
                break;
            case "Veranda":
                SetBGM(2, 0.1f);
                break;
            case "Bathroom":
            case "Bath":
                SetBGM(3, 1f);
                break;
            case "Ending":
                SetBGM(4, 0.5f);
                break;
            default:
                break;
        }
    }

    public void SetBGM(int num, float volume = 1)
    {
        if(num < bgmList.Length)
        {
            if (BGM.clip != bgmList[num])
            {
                isOkChgBGM = true;
                BGM.Stop();
            }
            else { isOkChgBGM = false; }
            BGM.clip = bgmList[num];
            if (isOkChgBGM)
            {
                BGM.Play();
                BGM.volume = volume;
            }
        }
    }
}
