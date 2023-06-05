using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speaker", menuName = "Story/New Speaker")]
public class Speaker : ScriptableObject {
    public int speakerID;
    //화자 이름
    public string speakerName;
    //화자 이미지
    public Sprite speakerSprite;
    //카톡이름
    public string talkName;
    //카톡 프로필
    public Sprite talkProfileSp;
}
