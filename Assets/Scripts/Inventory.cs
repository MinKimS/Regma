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
    //얻은 아이템들
    public List<GotItemData> gotItemList;

    public Inventory(){
        slotList = new List<GameObject>();
        slotDataList = new List<SlotData>();
    }

    private void Awake() {
        for(int i =0; i<slotList.Count; i++)
        {
            slotDataList.Add(slotList[i].GetComponent<SlotData>());
        }
    }
}
