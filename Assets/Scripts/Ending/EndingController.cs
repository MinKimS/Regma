using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
    public Sprite[] endingSPs;

    Image endingImg;

    private void Awake()
    {
        endingImg = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        SetEnding();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            LoadingManager.LoadScene("Title");
        }
    }

    void SetEnding()
    {
        if (!GameManager.instance._isEndingTwo)
        {
            SetEndingOne();
        }
        else
        {
            SetEndingTwo();
        }
    }

    //ù��° ��Ʈ�� ���� ����
    void SetEndingOne()
    {
        endingImg.sprite = endingSPs[0];
    }

    //�ι�° ��Ʈ�� ���� ����
    void SetEndingTwo()
    {
        endingImg.sprite = endingSPs[1];
    }
}
