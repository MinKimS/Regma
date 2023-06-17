using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjData : MonoBehaviour
{
    public int ObjID;
    public GameObject thisObj;
    
    public bool isOkInteracting = false;
    public bool isInteracting = false;

    public void SetIsOkInteraction(bool value)
    {
        isOkInteracting = value;
    }
}
