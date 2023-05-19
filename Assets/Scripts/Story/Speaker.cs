using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speaker", menuName = "Story/New Speaker")]
public class Speaker : ScriptableObject {
    //화자 이름
    public string speakerName;
    //화자 이미지
    public Sprite speakerSprite;
}
