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

    //소리 재생할지 여부
    bool canPlayTalkSound = true;

    //톡을 보내는 것이 가능여부
    [HideInInspector]
    public bool isOkStartTalk = true;

    //중간에 다른 톡을 보내야하는 경우 막는 용도
    [HideInInspector]
    public bool isTalkNeedToBeSend = false;

    int showedCount = 0;

    //엔딩2에서 톡 지우기를 위한 용도
    [HideInInspector] public List<RectTransform> phoneTalkList;

    //선택된 아이템
    int selectedOption = 1;
    [HideInInspector] public int talkIdx = 0;

    public List<sendTalkData> sendTalkList;

    //남은 톡을 설정할때 선택지가 있는 톡을 자신이 선택한 톡으로 남기기 위한 용도
    [HideInInspector] public int[] talkSelections = new int[2];
    [HideInInspector] public int talkselectionsIdx = 0;

    public Speaker child;

    public bool IsOpenPhone
    {
        get { return isOpenPhone;}
        set { isOpenPhone = value;}
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
        phoneTalkList = new List<RectTransform>();
    }

    private void Start()
    {
        talkSelections[0] = 0;
        talkSelections[1] = 0;
        SceneManager.sceneLoaded += LoadSceneEvent;
    }

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        if (scene.name != "LoadingScene" && scene.name != "Bathroom" && scene.name != "Bath" && scene.name != "Title" && scene.name != "Intro" && scene != null)
        {
            switch (scene.name)
            {
                case "SampleScene":
                    curTalk = talkList[0];
                    break;
                case "Kitchen":
                    curTalk = talkList[1];
                    break;
                case "SampleScene 2":
                    curTalk = talkList[2];
                    break;
                case "Veranda":
                    curTalk = talkList[3];
                    break;
            }
        }
        HidePhone();
        notification.SetHideTalkIconState();
    }

    public void SetCurTalk()
    {
        curTalk = talkList[talkListIdx];
    }
    public void SetCurTalk(int idx)
    {
        curTalk = talkList[idx];
    }
    //폰 보이기
    public void ShowPhone()
    {
        if (PlayerInfoData.instance != null && !PlayerInfoData.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing"))
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

        if(canPlayTalkSound && SceneManager.GetActiveScene().name != "Ending")
        {
            AudioManager.instance.StopSFX("Game Sound_카톡타자2");
            if (talk.talkText.preferredWidth > 500)
            {
                AudioManager.instance.SFXPlay("Game Sound_카톡타자2");
            }
            else if (talk.talkText.preferredWidth > 412.5)
            {
                AudioManager.instance.SFXPlayTime("Game Sound_카톡타자2", 1, 1, 60);
            }
            else if (talk.talkText.preferredWidth > 260)
            {
                AudioManager.instance.SFXPlayTime("Game Sound_카톡타자2", 1, 1, 40);
            }
            else
            {
                AudioManager.instance.SFXPlayTime("Game Sound_카톡타자2", 1, 1, 20);
            }
        }

        phoneTalkList.Add(talk.talkRT);

        //최근 톡의 사람이 지금 톡을 보낸 사람과 같은지여부
        bool isSameUser = lastTalk != null && lastTalk.userName == talk.userName;

        //이어지는 톡 설정
        talk.tail.SetActive(!isSameUser);

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

        phoneTalkList.Add(talk.talkRT);

        if (!isAnnouncement)
        {
            talk.userName = user.talkName;
        }

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

        lastTalk = talk;

        Invoke("ScrollToBottom", 0.03f);
        StartCoroutine(FitLayout(talkParentRT));

        notification.SetHideTalkIconState();
    }

    public void AddInOutTalk(bool isIn, string text)
    {
        TalkData talk = Instantiate(inOutTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);

        phoneTalkList.Add(talk.talkRT);
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

        talkScBar.value = 0f;
        isTalkNeedToBeSend = true;
        isOKSendTalk = true;
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

        isTalkNeedToBeSend = false;
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

        phoneTalkList.Add(talk.talkRT);
        //최근 톡의 사람이 지금 톡을 보낸 사람과 같은지여부
        bool isSameUser = lastTalk != null && lastTalk.userName == talk.userName;

        talk.profile.gameObject.SetActive(!isSameUser);
        talk.nameText.gameObject.SetActive(!isSameUser);
        talk.nameText.text = talk.userName;


        if (user.talkProfileSp != null)
        {
            talk.profileImage.sprite = user.talkProfileSp;
        }

        lastTalk = talk;

        Invoke("ScrollToBottom", 0.03f);
        StartCoroutine(FitLayout(talkParentRT));

        notification.SetHideTalkIconState();
    }

    public void AddVideoTalk(string user)
    {
        TalkData talk = Instantiate(videoTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);
        talk.userName = user;

        phoneTalkList.Add(talk.talkRT);
        //최근 톡의 사람이 지금 톡을 보낸 사람과 같은지여부
        bool isSameUser = lastTalk != null && lastTalk.userName == talk.userName;

        talk.profile.gameObject.SetActive(!isSameUser);
        talk.nameText.gameObject.SetActive(!isSameUser);
        talk.nameText.text = talk.userName;

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
        if(isOkStartTalk)
        {
            isOkStartTalk = false;
            StartCoroutine(OutputOtherUserTalk());
        }
    }
    public void StartTalkInTrigger()
    {
        if(isOkStartTalk)
        {
            StartCoroutine(OutputOtherUserTalk());
        }
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
        if (curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalk
                || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndRunEvent
                || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndRunNextTalk
                || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndStartTimeline
                || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndResumeTimeline)
        {
            if (curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndResumeTimeline)
            {
                TimelineManager.instance.timelineController.SetTimelinePause();
            }

            SetSendTalk(curTalk.answerTalk);
        }
        else if (curTalk.afterEndTalk == Talk.AfterEndTalk.StartTimeline)
        {
            TimelineManager.instance.timelineController.SetTimelineStart(curTalk.timelineName);
        }
        else if (curTalk.afterEndTalk == Talk.AfterEndTalk.ContinueTimeline)
        {
            TimelineManager.instance.timelineController.SetTimelineResume();
        }

        if (curTalk.afterEndTalk == Talk.AfterEndTalk.RunEvent
            && curTalk.runEvent != null)
        {
            curTalk.runEvent.Raise();
        }

        //다음 톡으로 설정
        if(curTalk.afterEndTalk == Talk.AfterEndTalk.None
            || curTalk.afterEndTalk == Talk.AfterEndTalk.StartTimeline
            || curTalk.afterEndTalk == Talk.AfterEndTalk.ContinueTimeline
            || curTalk.afterEndTalk == Talk.AfterEndTalk.RunEvent)
        {
            SetNextTalk();

            isTalkNeedToBeSend = false;
            //다른 톡을 시작 가능하게 설정
            isOkStartTalk = true;
        }
    }

    public void DeleteTalks()
    {
        StartCoroutine(IEDeleteTalks());
    }

    IEnumerator IEDeleteTalks()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        yield return new WaitForSeconds(1);
        for (int i = phoneTalkList.Count-1; i > -1; i--)
        {
            phoneTalkList[i].gameObject.SetActive(false);
            yield return wait;
        }
    }
    public void DeleteTalkAll()
    {
        foreach(Transform talkItem in talkParent.transform)
        {
            Destroy(talkItem.gameObject);
        }
        phoneTalkList.Clear();
    }


    //씬마다 남아있는 톡 설정
    public void SetRemainTalks(int idx)
    {
        canPlayTalkSound = false;

        Talk talk = talkList[0];
        talkselectionsIdx = 0;

        for (int i = 0; i < idx + 1; i++)
        {
            talk = talkList[i];

            while (talk.nextTalk != null && talk.nextTalk.Length > 0)
            {
                if (talk.TalkContexts.Count > 0)
                {
                    while (talk.TalkContexts.Count > talkIdx)
                    {
                        AddTalk(false, talk.TalkContexts[talkIdx].user, talk.TalkContexts[talkIdx++].talkText);
                    }
                }
                if (talk.answerTalk.Length > 0)
                {
                    if (talk.answerTalk.Length < 2)
                    {
                        curSendTalk = talk.answerTalk[0];
                    }
                    else
                    {
                        if (talk.answerTalk.Length > talkselectionsIdx)
                        {
                            curSendTalk = talk.answerTalk[talkSelections[talkselectionsIdx]];
                        }
                    }
                    AddTalk(curSendTalk.talkText);
                    while (curSendTalk.nextSendTalk != null)
                    {
                        if (curSendTalk.nextSendTalk != null)
                        {
                            SetNextSendTalk();
                        }
                        AddTalk(curSendTalk.talkText);
                    }
                }

                talkIdx = 0;
                if (talk.nextTalk != null && talk.nextTalk.Length > 0)
                {
                    if (talk.answerTalk.Length < 2)
                    {
                        talk = talk.nextTalk[0];
                    }
                    else
                    {
                        talk = talk.nextTalk[talkSelections[talkselectionsIdx]];
                        if (talkselectionsIdx < talkSelections.Length) { talkselectionsIdx++; }
                    }
                }
            }

            if (talk.TalkContexts.Count > 0)
            {
                while (talk.TalkContexts.Count > talkIdx)
                {
                    AddTalk(false, talk.TalkContexts[talkIdx].user, talk.TalkContexts[talkIdx++].talkText);
                }
            }
            if (talk.answerTalk.Length > 0)
            {
                curSendTalk = talk.answerTalk[0];
                AddTalk(curSendTalk.talkText);
                while (curSendTalk.nextSendTalk != null)
                {
                    if (curSendTalk.nextSendTalk != null)
                    {
                        SetNextSendTalk();
                    }
                    AddTalk(curSendTalk.talkText);
                }
            }

            if (i == 0 && child != null)
            {
                AddVideoTalk(child);
            }

            talkIdx = 0;
        }

        HideSendTalk();
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            talkInputArea.SetActive(false);
        }
        isTalkNeedToBeSend = false;
        IsOKSendTalk = false;
        isOkStartTalk = true;
        isPlayerFirstTalk = true;

        canPlayTalkSound = true;
    }

    IEnumerator IESetRemainTalks(int idx)
    {
        Talk talk = talkList[0];
        talkselectionsIdx = 0;

        for (int i = 0; i < idx+1; i++)
        {
            talk = talkList[i];

            while(talk.nextTalk != null && talk.nextTalk.Length > 0)
            {
                if (talk.TalkContexts.Count > 0)
                {
                    while (talk.TalkContexts.Count > talkIdx)
                    {
                        AddTalk(false, talk.TalkContexts[talkIdx].user, talk.TalkContexts[talkIdx++].talkText);
                        yield return null;
                    }
                }
                if (talk.answerTalk.Length > 0)
                {
                    if(talk.answerTalk.Length < 2)
                    {
                        curSendTalk = talk.answerTalk[0];
                    }
                    else
                    {
                        if(talk.answerTalk.Length > talkselectionsIdx)
                        {
                            curSendTalk = talk.answerTalk[talkSelections[talkselectionsIdx]];
                        }
                    }
                    AddTalk(curSendTalk.talkText);
                    while (curSendTalk.nextSendTalk != null)
                    {
                        if (curSendTalk.nextSendTalk != null)
                        {
                            SetNextSendTalk();
                        }
                        AddTalk(curSendTalk.talkText);
                        yield return null;
                    }
                }

                talkIdx = 0;
                if (talk.nextTalk != null && talk.nextTalk.Length > 0)
                {
                    if (talk.answerTalk.Length < 2)
                    {
                        talk = talk.nextTalk[0];
                    }
                    else
                    {
                        talk = talk.nextTalk[talkSelections[talkselectionsIdx]];
                        if (talkselectionsIdx < talkSelections.Length) { talkselectionsIdx++; }
                    }
                }
            }

            if (talk.TalkContexts.Count > 0)
            {
                while (talk.TalkContexts.Count > talkIdx)
                {
                    AddTalk(false, talk.TalkContexts[talkIdx].user, talk.TalkContexts[talkIdx++].talkText);
                    yield return null;
                }
            }
            if (talk.answerTalk.Length > 0)
            {
                curSendTalk = talk.answerTalk[0];
                AddTalk(curSendTalk.talkText);
                while (curSendTalk.nextSendTalk != null)
                {
                    if (curSendTalk.nextSendTalk != null)
                    {
                        SetNextSendTalk();
                    }
                    AddTalk(curSendTalk.talkText);
                    yield return null;
                }
            }

            if(i == 0 && child != null)
            {
                AddVideoTalk(child);
            }

            talkIdx = 0;
        }
        
        HideSendTalk();
        if(SceneManager.GetActiveScene().name == "Ending")
        {
            talkInputArea.SetActive(false);
        }
        isTalkNeedToBeSend = false;
        IsOKSendTalk = false;
        isOkStartTalk = true;
        isPlayerFirstTalk = true;

        canPlayTalkSound = true;
    }
}
