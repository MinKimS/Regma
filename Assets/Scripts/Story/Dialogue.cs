using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Story/New Dialogue")]
public class Dialogue : ScriptableObject
{
    public int dlgID;
    public enum EventType
    {
        None,
        Anim,
        Timeline,
    }
    //이번 대화에 등장할 캐릭터 리스트
    public List<Speaker> speakers;
    //대화리스트
    public List<Sentence> sentences;
    //다음 새로운 대화
    public Dialogue nextDlg;

    //현재 화면에 출력될 대화
    [System.Serializable]
    public struct Sentence
    {
        //이번 대사 캐릭터 정보를 가져오기 위한 인덱스
        public int speakerIdx;
        //대사 정보
        [TextArea]
        public string dlgTexts;
        
        //진행될 이벤트
        public EventType eventType;
    }
}
