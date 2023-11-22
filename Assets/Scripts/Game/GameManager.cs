using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    bool isEndingTwo = false;
    string lastScene = "Title";

    //use bath scene
    bool isMeetBathMob = false;

    public bool _isEndingTwo
    {
        get { return isEndingTwo; }
    }
    public bool _isMeetBathMob
    {
        get { return isMeetBathMob; }
        set { isMeetBathMob = value;}
    }

    //엔딩 루트 설정
    public void SetEndingRoute(bool b)
    {
        isEndingTwo = b ? true : false;
        print("isEndingTwo : " + isEndingTwo);
    }

    public void SetLastScene(string str)
    {
        lastScene = str;
    }
    public string GetLastScene()
    {
        return lastScene;
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
        if (SceneManager.GetActiveScene().name == "Title")
        {
            isMeetBathMob = false;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    //========

    [HideInInspector] public bool isMenuOpen = false;
    [HideInInspector] public bool isHowtoOpen = false;
}
