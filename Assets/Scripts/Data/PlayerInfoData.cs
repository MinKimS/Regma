using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfoData : MonoBehaviour
{
    //자주 쓰는 플레이어 관련 정보 가져오는 스크립트
    public Transform playerTr;
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

    void Start()
    {
        SceneManager.sceneLoaded += LoadSceneEvent;
        SetPlayerInfo();
    }
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoadingScene")
        {
            SetPlayerInfo();
        }
    }

    void SetPlayerInfo()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerAnim = playerTr.GetComponent<Animator>();
    }
}
