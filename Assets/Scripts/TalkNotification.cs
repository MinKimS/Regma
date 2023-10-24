using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //톡 알림 표시 상태 설정
    public void SetShowTalkIconState()
    {
        //보지 않은 톡이 온경우
        if(!isTalkIconShow && (SmartphoneManager.instance.inven.IsOpenInven || !SmartphoneManager.instance.phone.IsOpenPhone))
        {
            talkIcon.enabled = true;
            StartCoroutine(MoveTalkPos(showPos));
            isTalkIconShow = true;
        }
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
