using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SmartphoneManager : MonoBehaviour
{
    public static SmartphoneManager instance;

    [HideInInspector] public InventoryItemUsage itemUsage;

    [HideInInspector] public TalkNotification notification;

    [HideInInspector] public Phone phone;
    //핸드폰 인벤토리
    [HideInInspector] public InventoryController inven;
    [HideInInspector] public Diary diary;

    //톡 스크롤되는 속도
    public float talkScrollSpeed = 0.05f;

    //[NonSerialized]
    //public int itemIdx = 0;
    public List<GameEvent> gmEventList = new List<GameEvent>(16);

    public Sprite invenslotSirte;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

        itemUsage = GetComponent<InventoryItemUsage>();
        phone = GetComponentInChildren<Phone>();
        inven = phone.GetComponentInChildren<InventoryController>();
        notification = GetComponentInChildren<TalkNotification>();
        diary = GetComponentInChildren<Diary>();
    }
    private void Start() {
        inven.HideInven();
        phone.HidePhone();

    }
    private void OnEnable()
    {
        //이거 켜져 있으면 각각 씬에서 대사 테스트 안됨
        SceneManager.sceneLoaded += LoadSceneEvent;
    }
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (inven != null && phone != null)
        {
            inven.HideInven();
            phone.HidePhone();
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    private void Update() {
        if (!GameManager.instance.isMenuOpen
            && !GameManager.instance.isHowtoOpen)
        {
            if ((TimelineManager.instance._Tlstate == TimelineManager.TlState.End) && DialogueManager.instance._dlgState == DialogueManager.DlgState.End
                && !inven.filesInven.IsInvenItemActive
                && !TutorialController.instance.IsTutorialShowing
                && !itemUsage.isUsingItem
                && SceneManager.GetActiveScene().name != "Ending"
                && SceneManager.GetActiveScene().name != "Title"
                && SceneManager.GetActiveScene().name != "Intro")
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    if (inven.IsOpenInven) { inven.HideInven(); }
                    phone.HidePhone();
                }
                //폰 열기
                if (Input.GetKeyDown(KeyCode.P) && !GameManager.instance.isMenuOpen)
                {
                    if (!phone.IsOpenPhone)
                    {
                        phone.ShowPhone();
                    }
                    else
                    {
                        if (inven.IsOpenInven) { inven.HideInven(); }
                        phone.HidePhone();
                    }
                }
                //인벤 열기
                if (Input.GetKeyDown(KeyCode.I) && !GameManager.instance.isMenuOpen)
                {
                    if (!inven.IsOpenInven)
                    {
                        if (!phone.IsOpenPhone)
                        {
                            phone.ShowPhone();
                            inven.ShowInven();
                        }
                        else
                        {
                            inven.ShowInven();
                        }
                        inven.ShowInven();
                    }
                    else
                    {
                        inven.HideInven();
                    }
                }
            }


            if (!inven.filesInven.IsInvenItemActive)
            {
                //톡 화면 스크롤
                if (phone.IsOpenPhone && !inven.IsOpenInven && phone.talkScView.verticalNormalizedPosition != 0
                    && !phone.IsOKSendTalk)
                {
                    if (Input.GetAxisRaw("Vertical") > 0)
                    {
                        if (phone.talkScView.verticalNormalizedPosition < 1f)
                        {
                            phone.talkScBar.value += talkScrollSpeed;
                        }
                    }
                    if (Input.GetAxisRaw("Vertical") < 0)
                    {
                        if (phone.talkScView.verticalNormalizedPosition > 0f)
                        {
                            phone.talkScBar.value -= talkScrollSpeed;
                        }
                    }
                }

                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !GameManager.instance.isMenuOpen)
                {
                    if (phone.IsOpenPhone && !inven.IsOpenInven && phone.SelectedOption > 1)
                    {
                        phone.SelectTalk(-1);
                    }
                    //얻은 아이템 항목 선택
                    if (inven.selectedOption > 2)
                    {
                        if (inven.IsOpenFiles && inven.MaxFilesSlot != 0)
                        {
                            inven.SetSelectInvenItem(-2, inven.filesInven);

                            //만약 화면상 첫번째 라인이면 다음 라인 아래로 보이기
                            if (inven.IsFirstLine)
                            {
                                inven.ShowOutLine(inven.filesInven, -2, true);
                            }
                            //화면상 첫번째 라인이지 체크
                            if (inven.selectedOption == inven.FirstLineFirstNum || inven.SelectedOption == inven.FirstLineFirstNum + 1)
                            {
                                inven.IsFirstLine = true;
                            }
                        }
                        if (inven.IsOpenPictures && inven.MaxPicSlot != 0)
                        {
                            inven.SetSelectInvenItem(-2, inven.picsInven);

                            //만약 화면상 첫번째 라인이면 다음 라인 아래로 보이기
                            if (inven.IsFirstLine)
                            {
                                inven.ShowOutLine(inven.picsInven, -2, true);
                            }
                            //화면상 첫번째 라인이지 체크
                            if (inven.SelectedOption == inven.FirstLineFirstNum || inven.SelectedOption == inven.FirstLineFirstNum + 1)
                            {
                                inven.IsFirstLine = true;
                            }
                        }
                    }
                    if (inven.IsLastLine)
                    {
                        inven.IsLastLine = false;
                    }
                }
                if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !GameManager.instance.isMenuOpen)
                {
                    if (phone.IsOpenPhone && !inven.IsOpenInven && phone.SelectedOption < phone.ShowedCount)
                    {
                        phone.SelectTalk(1);
                    }
                    //얻은 아이템 항목 선택
                    if (inven.IsOpenFiles && inven.MaxFilesSlot != 0 && inven.SelectedOption < inven.MaxFilesSlot - 1)
                    {
                        inven.SetSelectInvenItem(2, inven.filesInven);

                        //만약 화면상 마지막 라인이면 다음 라인 위로 보이기
                        if (inven.IsLastLine)
                        {
                            inven.ShowOutLine(inven.filesInven, 2, false);
                        }
                        //화면상 마지막 라인이지 체크
                        if (inven.SelectedOption == inven.LastLineFirstNum || inven.SelectedOption == inven.LastLineFirstNum + 1)
                        {
                            inven.IsLastLine = true;
                        }
                    }
                    if (inven.IsOpenPictures && inven.MaxPicSlot != 0 && inven.SelectedOption < inven.MaxPicSlot - 1)
                    {
                        inven.SetSelectInvenItem(2, inven.picsInven);

                        //만약 화면상 마지막 라인이면 다음 라인 위로 보이기
                        if (inven.IsLastLine)
                        {
                            inven.ShowOutLine(inven.picsInven, 2, false);
                        }
                        //화면상 마지막 라인이지 체크
                        if (inven.SelectedOption == inven.LastLineFirstNum || inven.SelectedOption == inven.LastLineFirstNum + 1)
                        {
                            inven.IsLastLine = true;
                        }
                    }
                    if (inven.IsFirstLine)
                    {
                        inven.IsFirstLine = false;
                    }
                }
                if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !GameManager.instance.isMenuOpen)
                {
                    ////파일 선택
                    //if (inven.IsOpenInven && !inven.IsOpenFiles && !inven.IsOpenPictures)
                    //{
                    //    inven.SelectedInvenOption = 0;
                    //    inven.invenOption[1].color = Color.white;
                    //    inven.invenOption[0].color = Color.gray;
                    //}
                    //얻은 아이템 항목 선택
                    if (inven.SelectedOption % 2 == 0 && (inven.IsOpenFiles || inven.IsOpenPictures))
                    {
                        if (inven.IsOpenFiles && inven.MaxFilesSlot != 0)
                        {
                            inven.SetSelectInvenItem(-1, inven.filesInven);
                        }
                        if (inven.IsOpenPictures && inven.MaxPicSlot != 0)
                        {
                            //선택 해제 표시
                            inven.SetSelectInvenItem(-1, inven.picsInven);
                        }
                    }
                }
                if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !GameManager.instance.isMenuOpen)
                {
                    ////사진 선택
                    //if (inven.IsOpenInven && !inven.IsOpenFiles && !inven.IsOpenPictures)
                    //{
                    //    inven.SelectedInvenOption = 1;
                    //    inven.invenOption[0].color = Color.white;
                    //    inven.invenOption[1].color = Color.gray;
                    //}
                    //얻은 아이템 항목 선택
                    if (inven.SelectedOption % 2 != 0 && (inven.IsOpenFiles || inven.IsOpenPictures))
                    {
                        //얻은 아이템의 수에 따라 항목 이동 가능여부 결정
                        if (inven.IsOpenFiles && inven.MaxFilesSlot != 0 && inven.MaxFilesSlot != inven.SelectedOption)
                        {
                            inven.SetSelectInvenItem(1, inven.filesInven);
                        }
                        if (inven.IsOpenPictures && inven.MaxPicSlot != 0 && inven.MaxPicSlot != inven.SelectedOption)
                        {
                            inven.SetSelectInvenItem(1, inven.picsInven);
                        }
                    }
                }

                //선택 및 사용
                if (Input.GetKeyDown(KeyCode.Return) && DialogueManager.instance._dlgState == DialogueManager.DlgState.End && !TutorialController.instance.IsTutorialShowing
                     && !GameManager.instance.isMenuOpen)
                {
                    ////파일과 사진 중 선택
                    //if (inven.IsOpenInven && !inven.IsOpenFiles && !inven.IsOpenPictures)
                    //{
                    //    if (inven.SelectedInvenOption == 0)
                    //    {
                    //        inven.SetFilesActive(true);
                    //        inven.SetPicsActive(false);
                    //    }
                    //    if (inven.SelectedInvenOption == 1)
                    //    {
                    //        inven.SetPicsActive(true);
                    //        inven.SetFilesActive(false);
                    //    }
                    //}
                    //파일에서 아이템 사용
                    if (inven.IsOpenFiles && inven.MaxFilesSlot != 0)
                    {
                        itemUsage.UseItem(inven.filesInven.slotDataList[inven.SelectedOption - 1].item);

                        if (inven.IsOpenInven) { inven.HideInven(); }
                        phone.HidePhone();
                    }
                    ////사진에서 아이템 사용
                    //else if (inven.IsOpenPictures && inven.MaxPicSlot != 0)
                    //{
                    //    print(inven.picsInven.slotDataList[inven.SelectedOption - 1].item.itemName);
                    //    gmEventList[inven.SelectedOption - 1].Raise();
                    //    print(inven.SelectedOption - 1);
                    //}

                    if (phone.curTalk != null && phone.curTalk.answerTalk == null)
                    {
                        phone.IsOKSendTalk = false;
                    }
                    //톡 선택
                    if (phone.IsOpenPhone && !inven.IsOpenInven && phone.IsOKSendTalk)
                    {
                        phone.IsOKSendTalk = false;

                        if (phone.IsPlayerFirstTalk)
                        {
                            phone.curSendTalk = phone.curTalk.answerTalk[phone.SelectedOption - 1];

                            if (phone.curTalk.answerTalk.Length > 1)
                            {
                                phone.talkSelections[phone.talkselectionsIdx] = phone.SelectedOption - 1;
                                if (phone.talkselectionsIdx < phone.talkSelections.Length)
                                {
                                    phone.talkselectionsIdx++;
                                }
                            }
                        }

                        //화면에 톡 추가
                        phone.AddTalk(phone.curSendTalk.talkText);
                        phone.HideSendTalk();

                        //플레이어가 이어서 보낼 톡이 있는경우
                        if (phone.curSendTalk.nextSendTalk != null)
                        {
                            phone.Invoke("SetNextSendTalk", 1.0f);
                            return;
                        }

                        if (phone.curSendTalk.nextSendTalk == null)
                        {
                            phone.isTalkNeedToBeSend = false;
                        }

                        //더 이상 보낼 플레이어가 보낼 톡이 없는 경우 수행
                        if (phone.curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndRunEvent
                            && phone.curTalk.runEvent != null)
                        {
                            phone.curTalk.runEvent.Raise();
                            phone.SetNextTalk();
                        }
                        else if (phone.curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndRunNextTalk)
                        {
                            phone.SetNextTalk();

                            phone.isOkStartTalk = true;
                            phone.StartTalk();
                        }

                        if (phone.curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndResumeTimeline)
                        {
                            phone.SetNextTalk();
                            TimelineManager.instance.timelineController.SetTimelineResume();
                        }
                        else if (phone.curTalk.afterEndTalk == Talk.AfterEndTalk.SendTalkAndStartTimeline)
                        {
                            phone.SetNextTalk();
                            TimelineManager.instance.timelineController.SetTimelineStart(phone.curTalk.timelineName);
                            print("settimelinestart");
                        }
                        phone.isOkStartTalk = true;
                    }
                }
            }
        }
    }

    public void SetInvenItem(ItemData item)
    {
        if (item == null)
        {
            print("No Item Data.");
        }
        //비어있는 슬롯에만 저장
        for (int i = 0; i < inven.filesInven.slotList.Count; i++)
        {
            if (!inven.filesInven.slotDataList[i].isFull)
            {
                SetItem(i, item);
                inven.filesInven.slotDataList[i].isFull = true;
                inven.maxFilesSlot++;
                break;
            }
        }
    }

    public void SetItem(int i, ItemData item)
    {
        if (item != null)
        {
            ItemData itemData = inven.filesInven.slotDataList[i].gameObject.GetComponent<ItemData>();
            SetInvenItemData(i, item, itemData);
            gmEventList.Add(item.itemEvent);
            inven.filesInven.slotDataList[i].item = itemData;
            inven.filesInven.slotDataList[i].slotItemImg.sprite = itemData.itemImg;
        }
    }

    //인벤토리서 선택한 아이템 삭제
    public void DeleteSelectItem()
    {
        ItemData itemData;
        for (int i = inven.SelectedOption-1; i < inven.maxFilesSlot-1; i++)
        {
            itemData = inven.filesInven.slotDataList[i].gameObject.GetComponent<ItemData>();
            SetInvenItemData(i, inven.filesInven.slotDataList[i + 1].item, itemData);
            inven.filesInven.slotDataList[i].slotItemImg.sprite = itemData.itemImg;
        }

        itemData = inven.filesInven.slotDataList[inven.maxFilesSlot - 1].gameObject.GetComponent<ItemData>();
        SetInvenItemDataNull(inven.maxFilesSlot - 1, itemData);
        inven.filesInven.slotDataList[inven.maxFilesSlot - 1].slotItemImg.sprite = invenslotSirte;
        inven.filesInven.slotDataList[inven.maxFilesSlot - 1].isFull = false;
        //inven.filesInven.slotDataList.RemoveAt(inven.maxFilesSlot);
        inven.maxFilesSlot--;
    }

    void SetInvenItemData(int i, ItemData item, ItemData itemData)
    {
        itemData.itemName = item.itemName;
        itemData.itemImg = item.itemImg;
        itemData.itemID = item.itemID;
        itemData.itemEvent = item.itemEvent;
        inven.filesInven.slotDataList[i].slotItemImg.sprite = item.itemImg;
    }
    void SetInvenItemDataNull(int i, ItemData itemData)
    {
        itemData.itemName = "";
        itemData.itemImg = null;
        itemData.itemID = -1;
        itemData.itemEvent = null;
    }

    //모든 인벤토리 아이템 삭제
    public void DeleteAllItemInInven()
    {
        ItemData itemData;

        for(int i = 0; i < inven.maxFilesSlot; i++)
        {
            itemData = inven.filesInven.slotDataList[i].gameObject.GetComponent<ItemData>();
            SetInvenItemDataNull(i, itemData);
            inven.filesInven.slotDataList[i].slotItemImg.sprite = invenslotSirte;
            inven.filesInven.slotDataList[i].isFull = false;
        }

        //inven.filesInven.slotDataList.Clear();
        inven.maxFilesSlot = 0;
    }
}
