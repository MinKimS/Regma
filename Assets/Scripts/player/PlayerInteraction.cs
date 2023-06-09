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
            if(obj != null && obj.isOkInteracting)
            {
                if(obj.isInteracting)
                {
                    if(obj!=null && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
                    {
                        CancelEvent();
                    }
                    return;
                }
                if(Input.GetKeyDown(KeyCode.E))
                {
                    obj.isInteracting=true;
                    switch(obj.ObjID)
                    {
                        case 0:
                            Pot pot = obj.thisObj.GetComponent<Pot>();
                            pot.PushPot(gameObject.transform);
                            break;
                        case 1:
                            Door door = obj.thisObj.GetComponent<Door>();
                            door.StartCurDoorEvent();
                            obj.isInteracting=false;
                            obj.isOkInteracting = false;
                            break;
                        case 2:
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
                            obj.isOkInteracting=false;
                            obj.gameObject.SetActive(false);
                            break;
                        case 3:
                            Television tv = obj.thisObj.GetComponent<Television>();
                            //after reading diary
                            tv.TVOnAfterReadingDairy();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    switch(obj.ObjID)
                    {
                        case 0:
                            obj.thisObj.GetComponent<Pot>().PotDlg(0);
                            break;
                        case 1:
                            if(obj.thisObj.GetComponent<Door>().checkWorkDo >2)
                            {
                                print("move next stage");
                            }
                            break;
                        case 3:
                            Television tv = obj.thisObj.GetComponent<Television>();
                            //before reading diary
                            tv.TVOnBeforeReadingDiary();
                            break;
                        //소파, 책장
                        case 4:
                        case 5:
                            DialogueManager.instance.PlayDlg(obj.objDlg[0]);
                            break;
                    }
                }
            }
        }
    }

    //진행중인 상호작용 취소
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
            case 3:
                obj.thisObj.GetComponent<Television>().TVOff();
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
