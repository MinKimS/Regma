using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartphoneManager : MonoBehaviour
{
    public static SmartphoneManager instance;
    //핸드폰 ui가 있는 캔버스
    public Canvas phoneCanvas;
    //핸드폰 인벤토리
    public GameObject phoneInven;
    //파일 인벤토리
    public GameObject filesInvenObj;
    //사진 인벤토리
    public GameObject picsInvenObj;
    //톡이 나오는 곳
    public GameObject talkParent;
    private RectTransform talkParentRT;

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
    public GameObject InOutTalk;
    //가장 최근에 온 톡
    public TalkData LastTalk;

    //인벤정보
    private Inventory picsInven;
    private Inventory filesInven;

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

        //인벤 스크롤 시 사용되는 수치
        invenOriginPos = new Vector2(0f, -630f);
        invenUpValue = new Vector2(0f,-300f);
        invenDownValue = new Vector2(0f,300f);

        HidePhone();

        talkParentRT=talkParent.GetComponent<RectTransform>();
    }
    private void Update() {
        //테스트
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            //AddTalk(true, "00000333330304 45r");
            
            AddTalk(false, "LALALALALa", null);
        }

        //폰 열기
        if(Input.GetKeyDown(KeyCode.P)||(Input.GetKeyDown(KeyCode.Escape)&&!isOpenInven&&isOpenPhone))
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
        if(Input.GetKeyDown(KeyCode.I)||(Input.GetKeyDown(KeyCode.Escape)&&isOpenInven))
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
                print(filesInven.slotDataList[selectedOption-1].item.itemName);
            }
            else if(isOpenPictures && maxPicSlot!=0)
            {
                print(picsInven.slotDataList[selectedOption-1].item.itemName);
            }
        }
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            //서랍창 나가기
            if(isOpenFiles) {SetFilesActive(false);}
            if(isOpenPictures) {SetPicsActive(false);}
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
    private void ShowPhone()
    {
        //가장 최근의 톡부터 보이도록 설정
        phoneCanvas.gameObject.SetActive(true);
        // talkScR.verticalNormalizedPosition = 0f;
        talkScBar.value = 0f;
        isOpenPhone = true;
    }
    //폰 숨기기
    private void HidePhone()
    {
        //인벤이 켜져있으면 함께 끄기
        if(!isOpenInven) {HideInven();}
        phoneCanvas.gameObject.SetActive(false);
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
    //카톡보내기
    private void SendTalk()
    {
        print("send talk");
    }
    //아이템 설정
    //아이템 획득시 호출
    private void SetItemToInven(bool isFile, int index, GotItemData gotItem)
    {
        //인벤에 보이는 이미지 설정
        if(isFile)
        {
            filesInven.gotItemList.Add(gotItem);
            filesInven.slotList[index].GetComponent<Image>().sprite=filesInven.slotDataList[index].item.itemSprite;
        }
        else
        {
            picsInven.gotItemList.Add(gotItem);
            picsInven.slotList[index].GetComponent<Image>().sprite=picsInven.slotDataList[index].item.itemSprite;
        }
    }
    
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
    private void AddTalk(bool isPlayer, string text, Sprite sp)
    {
        bool isBottom = talkScBar.value <= 0.0001f;
        
        TalkData talk = Instantiate(isPlayer ? playerTalk : anotherTalk).GetComponent<TalkData>();
        talk.transform.SetParent(talkParent.transform, false);
        talk.talkText.text = text;

        //최근 톡의 사람이 지금 톡을 보낸 사람과 같은지여부
        bool isSameUser = LastTalk != null && LastTalk.userName == talk.userName;
        
        //이어지는 톡 설정
        talk.tail.SetActive(!isSameUser);
        if(!isPlayer)
        {
            talk.profileImage.gameObject.SetActive(!isSameUser);
            talk.nameText.gameObject.SetActive(!isSameUser);
            talk.nameText.text = talk.name;
            if(sp!=null)
            {
                talk.profileImage.sprite = sp;
            }
        }

        LastTalk = talk;

        StartCoroutine(FitLayout(talkParentRT, 0.03f));

        //플레이어 톡이 아닌경우
        if(!isPlayer&&!isBottom) {return;}
        Invoke("ScrollToBottom", 0.03f);
    }

    //레이아웃 버그 해소
    IEnumerator FitLayout(RectTransform rt, float time)
    {
        yield return new WaitForSeconds(time);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
    }

    //가장 최근 본인 톡 보이기
    void ScrollToBottom()
    {
        talkScBar.value = 0f;
    }
}
