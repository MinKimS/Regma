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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            SetEndingRoute(false);
        }
        else if (Input.GetKeyDown(KeyCode.End))
        {
            SetEndingRoute(true);
        }
    }
}
