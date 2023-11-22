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
    bool isOkEnter = true;

    int endingIdx = 0;

    public Fade fade;

    private void Awake()
    {
        endingImg = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        SetEnding();

        //if (!isGoingTitle)
        //{
        //    if (!isShowingEnding && !isFirstEnding)
        //    {
        //        isShowingEnding = true;
        //        StartCoroutine(IEEndingTwo());
        //    }

        //    if (isFirstEnding)
        //    {
        //        StartCoroutine(IEGoToTitle());
        //    }
        //}

        AudioManager.instance.StopSFXAll();
    }

    private void Update()
    {
        //시연을 위해 엔터를 통해 엔딩 넘기는 기능
        if (Input.GetKeyDown(KeyCode.Return) && !isGoingTitle && isOkEnter)
        {
            //시간 관계상 다음과 같이 구현
            if (endingIdx == 0)
            {
                ShowImage(2);
            }
            else if (endingIdx == 1)
            {
                ShowImage(3);
            }
            else if (endingIdx == 2)
            {
                ShowImage(4);
            }
            else if (endingIdx == 3)
            {
                isOkEnter = false;
                StartCoroutine(IEGoToTitle());
            }
            endingIdx++;
        }
    }

    IEnumerator IEEndingTwoPhone()
    {
        isOkEnter = false;
        SmartphoneManager.instance.phone.ShowPhone();

        yield return new WaitForSeconds(0.1f);

        SmartphoneManager.instance.phone.DeleteTalks();

        yield return new WaitUntil(() => !SmartphoneManager.instance.phone.phoneTalkList[0].gameObject.activeSelf);
        yield return new WaitForSeconds(0.1f);
        SmartphoneManager.instance.phone.HidePhone();
        endingImg.color = Color.white;
        endingImg.sprite = endingSPs[1];
        isOkEnter = true;
    }

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
        fade.SetFadeOut(0.000005f);
        isGoingTitle = true;
        yield return new WaitForSeconds(4f);
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
        endingImg.color = Color.white;
    }

    //두번째 루트로 엔딩 설정
    void SetEndingTwo()
    {
        isFirstEnding = false;
        StartCoroutine(IEEndingTwoPhone());
    }

    void ShowImage(int num)
    {
        endingImg.sprite = endingSPs[num];
    }
}
