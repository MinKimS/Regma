using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfoData : MonoBehaviour
{
    //자주 쓰는 플레이어 관련 정보 가져오는 스크립트
    public Transform playerTr;
    public Rigidbody2D playerRb;
    public Animator playerAnim;

    public static PlayerInfoData instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (LoadingManager.nextScene != "Ending" || SceneManager.GetActiveScene().name != "Ending")
        {
            SceneManager.sceneLoaded += LoadSceneEvent;
        }
    }

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if(LoadingManager.nextScene == "Title" || LoadingManager.nextScene == "Intro" || LoadingManager.nextScene == "Ending")
        {
            Destroy(gameObject);
        }
        if(SceneManager.GetActiveScene().name != "Title")
        {
            StartCoroutine(SetPlayerInfo());
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    IEnumerator SetPlayerInfo()
    {
        Scene sc = SceneManager.GetActiveScene();
        yield return new WaitWhile(() => sc.name == "LoadingScene");
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        playerAnim = playerTr.GetComponent<Animator>();
    }
}
