using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    ////���� �κ��丮
    //public GameObject filesInvenObj;
    ////���� �κ��丮
    //public GameObject picsInvenObj;

    //�κ�����
    public Inventory picsInven;
    public Inventory filesInven;
    public GameObject picsArea;
    public GameObject fileArea;

    //�κ� ���̴� ����
    bool isOpenInven = false;
    //�κ��� ���� ���̴� ����
    bool isOpenFiles = false;
    //�κ��� ���� ���̴� ����
    bool isOpenPictures = false;
    //�κ��� ���� ���õ� ������ ȭ��� ù��° �����ΰ�
    bool isFirstLine = false;
    //�κ��� ���� ���õ� ������ ȭ��� ������ �����ΰ�
    bool isLastLine = false;

    //���õ� ������
    public int selectedOption = 1;
    //���ϰ� ���� ����
    int selectedInvenOption = 0;
    //���� ���� ������ ��
    int maxPicSlot = 0;
    //���� ���� ������ ��
    int maxFilesSlot = 0;
    //�κ��� ù��° ������ ù��° ���� ��ȣ
    int firstLineFirstNum = 1;
    //�κ��� ������ ������ ù��° ���� ��ȣ
    int lastLineFirstNum = 5;

    //�κ� ��ũ�� �� ���Ǵ� ��ġ
    Vector2 invenOriginPos;
    Vector2 invenUpValue;
    Vector2 invenDownValue;

    //�κ� ���ö�
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
        //�κ� ��ũ�� �� ���Ǵ� ��ġ
        invenOriginPos = new Vector2(17f, -15f);
        invenUpValue = new Vector2(0f, -70);
        invenDownValue = new Vector2(0f, 70f);
    }

    //�κ� ���̱�
    public void ShowInven()
    {
        invenOption[1].color = Color.white;
        invenOption[0].color = Color.gray;
        gameObject.SetActive(true);
        isOpenInven = true;
    }
    //�ε� �����
    public void HideInven()
    {
        //�����̳� ������ ���������� �ݱ�
        if (isOpenFiles) { SetFilesActive(false); }
        if (isOpenPictures) { SetPicsActive(false); }

        gameObject.SetActive(false);
        isOpenInven = false;
    }
    //���õ� ���� ����
    public void SetSelectInvenItem(int value, Inventory inven)
    {
        //���� ���� ǥ��
        inven.slotList[selectedOption - 1].GetComponent<Image>().color = Color.white;

        selectedOption += value;

        //���õǾ� �ִ� ���� ǥ��
        inven.slotList[selectedOption - 1].GetComponent<Image>().color = Color.gray;
    }
    //ȭ��ۿ� �ִ� ���� ���̱�
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
            //���õǾ� �ִ� ���� ǥ��
            //ȹ���� �ִ� ���
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
    //������ ���̴� ���� ����
    public void SetPicsActive(bool state)
    {
        if (state)
        {
            //���õǾ� �ִ� ���� ǥ��
            //ȹ���� �ִ� ���
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
