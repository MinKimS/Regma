using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GotItemData", menuName = "Data/GotItemData")]
public class GotItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemInfo;
}
