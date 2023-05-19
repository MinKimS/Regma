using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour
{
    //대화 출력여부 확인용
    public enum DlgState
    {
        TYPING,
        DONE
    }
    DlgState dlgState = DlgState.TYPING;

    //대화텍스트
    public TextMeshProUGUI dlgText;
    //화자이름
    public TextMeshProUGUI speaker;
    //말 끝남표시(확인용)
    public Image nextArrow;
    //현재 출력될 대화
    public Dialogue curDlg;
    //현재 출력되는 대화 인덱스
    private int setenceIdx = 0;

    void Start()
    {
        //대화 출력 테스트용
        StartCoroutine(TypingDlg(curDlg.sentences[setenceIdx].dlgTexts, curDlg.sentences[setenceIdx].speaker));
    }

    void Update()
    {
        //다음 대화로 넘어가기
        //대화 출력중에는 넘어가지 못함
        if(Input.GetKeyDown(KeyCode.Return) && dlgState == DlgState.DONE)
        {
            if(++setenceIdx < curDlg.sentences.Count)
            {
                StartCoroutine(TypingDlg(curDlg.sentences[setenceIdx].dlgTexts, curDlg.sentences[setenceIdx].speaker));
            }
        }
    }
    
    //대화 출력
    private IEnumerator TypingDlg(string text, string name)
    {
        nextArrow.enabled = false;
        
        dlgState = DlgState.TYPING;
        dlgText.text = "";
        int dlgWordIdx = 0;

        //말하는 애 이름 설정
        if(name != "")
        {
            speaker.text = name;
        }
        //사람이 아닌거에 대한 대화창 출력시
        else
        {
            speaker.text = "";
        }
        //대화 출력하는 부분
        while(dlgWordIdx != text.Length)
        {
            dlgText.text += text[dlgWordIdx++];
            yield return new WaitForSeconds(0.15f);
        }
        dlgState = DlgState.DONE;
        
        nextArrow.enabled = true;
    }

    //대화창 보이기
    public void DialogueShow()
    {
        gameObject.SetActive(true);
    }
    //대화창 숨기기
    public void DialogueHide()
    {
        gameObject.SetActive(false);
    }
    //다음 새로운 대화로 넘어가기
    public void NextDlg()
    {
        if(curDlg.nextDlg != null)
        {
            setenceIdx = -1;
            //새로운 대화로 설정
            curDlg = curDlg.nextDlg;
        }
    }
}
