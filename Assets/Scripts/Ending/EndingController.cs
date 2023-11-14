using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
    public Sprite[] endingSPs;

    Image endingImg;

    bool isFirstEnding = false;
    bool isShowingEnding = false;

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
        if(!isShowingEnding && Input.GetKeyDown(KeyCode.Return))
        {
            if(isFirstEnding)
            {
                LoadingManager.LoadScene("Title");
            }
            else
            {
                StartCoroutine(IEEndingTwo());
            }
            isShowingEnding = true;
        }
    }

    IEnumerator IEEndingTwo()
    {
        SmartphoneManager.instance.phone.AddTalk("test");

        yield return new WaitForSeconds(1f);

        SmartphoneManager.instance.phone.ShowPhone();

        yield return new WaitForSeconds(1f);

        SmartphoneManager.instance.phone.DeleteTalks();
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

    //첫번째 루트로 엔딩 설정
    void SetEndingOne()
    {
        isFirstEnding = true;
        endingImg.sprite = endingSPs[0];
    }

    //두번째 루트로 엔딩 설정
    void SetEndingTwo()
    {
        isFirstEnding = false;
        endingImg.sprite = endingSPs[1];
    }
}
