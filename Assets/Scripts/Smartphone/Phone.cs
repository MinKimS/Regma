using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    //모든 씬에 나올 첫번째 톡들
    public List<Talk> talkList;
    int talkListIdx = 0;

    //톡 스크롤 영역
    public ScrollRect talkScView;
    public Scrollbar talkScBar;
    //톡이 나오는 곳
    public GameObject talkParent;
    RectTransform talkParentRT;
    public GameObject talkInputArea;
    RectTransform talkInputAreaRT;
    //핸드폰 인벤토리
    public InventoryController inven;
    public TalkNotification notification;
    //핸드폰 뒷 배경
    Image phoneBgPanel;
    GameObject phoneFrame;

    //화면에 나오는 톡들------------

    //플레이어의 톡
    public GameObject playerTalk;
    //타인의 톡
    public GameObject anotherTalk;
    //입장 톡
    public GameObject inOutTalk;
    //공지 톡
    public GameObject announcementTalk;
    //영상 톡
    public GameObject videoTalk;
    //플레이어가 보낼 수 있는 톡
    public GameObject sendTalk;

    //가장 최근에 온 톡
    TalkData lastTalk;
    //가장 최근에 온 플레이어 톡
    [HideInInspector] public TalkData lastPlayerTalk;
    //현재 출력될 톡
    public Talk curTalk;
    //현재 보낼 톡
    public SendTalk curSendTalk;

    //폰 보이는 여부
    bool isOpenPhone = false;

    bool isPlayerFirstTalk = true;
    bool isOKSendTalk = false;

    [SerializeField]
    float phoneShowSpeed = 0.1f;

    int TalkMemberNum = 5;
    //int sendTalkIdx = 0;
    //int receiveTalkIdx = 0;
    int showedCount = 0;
    //선택된 아이템
    int selectedOption = 1;
    [HideInInspector] public int talkIdx = 0;

    public List<sendTalkData> sendTalkList;

    public bool IsOpenPhone
    {
        get { return isOpenPhone;}
    }
    public bool IsPlayerFirstTalk
    {
        get { return isPlayerFirstTalk; }
    }
    public bool IsOKSendTalk
    {
        get { return isOKSendTalk; }
        set { isOKSendTalk = value; }
    }
    public int SelectedOption
    {
        get { return selectedOption; }
    }
    public int ShowedCount
    {
        get { return showedCount; }
    }

    private void Awake()
    {
        talkScBar = talkScView.GetComponentInChildren<Scrollbar>();
        talkParentRT = talkParent.GetComponent<RectTransform>();
        talkInputAreaRT = talkInputArea.GetComponent<RectTransform>();
        phoneBgPanel = GetComponent<Image>();
        phoneFrame = GetComponentsInChildren<RectTransform>()[1].gameObject;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += LoadSceneEvent;
    }

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoadingScene" && scene.name != "Bathroom" && scene.name != "Bath")
        {
            //TODO : 같은 씬을 불러 왔을 때 설정
            //-----------------------------------------------------------
            //if(scene.name != SceneManager.GetActiveScene().name)
            //{
            //    talkListIdx++;
            //}
            //else
            //{
            //    talkListIdx = 0;
            //}
            talkListIdx++;
            curTalk = talkList[talkListIdx];
        }
    }
    //폰 보이기
    public void ShowPhone()
    {
        if (!PlayerInfoData.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing"))
        {
            PlayerInfoData.instance.playerAnim.SetBool("walk", false);
            PlayerInfoData.instance.playerAnim.SetBool("jump", false);
        }

        //가장 최근의 톡부터 보이도록 설정
        phoneBgPanel.enabled = true;
        phoneFrame.SetActive(true);

        talkScBar.value = 0f;
        isOpenPhone = true;
        //톡 알림 표시 표시중이면 비활성화
        notification.SetHideTalkIconState();

        Invoke("ScrollToBottom", 0.03f);
    }
    //폰 숨기기
    public void HidePhone()
    {
        //인벤이 켜져있으면 함께 끄기
        if (!inven.IsOpenInven) { inven.HideInven(); }
        phoneBgPanel.enabled = false;
        phoneFrame.SetActive(false);
        isOpenPhone = false;
    }

    //톡 추가
    public void AddTalk(string text)
    {
        TalkData talk = Instantiate(playerTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);
        talk.talkText.text = text;

        //최근 톡의 사람이 지금 톡을 보낸 사람과 같은지여부
        bool isSameUser = lastTalk != null && lastTalk.userName == talk.userName;

        //이어지는 톡 설정
        talk.tail.SetActive(!isSameUser);

        talk.readNum = TalkMemberNum - 1;
        talk.readNumText.text = talk.readNum.ToString();

        lastTalk = talk;
        lastPlayerTalk = talk;

        Invoke("ScrollToBottom", 0.03f);

        StartCoroutine(FitLayout(talkParentRT));
        StartCoroutine(FitLayout(talkInputAreaRT));
    }

    public void AddTalk(bool isAnnouncement, Speaker user, string text)
    {
        TalkData talk = Instantiate(isAnnouncement ? announcementTalk : anotherTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);
        talk.talkText.text = text;
        if (!isAnnouncement) talk.userName = user.talkName;

        //최근 톡의 사람이 지금 톡을 보낸 사람과 같은지여부
        bool isSameUser = lastTalk != null && lastTalk.userName == talk.userName;

        //이어지는 톡 설정
        talk.tail.SetActive(!isSameUser);
        if (!isAnnouncement)
        {
            talk.profile.gameObject.SetActive(!isSameUser);
            talk.nameText.gameObject.SetActive(!isSameUser);
            talk.nameText.text = talk.userName;


            if (user.talkProfileSp != null)
            {
                talk.profileImage.sprite = user.talkProfileSp;
            }
        }
        talk.readNum = TalkMemberNum - 2;
        talk.readNumText.text = talk.readNum.ToString();

        lastTalk = talk;

        Invoke("ScrollToBottom", 0.03f);
        StartCoroutine(FitLayout(talkParentRT));

        notification.SetHideTalkIconState();
    }

    public void AddInOutTalk(bool isIn, string text)
    {
        TalkData talk = Instantiate(inOutTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);

        if (isIn)
        {
            talk.talkText.text = text + "님이 들어왔습니다.";
        }
        else
        {
            talk.talkText.text = text;
        }

        lastTalk = talk;

        Invoke("ScrollToBottom", 0.03f);
        StartCoroutine(FitLayout(talkParentRT));
    }

    //플레이어가 보낼 톡 선택지 추가하기
    public void AddSendTalk()
    {
        isPlayerFirstTalk = true;
        SetSendTalk(curTalk.answerTalk);
        isOKSendTalk = true;
    }

    //가장 최근 본인 톡 보이기
    void ScrollToBottom()
    {
        talkScBar.value = 0.0001f;
    }

    //플레이어가 보낼 톡 선택지 설정
    public void SetSendTalk(SendTalk[] SendTalkContexts)
    {
        // isSendTalkReady = true;
        isOKSendTalk = true;

        //대답을 보낼 톡 선택지 설정
        if (SendTalkContexts != null)
        {
            for (int i = 0; i < SendTalkContexts.Length; i++)
            {
                sendTalkData sendTalk = sendTalkList[i];
                sendTalk.gameObject.SetActive(true);
                sendTalk.talkText.text = sendTalk.text = SendTalkContexts[i].talkText;
                sendTalkList[i].talkText.enabled = true;
                sendTalkList[i].talkText.color = Color.gray;
            }
        }
        showedCount = SendTalkContexts.Length;

        //sendTalkList[0].talkColor = Color.white;
        sendTalkList[0].talkText.color = Color.black;
        selectedOption = 1;

        StartCoroutine(FitLayout(talkInputAreaRT));
    }

    //플레이어가 보낼 톡이 1개인 경우
    public void SetSendTalk(SendTalk SendTalkContexts)
    {
        // isSendTalkReady = true;

        //대답을 보낼 톡 선택지 설정
        if (SendTalkContexts != null)
        {
            sendTalkData sendTalk = sendTalkList[0];
            sendTalk.gameObject.SetActive(true);
            sendTalk.talkText.text = sendTalk.text = SendTalkContexts.talkText;
            sendTalkList[0].talkText.enabled = true;
        }
        showedCount = 1;

        sendTalkList[0].talkText.color = Color.black;
        selectedOption = 1;
        isOKSendTalk = true;

        StartCoroutine(FitLayout(talkInputAreaRT));
    }

    public void HideSendTalk()
    {
        for (int i = 0; i < showedCount; i++)
        {
            sendTalkList[i].talkText.text = "";
            sendTalkList[i].talkText.enabled = false;
            sendTalkList[i].gameObject.SetActive(false);
        }
        StartCoroutine(FixLayoutGroup());

        if (curSendTalk == null && curTalk.isInTimeline)
        {
            TimelineManager.instance.timelineController.SetTimelineResume();
        }
    }

    public void SelectTalk(int value)
    {
        selectedOption += value;

        for(int i = 0; i < showedCount;i++)
        {
            //선택 해제 표시
            sendTalkList[i].talkText.color = Color.gray;
        }
        //선택되어 있는 상태 표시
        sendTalkList[selectedOption - 1].talkText.color = Color.black;
    }

    public void AddVideoTalk(Speaker user)
    {
        TalkData talk = Instantiate(videoTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);
        talk.userName = user.talkName;

        //최근 톡의 사람이 지금 톡을 보낸 사람과 같은지여부
        bool isSameUser = lastTalk != null && lastTalk.userName == talk.userName;

        talk.profile.gameObject.SetActive(!isSameUser);
        talk.nameText.gameObject.SetActive(!isSameUser);
        talk.nameText.text = talk.userName;


        if (user.talkProfileSp != null)
        {
            talk.profileImage.sprite = user.talkProfileSp;
        }
        talk.readNum = TalkMemberNum - 2;
        talk.readNumText.text = talk.readNum.ToString();

        lastTalk = talk;

        Invoke("ScrollToBottom", 0.03f);
        StartCoroutine(FitLayout(talkParentRT));

        notification.SetHideTalkIconState();
    }
    public void SetNextSendTalk()
    {
        isPlayerFirstTalk = false;
        curSendTalk = curSendTalk.nextSendTalk;
        SetSendTalk(curSendTalk);
    }

    //다음 톡 설정
    public void SetNextTalk()
    {
        talkIdx = 0;
        isPlayerFirstTalk = true;
        if (curTalk.nextTalk != null && curTalk.nextTalk.Length > 0)
        {
            if(curTalk.answerTalk.Length<2)
            {
                curTalk = curTalk.nextTalk[0];
            }
            else
            {
                curTalk = curTalk.nextTalk[selectedOption - 1];
            }
        }
    }

    IEnumerator ShowPhoneWithMotion()
    {
        while(transform.position.y <= 540f)
        {
            print("showing");
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 540), phoneShowSpeed);
            yield return null;
        }
    }
    //레이아웃 버그 해소
    IEnumerator FitLayout(RectTransform rt)
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
    }

    IEnumerator FixLayoutGroup()
    {
        VerticalLayoutGroup vlg = talkInputArea.GetComponent<VerticalLayoutGroup>();
        vlg.enabled = false;
        yield return new WaitForEndOfFrame();
        vlg.enabled = true;
    }

    //톡 시작
    public void StartTalk()
    {
        StartCoroutine(OutputOtherUserTalk());
    }

    //톡 출력
    IEnumerator OutputOtherUserTalk()
    {
        float delayTime = 0;

        notification.SetShowTalkIconState();

        //상대방 대화 출력
        while (curTalk.TalkContexts.Count > talkIdx)
        {
            delayTime = curTalk.TalkContexts[talkIdx].TalkSendDelay;
            yield return new WaitForSeconds(delayTime);
            SmartphoneManager.instance.phone.AddTalk(false, curTalk.TalkContexts[talkIdx].user, curTalk.TalkContexts[talkIdx++].talkText);
        }

        yield return new WaitForSeconds(1.0f);

        //모든 상대방의 대화가 나오고 난 뒤 수행되는 것
        if(curTalk.afterEndTalk != Talk.AfterEndTalk.None)
        {
            if (curTalk.afterEndTalk == Talk.AfterEndTalk.StartTimeline)
            {
                delayTime = curTalk.TalkContexts[talkIdx - 1].TalkSendDelay;
                yield return new WaitForSeconds(1.0f);
                TimelineManager.instance.timelineController.SetTimelineStart(curTalk.timelineName);
            }
            else if (curTalk.afterEndTalk == Talk.AfterEndTalk.ContinueTimeline)
            {
                TimelineManager.instance.timelineController.SetTimelineResume();
            }
            else if(curTalk.afterEndTalk == Talk.AfterEndTalk.RunEvent)
            {
                curTalk.runEvent.Raise();
            }
            else
            {
                if (curTalk.isInTimeline)
                {
                    TimelineManager.instance.timelineController.SetTimelinePause();
                }
                yield return new WaitForSeconds(1.0f);
                SmartphoneManager.instance.phone.SetSendTalk(curTalk.answerTalk);
            }
        }


        if(curTalk.afterEndTalk != Talk.AfterEndTalk.SendTalkAndRunNextTalk&& curTalk.afterEndTalk != Talk.AfterEndTalk.StartTimeline&& curTalk.afterEndTalk != Talk.AfterEndTalk.SendTalkAndRunEvent && !curTalk.isInTimeline)
        {
            if(curTalk.nextTalk!= null)
            {
                SetNextTalk();
            }
        }
    }
}
