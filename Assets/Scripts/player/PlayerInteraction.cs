using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Vector2 seeDir;
    Rigidbody2D rb;

    public GameObject interactionObj;
    InteractionObjData obj;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            seeDir = Vector2.left;
        }
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            seeDir = Vector2.right;
        }

        //오브젝트와 상호작용
        if(!SmartphoneManager.instance.phone.IsOpenPhone&&DialogueManager.instance._dlgState == DialogueManager.DlgState.End && interactionObj!=null)
        {
            obj = interactionObj.GetComponent<InteractionObjData>();
            //do interaction
            if(obj != null && obj.IsOkInteracting)
            {
                //상호작용
                if (!obj.IsRunInteraction)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (obj.gmEvent.Length > 0)
                        {
                            obj.gmEvent[obj.GmEventIdx].Raise();
                        }
                    }
                }
                //진행중인 상호작용 취소
                else
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
                    {
                        if (obj.cancelEvent.Length > 0)
                        {
                            obj.cancelEvent[obj.CancelEventIdx].Raise();
                        }
                    }
                }
            }
        }
    }

    private void FixedUpdate() {
        Debug.DrawRay(rb.position, seeDir * 1.5f, Color.yellow);
        RaycastHit2D rh = Physics2D.Raycast(rb.position, seeDir, 1.5f, LayerMask.GetMask("Obj"));

        if(rh.collider != null)
        {
            interactionObj = rh.collider.gameObject;
        }
        else
        {
            interactionObj = null;
        }
    }

    //private void SetItem(int i, ItemData item)
    //{
    //    ItemData itemData = SmartphoneManager.instance.inven.filesInven.slotDataList[i].gameObject.AddComponent<ItemData>();
    //    itemData.itemName = item.itemName;
    //    itemData.itemImg = item.itemImg;
    //    itemData.itemID = item.itemID;
    //    SmartphoneManager.instance.inven.filesInven.slotDataList[i].item = itemData;
    //    SmartphoneManager.instance.inven.filesInven.slotDataList[i].slotItemImg.sprite = itemData.itemImg;
    //}
}
