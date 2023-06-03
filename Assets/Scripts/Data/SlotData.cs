using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotData : MonoBehaviour
{
    public int slotID;
    public Image slotItemImg;
    [HideInInspector]
    public bool isFull = false;
    [HideInInspector]
    public ItemData item;
}
