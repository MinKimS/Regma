using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //인벤 항목들
    public RectTransform slotRT;
    //슬롯의 정보들
    public List<GameObject> slotList;
    //슬롯의 데이터 정보들
    public List<SlotData> slotDataList;
    public GameObject[] potEvent;

    public Inventory(){
        slotList = new List<GameObject>();
        slotDataList = new List<SlotData>();
    }

    //==============인벤토리에서 상호작용하면 생기는 이벤트들 관련===================
    public InteractionObjData pot;
    public InteractionObjData tv;
    public GameObject diary;
    //인벤토리 아이템 상호작용해서 수행중인것이 있는지 여부
    private bool isInvenItemActive = false;

    public bool IsInvenItemActive{
        get{
            return isInvenItemActive;
        }
    }
    
    //일기장
    public void ShowDiary()
    {
        diary.gameObject.SetActive(true);
        isInvenItemActive = true;
    }
    public void HideDiary()
    {
        if(!pot.isOkInteracting)
        {
            pot.isOkInteracting = true;
            
            potEvent[0].SetActive(true);
            potEvent[1].SetActive(true);
        }
        if(!tv.isOkInteracting) {tv.isOkInteracting = true;}
        diary.gameObject.SetActive(false);
        isInvenItemActive = false;
    }

}
