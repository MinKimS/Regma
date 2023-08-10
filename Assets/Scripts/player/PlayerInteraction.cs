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
            //do interaction
            if(obj != null && obj.IsOkInteracting)
            {
                //상호작용 중엔 x
                if(!obj.IsInteracting)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (obj.gmEvent.Length > 0)
                        {
                            obj.gmEvent[obj.GmEventIdx].Raise();
                        }
                    }
                }
            }
            //cancel interaction
            else
            {
                if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
                {
                    if(obj.cancelEvent.Length > 0)
                    {
                        obj.cancelEvent[obj.CancelEventIdx].Raise();
                        obj.IsInteracting = false;
                    }
                }
            }
        }
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
