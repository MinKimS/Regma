using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartphoneManager : MonoBehaviour
{
    public static SmartphoneManager instance;

    [HideInInspector] public TalkNotification notification;

    [HideInInspector] public Phone phone;
    //핸드폰 인벤토리
    [HideInInspector] public InventoryController inven;

    //톡 스크롤되는 속도
    public float talkScrollSpeed = 0.05f;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

        phone = GetComponentInChildren<Phone>();
        inven = phone.GetComponentInChildren<InventoryController>();
        notification = GetComponentInChildren<TalkNotification>();

    }
    private void Start() {
        inven.HideInven();
        phone.HidePhone();

    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LoadingManager.LoadScene("Kitchen");
        }

        if((TimelineManager.instance._Tlstate == TimelineManager.TlState.End) &&DialogueManager.instance._dlgState == DialogueManager.DlgState.End && !inven.filesInven.IsInvenItemActive)
        {
            //폰 열기
            if((Input.GetKeyDown(KeyCode.P)||(Input.GetKeyDown(KeyCode.Escape)&&!inven.IsOpenInven &&phone.IsOpenPhone)))
            {
                if(!phone.IsOpenPhone)
                {
                    phone.ShowPhone();
                }
                else
                {
                    if(inven.IsOpenInven) {inven.HideInven();}
                    phone.HidePhone();
                }
            }
            //인벤 열기
            if((Input.GetKeyDown(KeyCode.I)||(Input.GetKeyDown(KeyCode.Escape)&&inven.IsOpenInven)))
            {
                if(!inven.IsOpenInven)
                {
                    if(!phone.IsOpenPhone)
                    {
                        phone.ShowPhone();
                        inven.ShowInven();
                    }
                    else
                    {
                        inven.ShowInven();
                    }
                }
                else
                {
                    inven.HideInven();
                    phone.HidePhone();
                }
            }
        }

        if(inven.filesInven.IsInvenItemActive && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            inven.filesInven.HideDiary();
        }

        if(!inven.filesInven.IsInvenItemActive)
        {
            //화살표키==================================
            if(Input.GetKey(KeyCode.UpArrow))
            {
                //톡 화면 스크롤
                if(phone.IsOpenPhone && !inven.IsOpenInven)
                {
                    if(phone.talkScView.verticalNormalizedPosition < 1f)
                    {
                        //talkScR.verticalNormalizedPosition += talkScrollSpeed;
                        phone.talkScBar.value += talkScrollSpeed;
                    }
                }
            }
            if(Input.GetKey(KeyCode.DownArrow))
            {
                //톡 화면 스크롤
                if(phone.IsOpenPhone && !inven.IsOpenInven)
                {
                    if(phone.talkScView.verticalNormalizedPosition > 0f)
                    {
                        //talkScR.verticalNormalizedPosition -= talkScrollSpeed;
                        phone.talkScBar.value -= talkScrollSpeed;
                    }
                }
            }
            
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(phone.IsOpenPhone && !inven.IsOpenInven && inven.selectedOption > 1)
                {
                    phone.SelectTalk(-1);
                }
                //얻은 아이템 항목 선택
                if(inven.selectedOption >2)
                {
                    if(inven.IsOpenFiles && inven.MaxFilesSlot != 0)
                    {
                        inven.SetSelectInvenItem(-2, inven.filesInven);

                        //만약 화면상 첫번째 라인이면 다음 라인 아래로 보이기
                        if(inven.IsFirstLine)
                        {
                            inven.ShowOutLine(inven.filesInven, -2, true);
                        }
                        //화면상 첫번째 라인이지 체크
                        if(inven.selectedOption == inven.FirstLineFirstNum || inven.SelectedOption == inven.FirstLineFirstNum + 1)
                        {
                            inven.IsFirstLine = true;
                        } 
                    }
                    if(inven.IsOpenPictures && inven.MaxPicSlot !=0)
                    {
                        inven.SetSelectInvenItem(-2, inven.picsInven);

                        //만약 화면상 첫번째 라인이면 다음 라인 아래로 보이기
                        if(inven.IsFirstLine)
                        {
                            inven.ShowOutLine(inven.picsInven, -2, true);
                        }
                        //화면상 첫번째 라인이지 체크
                        if(inven.SelectedOption == inven.FirstLineFirstNum || inven.SelectedOption == inven.FirstLineFirstNum + 1)
                        {
                            inven.IsFirstLine = true;
                        }
                    }
                }
                if(inven.IsLastLine)
                {
                    inven.IsLastLine=false;
                }
            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(phone.IsOpenPhone && !inven.IsOpenInven&&inven.SelectedOption < phone.ShowedCount)
                {
                    phone.SelectTalk(1);
                }
                //얻은 아이템 항목 선택
                if(inven.IsOpenFiles && inven.MaxFilesSlot!=0 && inven.SelectedOption < inven.MaxFilesSlot - 1)
                {
                    inven.SetSelectInvenItem(2, inven.filesInven);
                        
                    //만약 화면상 마지막 라인이면 다음 라인 위로 보이기
                    if(inven.IsLastLine)
                    {
                        inven.ShowOutLine(inven.filesInven, 2, false);
                    }
                    //화면상 마지막 라인이지 체크
                    if(inven.SelectedOption == inven.LastLineFirstNum || inven.SelectedOption == inven.LastLineFirstNum + 1)
                    {
                        inven.IsLastLine = true;
                    } 
                }
                if(inven.IsOpenPictures && inven.MaxPicSlot != 0 && inven.SelectedOption < inven.MaxPicSlot - 1)
                {   
                    inven.SetSelectInvenItem(2, inven.picsInven);

                    //만약 화면상 마지막 라인이면 다음 라인 위로 보이기
                    if(inven.IsLastLine)
                    {
                        inven.ShowOutLine(inven.picsInven, 2, false);
                    }
                    //화면상 마지막 라인이지 체크
                    if(inven.SelectedOption == inven.LastLineFirstNum || inven.SelectedOption == inven.LastLineFirstNum + 1)
                    {
                        inven.IsLastLine = true;
                    }  
                }
                if(inven.IsFirstLine)
                {
                    inven.IsFirstLine=false;
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //파일 선택
                if(inven.IsOpenInven && !inven.IsOpenFiles && !inven.IsOpenPictures)
                {
                    inven.SelectedInvenOption = 0;
                    inven.invenOption[1].color = Color.white;
                    inven.invenOption[0].color = Color.gray;
                }
                //얻은 아이템 항목 선택
                if(inven.SelectedOption %2==0 && (inven.IsOpenFiles || inven.IsOpenPictures))
                {
                    if(inven.IsOpenFiles && inven.MaxFilesSlot !=0)
                    {
                        inven.SetSelectInvenItem(-1, inven.filesInven);
                    }
                    if(inven.IsOpenPictures && inven.MaxPicSlot!=0)
                    {
                        //선택 해제 표시
                        inven.SetSelectInvenItem(-1, inven.picsInven);
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                //사진 선택
                if(inven.IsOpenInven&&!inven.IsOpenFiles && !inven.IsOpenPictures)
                {
                    inven.SelectedInvenOption = 1;
                    inven.invenOption[0].color = Color.white;
                    inven.invenOption[1].color = Color.gray;
                }
                //얻은 아이템 항목 선택
                if(inven.SelectedOption%2!=0 && (inven.IsOpenFiles||inven.IsOpenPictures))
                {
                    //얻은 아이템의 수에 따라 항목 이동 가능여부 결정
                    if(inven.IsOpenFiles && inven.MaxFilesSlot!=0 && inven.MaxFilesSlot != inven.SelectedOption)
                    {
                        inven.SetSelectInvenItem(1, inven.filesInven);
                    }
                    if(inven.IsOpenPictures && inven.MaxPicSlot!=0 && inven.MaxPicSlot!=inven.SelectedOption)
                    {
                        inven.SetSelectInvenItem(1, inven.picsInven);
                    }
                }
            }
            //화살표키---------------------

            if(Input.GetKeyDown(KeyCode.Return))
            {
                if (inven.IsOpenInven&&!inven.IsOpenFiles&&!inven.IsOpenPictures)
                {
                    if(inven.SelectedInvenOption == 0)
                    {
                        inven.SetFilesActive(true);
                        inven.SetPicsActive(false);
                    }
                    if(inven.SelectedInvenOption==1)
                    {
                        inven.SetPicsActive(true);
                        inven.SetFilesActive(false);
                    }
                }
                else if(inven.IsOpenFiles && inven.MaxFilesSlot!=0)
                {
                    //일기장
                    if(inven.filesInven.slotDataList[inven.SelectedOption - 1].item.itemID == 0)
                    {
                        inven.filesInven.ShowDiary();
                    }
                }
                else if(inven.IsOpenPictures && inven.MaxPicSlot!=0)
                {
                    print(inven.picsInven.slotDataList[inven.SelectedOption - 1].item.itemName);
                }
                //톡 선택
                if(phone.IsOpenPhone && !inven.IsOpenInven&& phone.IsOKSendTalk)
                {
                    // isSendTalkReady = false;
                    phone.IsOKSendTalk = false;

                    if(phone.IsPlayerFirstTalk)
                    {
                        phone.curSendTalk = phone.curTalk.answerTalk[phone.SelectedOption -1];
                    }

                    //화면에 톡 추가
                    phone.AddTalk(phone.curSendTalk.talkText);
                    phone.HideSendTalk();

                    //플레이어가 이어서 보낼 톡이 있는경우
                    if(phone.curSendTalk.nextSendTalk != null)
                    {
                        phone.Invoke("SetNextSendTalk", 1.0f);
                        return;
                    }

                    //더 이상 보낼 플레이어가 보낼 톡이 없는 경우 수행
                    if(TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
                    {
                        TimelineManager.instance.SetTimelineResume();
                    }
                    if(phone.curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndStartTimeline)
                    {
                        TimelineManager.instance.SetTimelineStart(phone.curTalk.timelineName);
                    }
                    if(phone.curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndRunEvent)
                    {
                        phone.curTalk.runEvent.Raise();
                    }

                    phone.SetNextTalk();
                }
            }
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                //서랍창 나가기
                if(inven.IsOpenFiles) { inven.SetFilesActive(false);}
                if(inven.IsOpenPictures) { inven.SetPicsActive(false);}
            }
        }
    }
}