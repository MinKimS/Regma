using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Vector2 seeDir;
    Rigidbody2D rb;
    public GameObject interactionObj;
    InteractionObjData obj;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            seeDir = Vector2.left;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            seeDir = Vector2.right;
        }

        //오브젝트와 상호작용
        if(!SmartphoneManager.instance.IsOpenPhone&&DialogueManager.instance._dlgState == DialogueManager.DlgState.End && interactionObj!=null)
        {
            obj = interactionObj.GetComponent<InteractionObjData>();
            if(!obj.isInteracting)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    switch(obj.ObjID)
                    {
                        case 0:
                            Pot pot = obj.thisObj.GetComponent<Pot>();
                            if(pot.diary.isHaveOpened)
                            {
                                obj.isInteracting=true;
                                pot.PushPot(gameObject.transform);
                            }
                            break;
                        case 1:
                            obj.isInteracting=true;
                            Door door = obj.thisObj.GetComponent<Door>();
                            door.ShowDoorImg();
                            break;
                        case 2:
                            obj.isInteracting=true;
                            ItemData item = obj.GetComponent<ItemData>();
                            for(int i =0; i< SmartphoneManager.instance.filesInven.slotList.Count; i++ )
                            {
                                if(!SmartphoneManager.instance.filesInven.slotDataList[i].isFull)
                                {
                                    SetItem(i, item);
                                    SmartphoneManager.instance.filesInven.slotDataList[i].isFull = true;
                                    SmartphoneManager.instance.maxFilesSlot++;
                                    break;
                                }
                            }
                            obj.isInteracting=false;
                            obj.gameObject.SetActive(false);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                if(obj!=null && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
                {
                    CancelEvent();
                }
            }
        }
    }

    public void CancelEvent()
    {
        switch(obj.ObjID)
        {
            case 0:
                obj.thisObj.GetComponent<Pot>().CancelPush(gameObject.transform);
                break;
            case 1:
                obj.thisObj.GetComponent<Door>().HideDoorImg();
                break;
        }
        obj.isInteracting = false;
    }

    private void FixedUpdate() {
        Debug.DrawRay(rb.position, seeDir * 1f, Color.yellow);
        RaycastHit2D rh = Physics2D.Raycast(rb.position, seeDir, 1f, LayerMask.GetMask("Obj"));

        if(rh.collider != null)
        {
            interactionObj = rh.collider.gameObject;
        }
        else
        {
            interactionObj = null;
        }
    }

    private void SetItem(int i, ItemData item)
    {
        ItemData itemData = SmartphoneManager.instance.filesInven.slotDataList[i].gameObject.AddComponent<ItemData>();
        itemData.itemName = item.itemName;
        itemData.itemImg = item.itemImg;
        itemData.itemID = item.itemID;
        SmartphoneManager.instance.filesInven.slotDataList[i].item = itemData;
        SmartphoneManager.instance.filesInven.slotDataList[i].slotItemImg.sprite = itemData.itemImg;
    }
}
