using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour
{
    // void Start()
    // {
    //     //대화 출력 테스트용
    //     DialogueManager.instance.Invoke("PlayDlg", 0.03f);
    // }

    void Update()
    {
        //다음 대화로 넘어가기
        //대화 출력중에는 넘어가지 못함
        if (Input.GetKeyDown(KeyCode.Return) && DialogueManager.instance._dlgState == DialogueManager.DlgState.DONE && DialogueManager.instance._dlgState != DialogueManager.DlgState.End)
        {
            if (!DialogueManager.instance.isSingleDlg)
            {
                //다음 대사로 넘어가기
                if (DialogueManager.instance.SetenceIdx < DialogueManager.instance.curDlg.sentences.Count)
                {
                    DialogueManager.instance.PlaySentence();
                }
                //마지막 대사인 경우 클릭 시 숨기기
                else
                {
                    DialogueManager.instance.DialogueHide();
                }
            }
            else
            {
                //다음 대사로 넘어가기
                if (DialogueManager.instance.SetenceIdx < DialogueManager.instance.singleDlg.sentences.Count)
                {
                    DialogueManager.instance.PlaySentence(DialogueManager.instance.singleDlg);
                }
                //마지막 대사인 경우 클릭 시 숨기기
                else
                {
                    DialogueManager.instance.DialogueHide(DialogueManager.instance.singleDlg);
                }
            }
        }
    }
}
