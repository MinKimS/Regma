using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
    public Sprite[] endingSPs;

    Image endingImg;

    bool isFirstEnding = false;

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
            if(isFirstEnding)
            {
                LoadingManager.LoadScene("Title");
            }
            else
            {
                SmartphoneManager.instance.phone.ShowPhone();
                SmartphoneManager.instance.phone.DeleteTalks();
            }
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
        isFirstEnding = true;
        endingImg.sprite = endingSPs[0];
    }

    //�ι�° ��Ʈ�� ���� ����
    void SetEndingTwo()
    {
        isFirstEnding = false;
        endingImg.sprite = endingSPs[1];
    }
}
