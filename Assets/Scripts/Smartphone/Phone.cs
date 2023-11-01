using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    //��� ���� ���� ù��° ���
    public List<Talk> talkList;
    int talkListIdx = 0;

    //�� ��ũ�� ����
    public ScrollRect talkScView;
    public Scrollbar talkScBar;
    //���� ������ ��
    public GameObject talkParent;
    RectTransform talkParentRT;
    public GameObject talkInputArea;
    RectTransform talkInputAreaRT;
    //�ڵ��� �κ��丮
    public InventoryController inven;
    public TalkNotification notification;
    //�ڵ��� �� ���
    Image phoneBgPanel;
    GameObject phoneFrame;

    //ȭ�鿡 ������ ���------------

    //�÷��̾��� ��
    public GameObject playerTalk;
    //Ÿ���� ��
    public GameObject anotherTalk;
    //���� ��
    public GameObject inOutTalk;
    //���� ��
    public GameObject announcementTalk;
    //���� ��
    public GameObject videoTalk;
    //�÷��̾ ���� �� �ִ� ��
    public GameObject sendTalk;

    //���� �ֱٿ� �� ��
    TalkData lastTalk;
    //���� �ֱٿ� �� �÷��̾� ��
    [HideInInspector] public TalkData lastPlayerTalk;
    //���� ��µ� ��
    public Talk curTalk;
    //���� ���� ��
    public SendTalk curSendTalk;

    //�� ���̴� ����
    bool isOpenPhone = false;

    bool isPlayerFirstTalk = true;
    bool isOKSendTalk = false;

    //���� ������ ���� ���ɿ���
    [HideInInspector]
    public bool isOkStartTalk = true;

    //�߰��� �ٸ� ���� �������ϴ� ��� ���� �뵵
    [HideInInspector]
    public bool isTalkNeedToBeSend = false;

    [SerializeField]
    float phoneShowSpeed = 0.1f;

    int TalkMemberNum = 5;
    //int sendTalkIdx = 0;
    //int receiveTalkIdx = 0;
    int showedCount = 0;
    //���õ� ������
    int selectedOption = 1;
    [HideInInspector] public int talkIdx = 0;

    public List<sendTalkData> sendTalkList;

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
    }

    private void Start()
    {
        SceneManager.sceneLoaded += LoadSceneEvent;
    }

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoadingScene" && scene.name != "Bathroom" && scene.name != "Bath")
        {
            StartCoroutine(SetCurTalkWhenSceneStart());
        }
    }

    IEnumerator SetCurTalkWhenSceneStart()
    {
        Scene sc = SceneManager.GetActiveScene();
        yield return new WaitWhile(() => sc.name == "LoadingScene");

        switch (sc.name)
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

    public void SetCurTalk()
    {
        curTalk = talkList[talkListIdx];
    }
    public void SetCurTalk(int idx)
    {
        curTalk = talkList[idx];
    }
    //�� ���̱�
    public void ShowPhone()
    {
        if (!PlayerInfoData.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing"))
        {
            PlayerInfoData.instance.playerAnim.SetBool("walk", false);
            PlayerInfoData.instance.playerAnim.SetBool("jump", false);
        }

        //���� �ֱ��� ����� ���̵��� ����
        phoneBgPanel.enabled = true;
        phoneFrame.SetActive(true);

        talkScBar.value = 0f;
        isOpenPhone = true;
        //�� �˸� ǥ�� ǥ�����̸� ��Ȱ��ȭ
        notification.SetHideTalkIconState();

        Invoke("ScrollToBottom", 0.03f);
    }
    //�� �����
    public void HidePhone()
    {
        //�κ��� ���������� �Բ� ����
        if (!inven.IsOpenInven) { inven.HideInven(); }
        phoneBgPanel.enabled = false;
        phoneFrame.SetActive(false);
        isOpenPhone = false;
    }

    //�� �߰�
    public void AddTalk(string text)
    {
        TalkData talk = Instantiate(playerTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);
        talk.talkText.text = text;

        //�ֱ� ���� ����� ���� ���� ���� ����� ����������
        bool isSameUser = lastTalk != null && lastTalk.userName == talk.userName;

        //�̾����� �� ����
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

        if (!isAnnouncement) talk.userName = user.talkName;

        //�ֱ� ���� ����� ���� ���� ���� ����� ����������
        bool isSameUser = lastTalk != null && lastTalk.userName == talk.userName;

        //�̾����� �� ����
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

        if (isIn)
        {
            talk.talkText.text = text + "���� ���Խ��ϴ�.";
        }
        else
        {
            talk.talkText.text = text;
        }

        lastTalk = talk;

        Invoke("ScrollToBottom", 0.03f);
        StartCoroutine(FitLayout(talkParentRT));
    }

    //�÷��̾ ���� �� ������ �߰��ϱ�
    public void AddSendTalk()
    {
        isPlayerFirstTalk = true;
        SetSendTalk(curTalk.answerTalk);
        isOKSendTalk = true;
    }

    //���� �ֱ� ���� �� ���̱�
    void ScrollToBottom()
    {
        talkScBar.value = 0.0001f;
    }

    //�÷��̾ ���� �� ������ ����
    public void SetSendTalk(SendTalk[] SendTalkContexts)
    {
        // isSendTalkReady = true;

        //����� ���� �� ������ ����
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

        isTalkNeedToBeSend = true;
        isOKSendTalk = true;
    }

    //�÷��̾ ���� ���� 1���� ���
    public void SetSendTalk(SendTalk SendTalkContexts)
    {
        // isSendTalkReady = true;

        //����� ���� �� ������ ����
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

        //if (curSendTalk == null && curTalk.isInTimeline)
        //{
        //    TimelineManager.instance.timelineController.SetTimelineResume();
        //}
    }

    public void SelectTalk(int value)
    {
        selectedOption += value;

        for(int i = 0; i < showedCount;i++)
        {
            //���� ���� ǥ��
            sendTalkList[i].talkText.color = Color.gray;
        }
        //���õǾ� �ִ� ���� ǥ��
        sendTalkList[selectedOption - 1].talkText.color = Color.black;
    }

    public void AddVideoTalk(Speaker user)
    {
        TalkData talk = Instantiate(videoTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);
        talk.userName = user.talkName;

        //�ֱ� ���� ����� ���� ���� ���� ����� ����������
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
    public void SetNextSendTalk()
    {
        isPlayerFirstTalk = false;
        curSendTalk = curSendTalk.nextSendTalk;
        SetSendTalk(curSendTalk);
    }

    //���� �� ����
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
    //���̾ƿ� ���� �ؼ�
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

    //�� ����
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

    //�� ���
    IEnumerator OutputOtherUserTalk()
    {
        float delayTime = 0;

        notification.SetShowTalkIconState();

        //���� ��ȭ ���
        while (curTalk.TalkContexts.Count > talkIdx)
        {
            delayTime = curTalk.TalkContexts[talkIdx].TalkSendDelay;
            yield return new WaitForSeconds(delayTime);
            SmartphoneManager.instance.phone.AddTalk(false, curTalk.TalkContexts[talkIdx].user, curTalk.TalkContexts[talkIdx++].talkText);
        }

        yield return new WaitForSeconds(1.0f);

        //��� ������ ��ȭ�� ������ �� �� ����Ǵ� ��
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

        if ((curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndRunEvent
            || curTalk.afterEndTalk == Talk.AfterEndTalk.RunEvent)
            && curTalk.runEvent != null)
        {
            curTalk.runEvent.Raise();
        }

        //���� ������ ����
        if(curTalk.afterEndTalk == Talk.AfterEndTalk.None
            || curTalk.afterEndTalk == Talk.AfterEndTalk.StartTimeline
            || curTalk.afterEndTalk == Talk.AfterEndTalk.ContinueTimeline
            || curTalk.afterEndTalk == Talk.AfterEndTalk.RunEvent)
        {
            SetNextTalk();

            isTalkNeedToBeSend = false;
            //�ٸ� ���� ���� �����ϰ� ����
            isOkStartTalk = true;
        }
    }
}
