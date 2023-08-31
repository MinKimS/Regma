using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
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

    //�� ���̱�
    public void ShowPhone()
    {
        if (!DialogueManager.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing"))
        {
            DialogueManager.instance.playerAnim.SetBool("walk", false);
            DialogueManager.instance.playerAnim.SetBool("jump", false);
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
        isOKSendTalk = true;

        //����� ���� �� ������ ����
        if (SendTalkContexts != null)
        {
            for (int i = 0; i < SendTalkContexts.Length; i++)
            {
                sendTalkData sendTalk = sendTalkList[i];
                sendTalk.gameObject.SetActive(true);
                sendTalk.talkText.text = sendTalk.text = SendTalkContexts[i].talkText;
                sendTalkList[i].talkText.enabled = true;
            }
        }
        showedCount = SendTalkContexts.Length;

        sendTalkList[0].talkColor = Color.white;
        selectedOption = 1;

        StartCoroutine(FitLayout(talkInputAreaRT));
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

        sendTalkList[0].talkColor = Color.white;
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

        if (curSendTalk.nextSendTalk == null && curTalk.isInTimeline)
        {
            TimelineManager.instance.SetTimelineResume();
        }
    }

    public void SelectTalk(int value)
    {
        //���� ���� ǥ��
        sendTalkList[selectedOption - 1].talkColor = Color.white;
        selectedOption += value;

        //���õǾ� �ִ� ���� ǥ��
        sendTalkList[selectedOption - 1].talkColor = Color.gray;
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

    //���� �� ����
    public void SetNextTalk()
    {
        talkIdx = 0;
        isPlayerFirstTalk = true;
        if (curTalk.nextTalk != null)
        {
            curTalk = curTalk.nextTalk;
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
        StartCoroutine(OutputOtherUserTalk());
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
        if (curTalk.afterEndTalk == Talk.AfterEndTalk.StartTimeline)
        {
            delayTime = curTalk.TalkContexts[talkIdx - 1].TalkSendDelay;
            yield return new WaitForSeconds(1.0f);
            TimelineManager.instance.SetTimelineStart(curTalk.timelineName);
        }
        if (curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalk || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndStartTimeline || curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndRunEvent)
        {
            if (curTalk.isInTimeline) { TimelineManager.instance.SetTimelinePause(); }
            yield return new WaitForSeconds(1.0f);
            SmartphoneManager.instance.phone.SetSendTalk(curTalk.answerTalk);
        }
        if (curTalk.afterEndTalk == Talk.AfterEndTalk.ContinueTimeline)
        {
            TimelineManager.instance.SetTimelineResume();
        }

        if (curTalk.answerTalk.Length > 1)
            SmartphoneManager.instance.phone.SetNextTalk();
    }
}
