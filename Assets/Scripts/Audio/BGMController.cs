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
            case "SampleScene":
            case "Bathroom":
            case "Bath":
            case "SampleScene 2":
                if(BGM.clip != bgmList[0])
                {
                    isOkChgBGM = true;
                    BGM.Stop();
                }
                else { isOkChgBGM = false; }
                BGM.clip = bgmList[0];
                if(isOkChgBGM) { BGM.Play(); }
                break;
            case "Kitchen":
                if (BGM.clip != bgmList[1])
                {
                    isOkChgBGM = true;
                    BGM.Stop();
                }
                else { isOkChgBGM = false; }
                BGM.clip = bgmList[1];
                if (isOkChgBGM) { BGM.Play(); }
                break;
            case "Veranda":
                if (BGM.clip != bgmList[2])
                {
                    isOkChgBGM = true;
                    BGM.Stop();
                }
                else { isOkChgBGM = false; }
                BGM.clip = bgmList[2];
                if (isOkChgBGM) { BGM.Play(); }
                break;
            default:
                break;
        }
    }
}
