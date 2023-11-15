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
            case "Bath":
            case "SampleScene 2":
                SetBGM(0, 0.5f);
                break;
            case "Kitchen":
                SetBGM(1);
                break;
            case "Veranda":
                SetBGM(2);
                break;
            case "Bathroom":
                SetBGM(3);
                break;
            case "Ending":
                SetBGM(4);
                break;
            default:
                break;
        }
    }

    void SetBGM(int num, float volume = 1)
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
