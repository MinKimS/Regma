using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public GameObject ItemHp;
    public Image HpScreen;


    void Start()
    {
        if(HpScreen != null)
            HpScreen.color = Color.clear;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if(HpScreen != null)
            {
                ShowHpScreen();
                return;
            }
            //인벤에 아이템 저장
            ItemData item = GetComponent<ItemData>();
            //if(item == null)
            //{
            //    print("No Item Data.");
            //}
            ////비어있는 슬롯에만 저장
            //for(int i =0; i< SmartphoneManager.instance.inven.filesInven.slotList.Count; i++ )
            //{
            //    if(!SmartphoneManager.instance.inven.filesInven.slotDataList[i].isFull)
            //    {
            //        SmartphoneManager.instance.SetItem(i, item);
            //        SmartphoneManager.instance.inven.filesInven.slotDataList[i].isFull = true;
            //        SmartphoneManager.instance.inven.MaxFilesSlot++;
            //        break;
            //    }
            //}

            SmartphoneManager.instance.SetInvenItem(item);

            Destroy(this.gameObject);
            //Damage HpControl = collision.GetComponent<Damage>();
            //if (HpControl != null)
            //{
            //    HpControl.ShowHpScreen();
            //    ItemHp.SetActive(true);
            //    HpScreen.enabled = true;
            //    gameObject.SetActive(true);
            //}
        }
    }
    public void ShowHpScreen()
    {
        HpScreen.color = new Color(1f, 1f, 1f, 1f);
    }

    //private void SetItem(int i, ItemData item)
    //{
    //    if(item != null)
    //    {
    //        ItemData itemData = SmartphoneManager.instance.inven.filesInven.slotDataList[i].gameObject.AddComponent<ItemData>();
    //        itemData.itemName = item.itemName;
    //        itemData.itemImg = item.itemImg;
    //        itemData.itemID = SmartphoneManager.instance.itemIdx;
    //        SmartphoneManager.instance.gmEventList.Add(item.itemEvent);
    //        SmartphoneManager.instance.inven.filesInven.slotDataList[i].item = itemData;
    //        SmartphoneManager.instance.inven.filesInven.slotDataList[i].slotItemImg.sprite = itemData.itemImg;
    //    }
    //}
}
