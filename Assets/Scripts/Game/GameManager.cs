using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool _isEndingTwo
    {
        get { return isEndingTwo; }
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

    //========

    [HideInInspector] public bool isMenuOpen = false;
    [HideInInspector] public bool isHowtoOpen = false;
}
