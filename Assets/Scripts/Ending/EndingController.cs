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
    bool isShowImg = false;

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
            if(!isShowingEnding)
            {
                isShowingEnding = true;
                StartCoroutine(IEEndingTwo());
            }

            if (isFirstEnding || isShowImg)
            {
                LoadingManager.LoadScene("Title");
            }
        }
    }

    IEnumerator IEEndingTwo()
    {
        SmartphoneManager.instance.phone.AddTalk("test1");
        SmartphoneManager.instance.phone.AddTalk("test2");
        SmartphoneManager.instance.phone.AddTalk("test3");
        SmartphoneManager.instance.phone.AddTalk("test4");
        SmartphoneManager.instance.phone.AddTalk("test5");
        SmartphoneManager.instance.phone.AddTalk("test6");
        SmartphoneManager.instance.phone.AddTalk("test7");
        SmartphoneManager.instance.phone.AddTalk("test8");
        SmartphoneManager.instance.phone.AddTalk("test9");
        SmartphoneManager.instance.phone.AddTalk("test10");

        yield return new WaitForSeconds(1f);

        SmartphoneManager.instance.phone.ShowPhone();

        yield return new WaitForSeconds(0.1f);

        SmartphoneManager.instance.phone.DeleteTalks();

        yield return new WaitUntil(() => !SmartphoneManager.instance.phone.phoneTalkList[0].gameObject.activeSelf);
        yield return new WaitForSeconds(1.5f);
        SmartphoneManager.instance.phone.HidePhone();

        ShowImage();
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

    void ShowImage()
    {
        endingImg.sprite = endingSPs[2];
        isShowImg = true;
    }
}
