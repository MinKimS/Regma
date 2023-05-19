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

    //화자 오브젝트
    public GameObject[] speakersObj;
    //화자 이미지들
    private Image[] speakerImg;
    //화자 크기
    private RectTransform[] speakerRectTr;

    //말 끝남표시(확인용)
    public Image nextArrow;
    //현재 출력될 대화
    public Dialogue curDlg;
    //현재 출력되는 대화 인덱스
    private int setenceIdx = 0;

    private Color imgToDark = new Color32(100,100,100,255);
    private Color imgToBright = new Color32(255,255,255,255);

    private void Awake() {
        speakerImg = new Image[speakersObj.Length];    
        speakerRectTr = new RectTransform[speakersObj.Length];    
    }
    void Start()
    {
        for(int i =0; i<speakersObj.Length; i++)
        {
            speakerImg[i] = speakersObj[i].GetComponent<Image>();
            speakerRectTr[i] = speakersObj[i].GetComponent<RectTransform>();
        }

        //대화 출력 테스트용
        StartDlg();
    }

    void Update()
    {
        //다음 대화로 넘어가기
        //대화 출력중에는 넘어가지 못함
        if(Input.GetKeyDown(KeyCode.Return) && dlgState == DlgState.DONE && setenceIdx < curDlg.sentences.Count)
        {
            PlayNextSentence();
        }
    }
    
    //-------------

    //사람의 대화 출력
    private IEnumerator TypingDlg(string text, Speaker curSpeaker)
    {
        nextArrow.enabled = false;
        
        dlgState = DlgState.TYPING;
        dlgText.text = "";
        int dlgWordIdx = 0;

        //말하는 애 설정
        speaker.text = curSpeaker.speakerName;

        //대화 출력하는 부분
        while(dlgWordIdx != text.Length)
        {
            dlgText.text += text[dlgWordIdx++];
            yield return new WaitForSeconds(0.05f);
        }
        dlgState = DlgState.DONE;
        
        nextArrow.enabled = true;
    }
    
    //사람이 아닌 거에 대한 대화 출력
    private IEnumerator TypingDlg(string text)
    {
        nextArrow.enabled = false;
        
        dlgState = DlgState.TYPING;
        dlgText.text = "";
        int dlgWordIdx = 0;

        speaker.text = "";

        //대화 출력하는 부분
        while(dlgWordIdx != text.Length)
        {
            dlgText.text += text[dlgWordIdx++];
            yield return new WaitForSeconds(0.05f);
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
            //새로운 대화로 설정
            setenceIdx = 0;
            curDlg = curDlg.nextDlg;
        }
    }

    //캐릭터 처음 등장 시키기
    public void ShowChrImg()
    {
        //등장할 캐릭터가 1명인 경우
        if(curDlg.speakers.Count<2)
        {
            //이미지 설정
            speakerImg[curDlg.speakers[0].imgIdx].sprite = curDlg.speakers[0].speaker.speakerSprite;
            //이미지 보이기
            speakerImg[curDlg.speakers[0].imgIdx].enabled=true;
        }
        //등장할 캐릭터가 2명이상인 경우
        else
        {
            //출현시킬 캐릭터 설정
            SetChrImg();
            //출현할 캐릭터 보이기
            for(int i = 0; i<curDlg.speakers.Count; i++)
            {
                //이미지 설정
                speakerImg[curDlg.speakers[i].imgIdx].sprite = curDlg.speakers[i].speaker.speakerSprite;
                //이미지 보이기
                speakerImg[curDlg.speakers[i].imgIdx].enabled=true;
            }
        }
    }

    //캐릭터 이미지 설정
    public void SetChrImg()
    {
        //현재 말하고 있는 캐릭터 인덱스랑 다르면 말하지 않는 것으로 설정
        for(int i = 0; i<curDlg.speakers.Count; i++)
        {
            if(i!=curDlg.sentences[setenceIdx].speakerIdx)
            {
                //어둡게 설정
                ChgImgToDark(speakerImg[curDlg.speakers[i].imgIdx]);
                //작게 설정
                SetImgScale(speakerRectTr[curDlg.speakers[i].imgIdx], 0.8f);
            }
            //말하고 있는 캐릭터 설정
            else
            {
                ChgImgToBright(speakerImg[curDlg.speakers[i].imgIdx]);
                SetImgScale(speakerRectTr[curDlg.speakers[i].imgIdx], 1f);
            }
        }
    }

    //이미지 크기 설정
    public void SetImgScale(RectTransform rectTr, float scale)
    {
        rectTr.localScale = new Vector3(scale, scale);
    }
    //이미지 어둡게 변경
    public void ChgImgToDark(Image img)
    {
        img.color = imgToDark;
    }
    //이미지 밝게 변경
    public void ChgImgToBright(Image img)
    {
        img.color = imgToBright;
    }

    //처음 대화 시작
    public void StartDlg()
    {
        //등장캐릭터가 나오는 경우에만 캐릭터 설정
        //오브젝트인 경우에는 설정x
        if(curDlg.speakers.Count>0)
        {
            ShowChrImg();
        }
        //사람인 경우
        if(curDlg.speakers[curDlg.sentences[setenceIdx].speakerIdx].speaker != null)
        {
            StartCoroutine(TypingDlg(curDlg.sentences[setenceIdx].dlgTexts, curDlg.speakers[curDlg.sentences[setenceIdx].speakerIdx].speaker));
        }
        //오브젝트인 경우
        else
        {
            StartCoroutine(TypingDlg(curDlg.sentences[setenceIdx].dlgTexts));
        }
        setenceIdx++;
    }

    public void PlayNextSentence()
    {
        //사람인 경우
        if(curDlg.speakers[curDlg.sentences[setenceIdx].speakerIdx].speaker != null)
        {
            SetChrImg();
            StartCoroutine(TypingDlg(curDlg.sentences[setenceIdx].dlgTexts, curDlg.speakers[curDlg.sentences[setenceIdx].speakerIdx].speaker));
        }
        //오브젝트인 경우
        else
        {
            StartCoroutine(TypingDlg(curDlg.sentences[setenceIdx].dlgTexts));
        }
        setenceIdx++;
    }
}
