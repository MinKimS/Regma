using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalkNotification : MonoBehaviour
{
    private Image talkIcon;
    private RectTransform curTr;
    private Vector2 showPos;
    private Vector2 hidePos;
    public float moveTime = 0.4f;
    [HideInInspector] public bool isTalkIconShow = false;

    private void Start() {
        talkIcon = GetComponent<Image>();
        curTr = GetComponent<RectTransform>();

        showPos = new Vector2(-40, 30);
        hidePos = new Vector2(-40, -170);
        curTr.anchoredPosition = hidePos;
    }


    private void OnEnable()
    {
        //이거 켜져 있으면 각각 씬에서 대사 테스트 안됨
        SceneManager.sceneLoaded += LoadSceneEvent;
    }
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (SmartphoneManager.instance != null)
        {
            SetHideTalkIconStateForcing();
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    //톡 알림 표시 상태 설정
    public void SetShowTalkIconState()
    {
        //보지 않은 톡이 온경우
        if(!isTalkIconShow && (SmartphoneManager.instance.inven.IsOpenInven || !SmartphoneManager.instance.phone.IsOpenPhone))
        {
            AudioManager.instance.SFXPlay("Game Sound_messgae alarm");
            talkIcon.enabled = true;
            StartCoroutine(MoveTalkPos(showPos));
            isTalkIconShow = true;
        }
    }

    //톡 알림만 깜빡이기
    public void SetNotification()
    {
        AudioManager.instance.SFXPlayLoop("ending_messenger alarm");
        StartCoroutine(IESetNotification());
    }
    IEnumerator IESetNotification()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (SceneManager.GetActiveScene().name == "Veranda")
        {
            talkIcon.enabled = true;
            StartCoroutine(MoveTalkPos(showPos));
            isTalkIconShow = true;
            print("show");
            yield return wait;

            talkIcon.enabled = false;
            curTr.anchoredPosition = hidePos;
            isTalkIconShow = false;
            print("hide");

            yield return wait;
        }
        SetHideTalkIconState();
    }

    //톡 알림 표시 상태 설정
    public void SetHideTalkIconState()
    {
        if(isTalkIconShow && SmartphoneManager.instance.phone.IsOpenPhone&&!SmartphoneManager.instance.inven.IsOpenInven)
        {
            talkIcon.enabled = false;
            curTr.anchoredPosition = hidePos;
            isTalkIconShow = false;
        }
    }

    public void SetHideTalkIconStateForcing()
    {
        if(talkIcon != null) { talkIcon.enabled = false; }
        if(curTr != null) { curTr.anchoredPosition = hidePos; }
        isTalkIconShow = false;
    }
    
    private IEnumerator MoveTalkPos(Vector2 targetPos)
    {
        Vector2 pos = curTr.anchoredPosition;
        float elapsedTime = 0f;

        while(elapsedTime < moveTime)
        {
            float t = elapsedTime / moveTime;
            curTr.anchoredPosition = Vector2.Lerp(pos, targetPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        curTr.anchoredPosition = targetPos;
    }
}
