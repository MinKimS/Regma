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
        isShowImg = true;
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

    void ShowImage(int num)
    {
        endingImg.sprite = endingSPs[num];
    }
}