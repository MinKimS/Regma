using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Story/New Dialogue")]
public class Dialogue : ScriptableObject
{
    //이번 대화에 등장할 캐릭터 리스트
    public List<Speakers> speakers;
    //대화리스트
    public List<Sentence> sentences;
    //다음 새로운 대화
    public Dialogue nextDlg;

    //대화에 나올 캐릭터
    [System.Serializable]
    public struct Speakers
    {
        //캐릭터 정보
        public Speaker speaker;
        //캐릭터 이미지가 적용될 오브젝트의 이미지 배열 인덱스
        public int imgIdx;
    }
    //현재 화면에 출력될 대화
    [System.Serializable]
    public struct Sentence
    {
        //이번 대사 캐릭터 정보를 가져오기 위한 인덱스
        public int speakerIdx;
        //대사 정보
        [TextArea]
        public string dlgTexts;
    }
}
