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
        if((Input.GetKeyDown(KeyCode.E) && interactionObj!=null))
        {
            obj = interactionObj.GetComponent<InteractionObjData>();
            if(!obj.isInteracting)
            {
                switch(obj.ObjID)
                {
                    case 0:
                        obj.isInteracting=true;
                        Pot pot = obj.thisObj.GetComponent<Pot>();
                        print(gameObject.transform);
                        pot.PushPot(gameObject.transform);
                        break;
                    default:
                        break;
                }
            }
        }
        else if(obj!=null && obj.isInteracting && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            switch(obj.ObjID)
            {
                case 0:
                    obj.thisObj.GetComponent<Pot>().CancelPush(gameObject.transform);
                    break;
            }
            obj.isInteracting = false;
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
}
