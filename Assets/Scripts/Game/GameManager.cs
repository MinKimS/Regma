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

    //���� ��Ʈ ����
    public void SetEndingRoute(bool b)
    {
        isEndingTwo = b ? true : false;
        print("isEndingTwo : " + isEndingTwo);
    }
}
