using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Story/New Dialogue")]
public class Dialogue : ScriptableObject
{
    //대화리스트
    public List<Sentence> sentences;
    //다음 새로운 대화
    public Dialogue nextDlg;

    //현재 컷에 화면에 출력될 대화
    [System.Serializable]
    public struct Sentence
    {
        public string speaker;
        [TextArea]
        public string dlgTexts;
    }
}
