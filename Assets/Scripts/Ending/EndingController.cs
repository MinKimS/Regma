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
    bool isGoingTitle = false;

    public Fade fade;

    private void Awake()
    {
        endingImg = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        SetEnding();

        if (!isGoingTitle)
        {
            if (!isShowingEnding && !isFirstEnding)
            {
                isShowingEnding = true;
                StartCoroutine(IEEndingTwo());
            }

            if (isFirstEnding)
            {
                StartCoroutine(IEGoToTitle());
            }
        }
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Return) && !isGoingTitle)
    //    {
    //        if(!isShowingEnding && !isFirstEnding)
    //        {
    //            isShowingEnding = true;
    //            StartCoroutine(IEEndingTwo());
    //        }

    //        if (isFirstEnding)
    //        {
    //            StartCoroutine(IEGoToTitle());
    //        }
    //    }
    //}

    IEnumerator IEEndingTwo()
    {
        yield return new WaitForSeconds(1f);

        SmartphoneManager.instance.phone.ShowPhone();

        yield return new WaitForSeconds(0.1f);

        SmartphoneManager.instance.phone.DeleteTalks();

        yield return new WaitUntil(() => !SmartphoneManager.instance.phone.phoneTalkList[0].gameObject.activeSelf);
        yield return new WaitForSeconds(1.5f);
        SmartphoneManager.instance.phone.HidePhone();

        ShowImage(2);
        yield return new WaitForSeconds(1.5f);
        ShowImage(3);
        yield return new WaitForSeconds(1.5f);
        ShowImage(4);
        StartCoroutine(IEGoToTitle());
    }

    IEnumerator IEGoToTitle()
    {
        yield return new WaitForSeconds(4f);
        isGoingTitle = true;
        fade.SetFadeOut(0.00001f);
        yield return new WaitForSeconds(2f);
        LoadingManager.LoadScene("Title");
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

    void ShowImage(int num)
    {
        endingImg.sprite = endingSPs[num];
    }
}
