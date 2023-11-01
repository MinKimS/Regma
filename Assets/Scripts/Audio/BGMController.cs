using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    AudioSource BGM;
    public AudioClip[] bgmList;

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

        BGM.Stop();
        switch (sc.name)
        {
            case "SampleScene":
                BGM.clip = bgmList[0];
                break;
            case "Kitchen":
                BGM.clip = bgmList[1];
                break;
            case "Veranda":
                BGM.clip = bgmList[2];
                break;
            default:
                break;
        }
        BGM.Play();
    }
}
