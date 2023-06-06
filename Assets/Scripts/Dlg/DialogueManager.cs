using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    //모든 씬에 나올 첫번째 대화들
    public List<Dialogue> dialogueList;
    //현재 출력될 대화
    public Dialogue curDlg;
    public Dialogue singleDlg;


    //대화 출력여부 확인용
    public enum DlgState
    {
        Start,
        TYPING,
        DONE,
        End
    }
    DlgState dlgState = DlgState.End;

    public bool isSingleDlg = false;

    //대화텍스트
    public TextMeshProUGUI dlgText;
    //화자이름
    public TextMeshProUGUI speaker;
    //대화창 배경
    public Image DlgBg;
    public RectTransform DlgRT;

    //화자 오브젝트
    public GameObject speakersObj;
    //화자 이미지들
    private Image speakerImg;
    //화자 크기
    private RectTransform speakerRectTr;

    //말 끝남표시(확인용)
    public Image nextArrow;
    //현재 출력되는 대화 인덱스
    private int setenceIdx = 0;
    public float typingSpeed = 0.1f;

    private Color imgToDark = new Color32(100,100,100,255);
    private Color imgToBright = new Color32(255,255,255,255);

    //대화창 기본 위치
    private Vector2 dlgPos;

    public int SetenceIdx
    {
        get
        {
            return setenceIdx;
        }
    }

    public DlgState _dlgState
    {
        get
        {
            return dlgState;
        }
    }

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

        //대화창 텍스트 초기설정
        dlgText.text = "";
        speaker.text = "";
    }

    private void Start() {
        speakerImg = speakersObj.GetComponent<Image>();
        speakerRectTr = speakersObj.GetComponent<RectTransform>();
        curDlg = dialogueList[1];
        dlgPos = DlgRT.anchoredPosition;
        SceneManager.sceneLoaded += SceneChangeEvent;
    }

    //씬 변경 시 수행될 이벤트들
    void SceneChangeEvent(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "LoadingScene")
        {
            curDlg = dialogueList[1];
        }
    }

    //===========
    //대화 출력
    private IEnumerator TypingDlg(string text, string curSpeakerName)
    {
        nextArrow.enabled = false;
        
        dlgState = DlgState.TYPING;
        dlgText.text = "";
        int dlgWordIdx = 0;

        //말하는 애 설정
        if(curDlg.sentences[setenceIdx].speakerIdx!=-1)
        {
            speaker.text = curSpeakerName;
        }
        else
        {
            speaker.text = "";
        }

        //대화 출력하는 부분
        while(dlgWordIdx != text.Length)
        {
            dlgText.text += text[dlgWordIdx++];
            yield return new WaitForSeconds(typingSpeed);
        }
        dlgState = DlgState.DONE;
        
        nextArrow.enabled = true;
    }

    //대화창 보이기
    private void DialogueShow()
    {
        if(curDlg.sentences[setenceIdx].speakerIdx == -1)
        {
            DlgRT.anchoredPosition -= (Vector2.right*300);
        }
        DlgBg.enabled = true;
        dlgState = DlgState.Start;
    }
    private void DialogueShow(Dialogue dlg)
    {
        if(dlg.sentences[setenceIdx].speakerIdx == -1)
        {
            DlgRT.anchoredPosition -= (Vector2.right*300);
        }
        DlgBg.enabled = true;
        dlgState = DlgState.Start;
    }
    //대화창 숨기기
    public void DialogueHide()
    {
        if(curDlg.sentences[setenceIdx-1].speakerIdx == -1)
        {
            DlgRT.anchoredPosition += (Vector2.right*300);
        }
        DlgBg.enabled = false;
        
        dlgText.text = "";
        speaker.text = "";
        nextArrow.enabled = false;
        dlgState = DlgState.End;
        HideChrImg();

        SetNextDlg();

        if(TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
        {
            TimelineManager.instance.SetTimelineResume();
        }
        else
        {
            TimelineManager.instance._Tlstate = TimelineManager.TlState.Resume;
        }
    }
    //대화창 숨기기
    public void DialogueHide(Dialogue dlg)
    {
        if(dlg.sentences[setenceIdx-1].speakerIdx == -1)
        {
            DlgRT.anchoredPosition = dlgPos;
        }
        DlgBg.enabled = false;
        
        dlgText.text = "";
        speaker.text = "";
        nextArrow.enabled = false;
        dlgState = DlgState.End;
        HideChrImg();

        SetNextDlg(dlg);
        isSingleDlg = false;
    }
    //다음 새로운 대화로 설정
    public void SetNextDlg()
    {
        setenceIdx = 0;
        if(curDlg.nextDlg != null)
        {
            //새로운 대화로 설정
            curDlg = curDlg.nextDlg;
        }
    }
    //다음 새로운 대화로 설정
    public void SetNextDlg(Dialogue dlg)
    {
        setenceIdx = 0;
        if(dlg.nextDlg != null)
        {
            //새로운 대화로 설정
            dlg = dlg.nextDlg;
        }
    }

    //캐릭터 처음 등장 시키기
    private void ShowChrImg()
    {
        //이미지 보이기
        speakerImg.enabled=true;
    }

    //등장했던 캐릭터 숨기기
    private void HideChrImg()
    {
        //이미지 숨기기
        speakerImg.enabled=false;
    }

    //캐릭터 이미지 설정
    private void SetChrImg()
    {
        //이미지 설정
        //사람인경우
        if(curDlg.sentences[setenceIdx].speakerIdx!=-1)
        {
            if(!speakerImg.enabled){speakerImg.enabled = true;}
            speakerImg.sprite = curDlg.speakers[curDlg.sentences[setenceIdx].speakerIdx].speakerSprite;
        }
        else
        {
            speakerImg.enabled = false;
        }
    }

    //캐릭터 이미지 설정
    private void SetChrImg(Dialogue dlg)
    {
        //이미지 설정
        //사람인경우
        if(dlg.sentences[setenceIdx].speakerIdx!=-1)
        {
            if(!speakerImg.enabled){speakerImg.enabled = true;}
            speakerImg.sprite = dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerSprite;
        }
        else
        {
            speakerImg.enabled = false;
        }
    }

    //처음 대화 시작
    public void PlayDlg()
    {
        if(SmartphoneManager.instance != null)
            SmartphoneManager.instance.isOKSendTalk=false;
        DialogueShow();
        //등장캐릭터가 나오는 경우에만 캐릭터 설정
        //오브젝트인 경우에는 설정x
        if(curDlg.speakers.Count>0)
        {
            ShowChrImg();
            SetChrImg();
        }
        //대화 진행
        PlaySentence();
    }

    //처음 대화 시작(흐름없이 나오는 중간에 언제나 나올 수 있는 대화)
    public void PlayDlg(Dialogue dlg)
    {
        isSingleDlg = true;
        singleDlg = dlg;
        if(SmartphoneManager.instance != null)
            SmartphoneManager.instance.isOKSendTalk=false;
        DialogueShow(dlg);
        //등장캐릭터가 나오는 경우에만 캐릭터 설정
        //오브젝트인 경우에는 설정x
        if(dlg.speakers.Count>0)
        {
            ShowChrImg();
            SetChrImg(dlg);
        }
        //대화 진행
        PlaySentence(dlg);
    }

    //다음 대화 진행
    public void PlaySentence()
    {
        if(curDlg.sentences[setenceIdx].eventType == Dialogue.EventType.Timeline && TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
        {
            TimelineManager.instance.SetTimelineResume();
        }

        //사람인경우
        if(curDlg.sentences[setenceIdx].speakerIdx!=-1){
            StartCoroutine(TypingDlg(curDlg.sentences[setenceIdx].dlgTexts, curDlg.speakers[curDlg.sentences[setenceIdx].speakerIdx].speakerName));
        }
        //사람 아닌경우
        else
        {
            StartCoroutine(TypingDlg(curDlg.sentences[setenceIdx].dlgTexts, ""));
        }
        setenceIdx++;
    }

    //다음 대화 진행
    public void PlaySentence(Dialogue dlg)
    {
        DlgRT.anchoredPosition = dlgPos;
        if(dlg.sentences[setenceIdx].speakerIdx == -1)
        {
            DlgRT.anchoredPosition -= (Vector2.right*300);
        }

        if(dlg.sentences[setenceIdx].eventType == Dialogue.EventType.Timeline && TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
        {
            TimelineManager.instance.SetTimelineResume();
        }

        //사람인경우
        if(dlg.sentences[setenceIdx].speakerIdx!=-1){
            StartCoroutine(TypingDlg(dlg.sentences[setenceIdx].dlgTexts, dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerName));
        }
        //사람 아닌경우
        else
        {
            StartCoroutine(TypingDlg(dlg.sentences[setenceIdx].dlgTexts, ""));
        }
        setenceIdx++;
    }

    public void OutPutDialogue(Dialogue dlg)
    {
        if(SmartphoneManager.instance != null)
            SmartphoneManager.instance.isOKSendTalk=false;
        DialogueShow();
        //등장캐릭터가 나오는 경우에만 캐릭터 설정
        //오브젝트인 경우에는 설정x
        if(dlg.speakers.Count>0)
        {
            ShowChrImg();
            if(dlg.sentences[setenceIdx].speakerIdx!=-1)
            {
                if(!speakerImg.enabled){speakerImg.enabled = true;}
                speakerImg.sprite = dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerSprite;
            }
            else
            {
                speakerImg.enabled = false;
            }
        }
        
        //대화 진행
        if(dlg.sentences[setenceIdx].eventType == Dialogue.EventType.Timeline && TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
        {
            TimelineManager.instance.SetTimelineResume();
        }

        SetChrImg();
        //사람인경우
        if(dlg.sentences[setenceIdx].speakerIdx!=-1){
            StartCoroutine(TypingDlg(dlg.sentences[setenceIdx].dlgTexts, dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerName));
        }
        //사람 아닌경우
        else
        {
            StartCoroutine(TypingDlg(dlg.sentences[setenceIdx].dlgTexts, ""));
        }
        setenceIdx++;
    }
}
