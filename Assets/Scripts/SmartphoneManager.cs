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
    //인벤 선택란
    public Image[] invenOption;
    //톡 스크롤 영역
    public ScrollRect talkScR;

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
        picsInven = picsInvenObj.GetComponent<Inventory>();
        filesInven = filesInvenObj.GetComponent<Inventory>();
    }
    private void Update() {
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
                    talkScR.verticalNormalizedPosition += talkScrollSpeed;
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
                    talkScR.verticalNormalizedPosition -= talkScrollSpeed;
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
                    //선택 해제 표시
                    filesInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;

                    selectedOption-=2;

                    //선택되어 있는 상태 표시
                    filesInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.gray;
                }
                if(isOpenPictures && maxPicSlot!=0)
                {
                    //선택 해제 표시
                    picsInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;

                    selectedOption-=2;

                    //선택되어 있는 상태 표시
                    picsInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.gray;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            //얻은 아이템 항목 선택
            if(isOpenFiles && maxFilesSlot!=0 && selectedOption<maxFilesSlot-1)
            {
                //선택 해제 표시
                filesInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;

                selectedOption+=2;

                //선택되어 있는 상태 표시
                filesInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.gray;
            }
            if(isOpenPictures && maxPicSlot!=0 && selectedOption<maxPicSlot-1)
            {
                //선택 해제 표시
                picsInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;

                selectedOption+=2;

                //선택되어 있는 상태 표시
                picsInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.gray;
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
                    //선택 해제 표시
                    filesInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;
                    
                    selectedOption--;

                    //선택되어 있는 상태 표시
                    filesInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.gray;
                }
                if(isOpenPictures && maxPicSlot!=0)
                {
                    //선택 해제 표시
                    picsInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;
                    
                    selectedOption--;

                    //선택되어 있는 상태 표시
                    picsInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.gray;
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
                    //선택 해제 표시
                    filesInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;
                    
                    selectedOption++;

                    //선택되어 있는 상태 표시
                    filesInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.gray;
                }
                if(isOpenPictures && maxPicSlot!=0 && maxPicSlot!=selectedOption)
                {
                    //선택 해제 표시
                    picsInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.white;
                    
                    selectedOption++;

                    //선택되어 있는 상태 표시
                    picsInven.slotList[selectedOption-1].GetComponent<Image>().color = Color.gray;
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
    public void SetFilesActive(bool state)
    {
        if(state)
        {
            //선택되어 있는 상태 표시
            //획득한 있는 경우
            if(maxFilesSlot!=0)
            {
                filesInven.slotList[0].GetComponent<Image>().color = Color.gray;
                selectedOption = 1;
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
    public void SetPicsActive(bool state)
    {
        if(state)
        {
            //선택되어 있는 상태 표시
            //획득한 있는 경우
            if(maxPicSlot!=0)
            {
                picsInven.slotList[0].GetComponent<Image>().color = Color.gray;
                selectedOption = 1;
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
        talkScR.verticalNormalizedPosition = 0f;
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
    public void SendTalk()
    {
        print("send talk");
    }
    //아이템 설정
    //아이템 획득시 호출
    public void SetItemToInven(bool isFile, int index, GotItemData gotItem)
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
}
