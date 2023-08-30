using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    ////파일 인벤토리
    //public GameObject filesInvenObj;
    ////사진 인벤토리
    //public GameObject picsInvenObj;

    //인벤정보
    public Inventory picsInven;
    public Inventory filesInven;
    public GameObject picsArea;
    public GameObject fileArea;

    //인벤 보이는 여부
    bool isOpenInven = false;
    //인벤의 파일 보이는 여부
    bool isOpenFiles = false;
    //인벤의 사진 보이는 여부
    bool isOpenPictures = false;
    //인벤의 현재 선택된 슬롯이 화면상 첫번째 라인인가
    bool isFirstLine = false;
    //인벤의 현재 선택된 슬롯이 화면상 마지막 라인인가
    bool isLastLine = false;

    //선택된 아이템
    public int selectedOption = 1;
    //파일과 사진 선택
    int selectedInvenOption = 0;
    //얻은 사진 아이템 수
    int maxPicSlot = 0;
    //얻은 파일 아이템 수
    int maxFilesSlot = 0;
    //인벤의 첫번째 라인의 첫번째 슬롯 번호
    int firstLineFirstNum = 1;
    //인벤의 마지막 라인의 첫번째 슬롯 번호
    int lastLineFirstNum = 5;

    //인벤 스크롤 시 사용되는 수치
    Vector2 invenOriginPos;
    Vector2 invenUpValue;
    Vector2 invenDownValue;

    //인벤 선택란
    public Image[] invenOption;

    public bool IsOpenInven
    {
        get { return isOpenInven; }
    }
    public bool IsOpenFiles
    {
        get { return isOpenFiles; }
    }
    public bool IsOpenPictures
    {
        get { return isOpenPictures; }
    }
    public bool IsFirstLine
    {
        get { return isFirstLine; }
        set { isFirstLine = value; }
    }
    public bool IsLastLine
    { 
        get { return isLastLine; } 
        set { isLastLine = value; }
    }
    public int MaxFilesSlot
    {
        get { return maxFilesSlot; }
        set {  maxFilesSlot = value; }
    }
    public int MaxPicSlot
    {
        get { return maxPicSlot; }
    }
    public int SelectedOption
    {
        get { return selectedOption; }
    }
    public int SelectedInvenOption
    {
        get { return selectedInvenOption; }
        set {  selectedInvenOption = value; }
    }
    public int FirstLineFirstNum
    {
        get { return firstLineFirstNum; }
    }
    public int LastLineFirstNum
    {
        get { return lastLineFirstNum; }
    }

    private void Awake()
    {
        //인벤 스크롤 시 사용되는 수치
        invenOriginPos = new Vector2(17f, -15f);
        invenUpValue = new Vector2(0f, -70);
        invenDownValue = new Vector2(0f, 70f);
    }

    //인벤 보이기
    public void ShowInven()
    {
        invenOption[1].color = Color.white;
        invenOption[0].color = Color.gray;
        gameObject.SetActive(true);
        isOpenInven = true;
    }
    //인덴 숨기기
    public void HideInven()
    {
        //파일이나 사진이 열려있으면 닫기
        if (isOpenFiles) { SetFilesActive(false); }
        if (isOpenPictures) { SetPicsActive(false); }

        gameObject.SetActive(false);
        isOpenInven = false;
    }
    //선택된 슬롯 변경
    public void SetSelectInvenItem(int value, Inventory inven)
    {
        //선택 해제 표시
        inven.slotList[selectedOption - 1].GetComponent<Image>().color = Color.white;

        selectedOption += value;

        //선택되어 있는 상태 표시
        inven.slotList[selectedOption - 1].GetComponent<Image>().color = Color.gray;
    }
    //화면밖에 있는 라인 보이기
    public void ShowOutLine(Inventory inven, int value, bool isUp)
    {
        if (isUp) { inven.slotRT.anchoredPosition += invenUpValue; }
        else { inven.slotRT.anchoredPosition += invenDownValue; }
        firstLineFirstNum += value;
        lastLineFirstNum += value;
    }
    public void SetFilesActive(bool state)
    {
        if (state)
        {
            //선택되어 있는 상태 표시
            //획득한 있는 경우
            if (maxFilesSlot != 0)
            {
                filesInven.slotList[0].GetComponent<Image>().color = Color.gray;
                selectedOption = 1;
                lastLineFirstNum = 5;
                firstLineFirstNum = 1;
                isLastLine = false;
                isFirstLine = true;
                filesInven.slotRT.anchoredPosition = invenOriginPos;
                filesInven.slotRT.offsetMin = invenOriginPos;
            }
        }
        else
        {
            filesInven.slotList[selectedOption - 1].GetComponent<Image>().color = Color.white;
        }
        fileArea.SetActive(state);
        isOpenFiles = state;
    }
    //사진함 보이는 여부 설정
    public void SetPicsActive(bool state)
    {
        if (state)
        {
            //선택되어 있는 상태 표시
            //획득한 있는 경우
            if (maxPicSlot != 0)
            {
                picsInven.slotList[0].GetComponent<Image>().color = Color.gray;
                selectedOption = 1;
                lastLineFirstNum = 5;
                firstLineFirstNum = 1;
                isLastLine = false;
                isFirstLine = true;
                picsInven.slotRT.anchoredPosition = invenOriginPos;
            }
        }
        else
        {
            picsInven.slotList[selectedOption - 1].GetComponent<Image>().color = Color.white;
            invenOption[0].color = Color.gray;
            invenOption[1].color = Color.white;
            selectedInvenOption = 0;
        }

        picsArea.SetActive(state);
        isOpenPictures = state;
    }
}
