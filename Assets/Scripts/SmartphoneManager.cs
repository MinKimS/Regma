using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartphoneManager : MonoBehaviour
{
    public static SmartphoneManager instance;
    public TalkNotification notification;
    //핸드폰 ui가 있는 캔버스
    public Canvas phoneCanvas;
    //핸드폰
    public GameObject phonePanel;
    //핸드폰 인벤토리
    public GameObject phoneInven;
    //파일 인벤토리
    public GameObject filesInvenObj;
    //사진 인벤토리
    public GameObject picsInvenObj;
    //톡이 나오는 곳
    public GameObject talkParent;
    private RectTransform talkParentRT;
    public bool isOKSendTalk = false;

    //인벤 선택란
    public Image[] invenOption;
    //톡 스크롤 영역
    public ScrollRect talkScR;
    public Scrollbar talkScBar;
    //플레이어의 톡
    public GameObject playerTalk;
    //타인의 톡
    public GameObject anotherTalk;
    //입장 톡
    public GameObject inOutTalk;
    //공지 톡
    public GameObject announcementTalk;
    //플레이어가 보낼 수 있는 톡
    public GameObject sendTalk;
    //가장 최근에 온 톡
    public TalkData lastTalk;
    //가장 최근에 온 플레이어 톡
    public TalkData lastPlayerTalk;
    //톡을 보내는 영역
    public GameObject talkInputArea;
    private int TalkMemberNum = 5;
    public List<sendTalkData> sendTalkList;
    //보낼 톡 내용들
    public List<string> sendTalkContentList = new List<string>();
    //받을 톡 내용들
    public List<List<string>> receiveTalkContentList = new List<List<string>>();
    public int sendTalkIdx = 0;
    public int receiveTalkIdx = 0;
    RectTransform talkInputAreaRT;
    int showedCount = 0;
    public RectTransform inputArea;

    //인벤정보
    [HideInInspector]
    public Inventory picsInven;
    [HideInInspector]
    public Inventory filesInven;

    //폰 보이는 여부
    private bool isOpenPhone = false;
    //인벤 보이는 여부
    private bool isOpenInven = false;
    //인벤의 파일 보이는 여부
    private bool isOpenFiles = false;
    //인벤의 사진 보이는 여부
    private bool isOpenPictures = false;

    //톡 스크롤되는 속도
    public float talkScrollSpeed = 0.05f;
    //파일과 사진 선택
    private int selectedInvenOption = 0;
    //선택된 아이템
    private int selectedOption = 1;
    //얻은 사진 아이템 수
    public int maxPicSlot = 0;
    //얻은 파일 아이템 수
    public int maxFilesSlot = 0;
    //인벤의 현재 선택된 슬롯이 화면상 첫번째 라인인가
    private bool isFirstLine = false;
    //인벤의 현재 선택된 슬롯이 화면상 마지막 라인인가
    private bool isLastLine = false;
    //인벤의 첫번째 라인의 첫번째 슬롯 번호
    private int firstLineFirstNum = 1;
    //인벤의 마지막 라인의 첫번째 슬롯 번호
    private int lastLineFirstNum = 5;
    //인벤 스크롤 시 사용되는 수치
    Vector2 invenOriginPos;
    Vector2 invenUpValue;
    Vector2 invenDownValue;

    //플레이어가 보내는 톡을 보낼 준비가 되었는지 여부
    bool isSendTalkReady = false;

    public bool IsOpenPhone
    {
        get{return isOpenPhone;}
    }

    public bool IsOpenInven
    {
        get{return isOpenInven;}
    }

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            DontDestroyOnLoad(phoneCanvas);
        }
        else Destroy(gameObject);
    }
    private void Start() {
        picsInven = picsInvenObj.GetComponentInChildren<Inventory>();
        filesInven = filesInvenObj.GetComponentInChildren<Inventory>();
        talkInputAreaRT = talkInputArea.GetComponent<RectTransform>();

        //인벤 스크롤 시 사용되는 수치
        invenOriginPos = new Vector2(15f, -15f);
        invenUpValue = new Vector2(0f,-70);
        invenDownValue = new Vector2(0f,70f);

        HidePhone();

        SetList();

        talkParentRT=talkParent.GetComponent<RectTransform>();
    }
    private void Update() {
        if((TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop) &&DialogueManager.instance._dlgState == DialogueManager.DlgState.End && !filesInven.IsInvenItemActive)
        {
            //폰 열기
            if((Input.GetKeyDown(KeyCode.P)||(Input.GetKeyDown(KeyCode.Escape)&&!isOpenInven&&isOpenPhone))&&!isSendTalkReady)
            {
                if(!isOpenPhone)
                {
                    ShowPhone();
                }
                else
                {
                    if(isOpenInven){HideInven();}
                    HidePhone();
                }
            }
            //인벤 열기
            if((Input.GetKeyDown(KeyCode.I)||(Input.GetKeyDown(KeyCode.Escape)&&isOpenInven))&&!isSendTalkReady)
            {
                if(!isOpenInven)
                {
                    if(!isOpenPhone)
                    {
                        ShowPhone();
                        ShowInven();
                    }
                    else
                    {
                        ShowInven();
                    }
                }
                else
                {
                    HideInven();
                    HidePhone();
                }
            }
        }

        if(filesInven.IsInvenItemActive && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            filesInven.HideDiary();
        }

        if(!filesInven.IsInvenItemActive)
        {
            //화살표키==================================
            if(Input.GetKey(KeyCode.UpArrow))
            {
                //톡 화면 스크롤
                if(isOpenPhone&&!isOpenInven)
                {
                    if(talkScR.verticalNormalizedPosition < 1f)
                    {
                        //talkScR.verticalNormalizedPosition += talkScrollSpeed;
                        talkScBar.value += talkScrollSpeed;
                    }
                }
            }
            if(Input.GetKey(KeyCode.DownArrow))
            {
                //톡 화면 스크롤
                if(isOpenPhone&&!isOpenInven)
                {
                    if(talkScR.verticalNormalizedPosition > 0f)
                    {
                        //talkScR.verticalNormalizedPosition -= talkScrollSpeed;
                        talkScBar.value -= talkScrollSpeed;
                    }
                }
            }
            
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(isOpenPhone&&!isOpenInven&&selectedOption > 1)
                {
                    SelectTalk(-1);
                }
                //얻은 아이템 항목 선택
                if(selectedOption>2)
                {
                    if(isOpenFiles && maxFilesSlot!=0)
                    {
                        SetSelectInvenItem(-2, filesInven);

                        //만약 화면상 첫번째 라인이면 다음 라인 아래로 보이기
                        if(isFirstLine)
                        {
                            ShowOutLine(filesInven, -2, true);
                        }
                        //화면상 첫번째 라인이지 체크
                        if(selectedOption == firstLineFirstNum || selectedOption == firstLineFirstNum+1)
                        {
                            isFirstLine = true;
                        } 
                    }
                    if(isOpenPictures && maxPicSlot!=0)
                    {
                        SetSelectInvenItem(-2, picsInven);

                        //만약 화면상 첫번째 라인이면 다음 라인 아래로 보이기
                        if(isFirstLine)
                        {
                            ShowOutLine(picsInven, -2, true);
                        }
                        //화면상 첫번째 라인이지 체크
                        if(selectedOption == firstLineFirstNum || selectedOption == firstLineFirstNum+1)
                        {
                            isFirstLine = true;
                        }
                    }
                }
                if(isLastLine)
                {
                    isLastLine=false;
                }
            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(isOpenPhone&&!isOpenInven&&selectedOption < showedCount)
                {
                    SelectTalk(1);
                }
                //얻은 아이템 항목 선택
                if(isOpenFiles && maxFilesSlot!=0 && selectedOption<maxFilesSlot-1)
                {  
                    SetSelectInvenItem(2, filesInven);
                        
                    //만약 화면상 마지막 라인이면 다음 라인 위로 보이기
                    if(isLastLine)
                    {
                        ShowOutLine(filesInven, 2, false);
                    }
                    //화면상 마지막 라인이지 체크
                    if(selectedOption == lastLineFirstNum || selectedOption == lastLineFirstNum+1)
                    {
                        isLastLine = true;
                    } 
                }
                if(isOpenPictures && maxPicSlot!=0 && selectedOption<maxPicSlot-1)
                {   
                    SetSelectInvenItem(2, picsInven);

                    //만약 화면상 마지막 라인이면 다음 라인 위로 보이기
                    if(isLastLine)
                    {
                        ShowOutLine(picsInven, 2, false);
                    }
                    //화면상 마지막 라인이지 체크
                    if(selectedOption == lastLineFirstNum || selectedOption == lastLineFirstNum+1)
                    {
                        isLastLine = true;
                    }  
                }
                if(isFirstLine)
                {
                    isFirstLine=false;
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //파일 선택
                if(isOpenInven&&!isOpenFiles&&!isOpenPictures)
                {
                    selectedInvenOption = 0;
                    invenOption[1].color = Color.white;
                    invenOption[0].color = Color.gray;
                }
                //얻은 아이템 항목 선택
                if(selectedOption%2==0 && (isOpenFiles||isOpenPictures))
                {
                    if(isOpenFiles && maxFilesSlot!=0)
                    {
                        SetSelectInvenItem(-1, filesInven);
                    }
                    if(isOpenPictures && maxPicSlot!=0)
                    {
                        //선택 해제 표시
                        SetSelectInvenItem(-1, picsInven);
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                //사진 선택
                if(isOpenInven&&!isOpenFiles&&!isOpenPictures)
                {
                    selectedInvenOption = 1;
                    invenOption[0].color = Color.white;
                    invenOption[1].color = Color.gray;
                }
                //얻은 아이템 항목 선택
                if(selectedOption%2!=0 && (isOpenFiles||isOpenPictures))
                {
                    //얻은 아이템의 수에 따라 항목 이동 가능여부 결정
                    if(isOpenFiles && maxFilesSlot!=0 && maxFilesSlot!=selectedOption)
                    {
                        SetSelectInvenItem(1, filesInven);
                    }
                    if(isOpenPictures && maxPicSlot!=0 && maxPicSlot!=selectedOption)
                    {
                        SetSelectInvenItem(1, picsInven);
                    }
                }
            }
            //화살표키---------------------

            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(isOpenInven&&!isOpenFiles&&!isOpenPictures)
                {
                    if(selectedInvenOption==0)
                    {
                        SetFilesActive(true);
                        SetPicsActive(false);
                    }
                    if(selectedInvenOption==1)
                    {
                        SetPicsActive(true);
                        SetFilesActive(false);
                    }
                }
                else if(isOpenFiles && maxFilesSlot!=0)
                {
                    //일기장
                    if(filesInven.slotDataList[selectedOption-1].item.itemID == 0)
                    {
                        filesInven.ShowDiary();
                    }
                }
                else if(isOpenPictures && maxPicSlot!=0)
                {
                    print(picsInven.slotDataList[selectedOption-1].item.itemName);
                }
                //톡 선택
                if(isOpenPhone&&!isOpenInven&& isOKSendTalk)
                {
                    AddTalk(sendTalkList[selectedOption-1].text);
                    HideSendTalk();
                }
            }
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                //서랍창 나가기
                if(isOpenFiles) {SetFilesActive(false);}
                if(isOpenPictures) {SetPicsActive(false);}
            }
        }
    }

    //파일함 보이는 여부 설정
    private void SetFilesActive(bool state)
    {
        if(state)
        {
            //선택되어 있는 상태 표시
            //획득한 있는 경우
            if(maxFilesSlot!=0)
            {
                filesInven.slotList[0].GetComponent<Image>().color = Color.gray;
                selectedOption = 1;
                lastLineFirstNum = 5;
                firstLineFirstNum = 1;
                isLastLine=false;
                isFirstLine=true;
                filesInven.slotRT.anchoredPosition = invenOriginPos;
            }
        }
        else
        {
            filesInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;
        }
        filesInvenObj.gameObject.SetActive(state);
        isOpenFiles = state;
    }
    //사진함 보이는 여부 설정
    private void SetPicsActive(bool state)
    {
        if(state)
        {
            //선택되어 있는 상태 표시
            //획득한 있는 경우
            if(maxPicSlot!=0)
            {
                picsInven.slotList[0].GetComponent<Image>().color = Color.gray;
                selectedOption = 1;
                lastLineFirstNum = 5;
                firstLineFirstNum = 1;
                isLastLine=false;
                isFirstLine=true;
                picsInven.slotRT.anchoredPosition = invenOriginPos;
            }
        }
        else
        {
            picsInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;
            invenOption[0].color = Color.gray;
            invenOption[1].color = Color.white;
            selectedInvenOption = 0;
        }

        picsInvenObj.gameObject.SetActive(state);
        isOpenPictures = state;
    }
    //폰 보이기
    public void ShowPhone()
    {
        //톡 알림 표시 표시중이면 비활성화
        notification.SetHideTalkIconState();

        if(!DialogueManager.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing"))
        {
            DialogueManager.instance.playerAnim.SetBool("walk", false);
            DialogueManager.instance.playerAnim.SetBool("jump", false);
        }

        //가장 최근의 톡부터 보이도록 설정
        phonePanel.gameObject.SetActive(true);
        // talkScR.verticalNormalizedPosition = 0f;
        talkScBar.value = 0f;
        isOpenPhone = true;
    }
    //폰 숨기기
    public void HidePhone()
    {
        //인벤이 켜져있으면 함께 끄기
        if(!isOpenInven) {HideInven();}
        phonePanel.gameObject.SetActive(false);
        isOpenPhone = false;
    }
    //인벤 보이기
    private void ShowInven()
    {
        invenOption[1].color = Color.white;
        invenOption[0].color = Color.gray;
        phoneInven.gameObject.SetActive(true);
        isOpenInven = true;
    }
    //인덴 숨기기
    private void HideInven()
    {
        //파일이나 사진이 열려있으면 닫기
        if(isOpenFiles) {SetFilesActive(false);}
        if(isOpenPictures) {SetPicsActive(false);}

        phoneInven.gameObject.SetActive(false);
        isOpenInven = false;
    }
    // //아이템 설정
    // //아이템 획득시 호출
    // private void SetItemToInven(bool isFile, int index, GotItemData gotItem)
    // {
    //     //인벤에 보이는 이미지 설정
    //     if(isFile)
    //     {
    //         filesInven.gotItemList.Add(gotItem);
    //         filesInven.slotList[index].GetComponent<Image>().sprite=filesInven.slotDataList[index].item.itemSprite;
    //     }
    //     else
    //     {
    //         picsInven.gotItemList.Add(gotItem);
    //         picsInven.slotList[index].GetComponent<Image>().sprite=picsInven.slotDataList[index].item.itemSprite;
    //     }
    // }
    
    //선택된 슬롯 변경
    private void SetSelectInvenItem(int value, Inventory inven)
    {
        //선택 해제 표시
        inven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;

        selectedOption+=value;

        //선택되어 있는 상태 표시
        inven.slotList[selectedOption-1].GetComponent<Image>().color = Color.gray;
    }
    //화면밖에 있는 라인 보이기
    private void ShowOutLine(Inventory inven, int value, bool isUp)
    {
        if(isUp) {inven.slotRT.anchoredPosition += invenUpValue;}
        else {inven.slotRT.anchoredPosition += invenDownValue;}
        firstLineFirstNum+=value;
        lastLineFirstNum+=value;
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

        StartCoroutine(FitLayout(talkParentRT, 0.03f));
        StartCoroutine(FitLayout(talkInputAreaRT, 0.03f));

        if(TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
        {
            TimelineManager.instance.SetTimelineResume();
        }
    }

    public void AddTalk(bool isAnnouncement, Speaker user, string text)
    {
        TalkData talk = Instantiate(isAnnouncement ? announcementTalk : anotherTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);
        talk.talkText.text = text;
        if(!isAnnouncement) talk.userName = user.talkName;

        //최근 톡의 사람이 지금 톡을 보낸 사람과 같은지여부
        bool isSameUser = lastTalk != null && lastTalk.userName == talk.userName;
        
        //이어지는 톡 설정
        talk.tail.SetActive(!isSameUser);
        if(!isAnnouncement)
        {   
            talk.profile.gameObject.SetActive(!isSameUser);
            talk.nameText.gameObject.SetActive(!isSameUser);
            talk.nameText.text = talk.userName;


            if(user.talkProfileSp!=null)
            {
                talk.profileImage.sprite = user.talkProfileSp;
            }
        }
        talk.readNum = TalkMemberNum - 2;
        talk.readNumText.text = talk.readNum.ToString();

        lastTalk = talk;

        Invoke("ScrollToBottom", 0.03f);
        StartCoroutine(FitLayout(talkParentRT, 0.03f));

        //카톡을 안보고있을 때 톡이 온경우 톡이 온것을 알림
        notification.SetShowTalkIconState();
    }

    public void AddInOutTalk(bool isIn, string text)
    {        
        TalkData talk = Instantiate(inOutTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);

        if(isIn)
        {
            talk.talkText.text = text + "님이 들어왔습니다.";
        }
        else
        {
            talk.talkText.text = text;
        }

        lastTalk = talk;

        Invoke("ScrollToBottom", 0.03f);
        StartCoroutine(FitLayout(talkParentRT, 0.03f));
    }

    //레이아웃 버그 해소
    IEnumerator FitLayout(RectTransform rt, float time)
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
    }

    //가장 최근 본인 톡 보이기
    void ScrollToBottom()
    {
        talkScBar.value = 0.0001f;
    }

    public void SetSendTalk(int count)
    {
        isSendTalkReady = true;
        for(int i = 0; i < count; i++)
        {
            sendTalkData sendTalk = sendTalkList[i];
            sendTalk.gameObject.SetActive(true);
            sendTalk.text = sendTalkContentList[sendTalkIdx];
            sendTalk.talkText.text = sendTalkList[i].text;
            sendTalkList[i].talkText.enabled = true;
            sendTalkIdx++;
        }
        showedCount = count;

        sendTalkList[0].talkColor = Color.white;
        selectedOption = 1;

        StartCoroutine(FitLayout(talkInputAreaRT, 0.03f));
        StartCoroutine(FitLayout(inputArea, 0.03f));
    }

    public void HideSendTalk()
    {
        for(int i =0; i< showedCount; i++)
        {
            sendTalkList[i].talkText.text = "";
            sendTalkList[i].talkText.enabled = false;
            sendTalkList[i].gameObject.SetActive(false);
            isOKSendTalk = false;
        }
        StartCoroutine(FixLayoutGroup());

        isSendTalkReady = false;

        TimelineManager.instance.SetTimelineResume();
    }
    
    IEnumerator FixLayoutGroup()
    {
        VerticalLayoutGroup vlg = talkInputArea.GetComponent<VerticalLayoutGroup>();
        vlg.enabled = false;
        yield return new WaitForEndOfFrame();
        vlg.enabled = true;
    }

    public void SelectTalk(int value)
    {
        //선택 해제 표시
        sendTalkList[selectedOption-1].talkColor = Color.white;
        selectedOption+=value;

        //선택되어 있는 상태 표시
        sendTalkList[selectedOption-1].talkColor = Color.gray;
    }

    private void SetList()
    {
        sendTalkContentList.Add("다들 어디에요?");
        sendTalkContentList.Add("보면 어딘지 말하세요");
        sendTalkContentList.Add("누구세요?");
        sendTalkContentList.Add("아호 님! 나는 거실이에요.");
        sendTalkContentList.Add("현관문이고 뭐고 아무것도 안보여요.");
        sendTalkContentList.Add("아호님 소리 못들었어요.");
        sendTalkContentList.Add("나도 소리 쳤는데;;");
        sendTalkContentList.Add("서로 소리가 안들리나봐요.");
        sendTalkContentList.Add("네");
        sendTalkContentList.Add("너 대체 누구냐고?");

        receiveTalkContentList.Add(new List<string> {"익명", "안녕?"});
        receiveTalkContentList.Add(new List<string> {"익명", "반가워"});
        receiveTalkContentList.Add(new List<string> {"아호", "다들 어딨어요?"});
        receiveTalkContentList.Add(new List<string> {"아호", "나 여기 주방이고"});
        receiveTalkContentList.Add(new List<string> {"아호", "문 앞에 서있는데"});
        receiveTalkContentList.Add(new List<string> {"아호", "어디로 가는건지\n모르겠어요."});
        receiveTalkContentList.Add(new List<string> {"아호", "도원 님이에요?"});
        receiveTalkContentList.Add(new List<string> {"아호", "소리 지르고 있는데\n안 들려요?"});
        receiveTalkContentList.Add(new List<string> {"아호", "우선 빨리 열어보죠."});
        receiveTalkContentList.Add(new List<string> {"아호", "저도 여는 방법 찾아볼테니까\n도원 님도 찾아봐요."});
        receiveTalkContentList.Add(new List<string> {"익명", "오늘도 일기 써야하는데…\n안 쓰면 혼나.."});
    }
}
