using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            //인벤에 아이템 저장
            ItemData item = GetComponent<ItemData>();
            if(item == null)
            {
                print("No Item Data.");
            }
            //비어있는 슬롯에만 저장
            for(int i =0; i< SmartphoneManager.instance.filesInven.slotList.Count; i++ )
            {
                if(!SmartphoneManager.instance.filesInven.slotDataList[i].isFull)
                {
                    SmartphoneManager.instance.filesInven.slotDataList[i].slotItemImg.sprite = item.itemImg;
                    SmartphoneManager.instance.filesInven.slotDataList[i].isFull = true;
                    SmartphoneManager.instance.maxFilesSlot++;
                    break;
                }
            }
            
            Destroy(this.gameObject);
        }
    }
   
}
