using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField]
    private int setenceIdx = 0;
    public float typingSpeed = 0.1f;

    private Color imgToDark = new Color32(100,100,100,255);
    private Color imgToBright = new Color32(255,255,255,255);

    //대화창 기본 위치
    private Vector2 dlgPos;

    //대사 출력 시 태그 여부
    bool isTag = false;

    IEnumerator typingIEnumerator = null;

    [HideInInspector] public bool isShowDlg = false;

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
        set
        {
            dlgState = value;
        }
    }

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

        if (LoadingManager.nextScene == "Ending")
        {
            Destroy(gameObject);
        }

        //대화창 텍스트 초기설정
        dlgText.text = "";
        speaker.text = "";
    }

    private void Start() {
        speakerImg = speakersObj.GetComponent<Image>();
        speakerRectTr = speakersObj.GetComponent<RectTransform>();
        dlgPos = DlgRT.anchoredPosition;
    }
    private void OnEnable()
    {
        //이거 켜져 있으면 각각 씬에서 대사 테스트 안됨
        SceneManager.sceneLoaded += LoadSceneEvent;
    }
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoadingScene" && scene.name != "Title" && scene.name != "Intro" && scene != null)
        {
            if (scene.buildIndex - 1 > -1 && scene.buildIndex - 1 < dialogueList.Count)
            {
                _dlgState = DlgState.End;
                SetCurDlg(scene.buildIndex - 1);
            }
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    public void SetCurDlg(int idx)
    {
        curDlg = dialogueList[idx];
    }

    //===========
    //대화 출력
    private IEnumerator TypingDlg(string text, string curSpeakerName)
    {
        nextArrow.enabled = false;
        
        dlgState = DlgState.TYPING;
        dlgText.text = "";
        int dlgWordIdx = 0;

        if(!isSingleDlg)
        {
            //말하는 애 설정
            if(curDlg.sentences[setenceIdx].speakerIdx!=-1)
            {
                speaker.text = curSpeakerName;
            }
            else
            {
                speaker.text = "";
            }
        }
        else
        {
            if(singleDlg.sentences[setenceIdx].speakerIdx!=-1)
            { speaker.text = curSpeakerName; }
            else
            { speaker.text = ""; }
        }

        //대화 출력하는 부분
        while(dlgWordIdx != text.Length)
        {

            if (text[dlgWordIdx] == '<')
            {
                isTag = true;
            }
            else if(text[dlgWordIdx] == '>')
            {
                isTag = false;
            }

            dlgText.text += text[dlgWordIdx++];

            if (!isTag)
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        dlgState = DlgState.DONE;
        
        nextArrow.enabled = true;
        typingIEnumerator = null;
    }

    //대화창 보이기
    private void DialogueShow()
    {
        isShowDlg = true;
        if (!PlayerInfoData.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing"))
        {
            PlayerInfoData.instance.playerAnim.SetBool("walk", false);
            PlayerInfoData.instance.playerAnim.SetBool("jump", false);
        }

        if (!speakerImg.enabled)
        {
            DlgRT.offsetMin = new Vector2(640f, DlgRT.offsetMin.y);
            DlgRT.offsetMax = new Vector2(-185f, DlgRT.offsetMax.y);
        }
        else
        {
            DlgRT.offsetMin = new Vector2(350f, DlgRT.offsetMin.y);
            DlgRT.offsetMax = new Vector2(-350f, DlgRT.offsetMax.y);
        }
        DlgBg.enabled = true;
        dlgState = DlgState.Start;
    }
    private void DialogueShow(Dialogue dlg)
    {
        isShowDlg = true;
        if (!PlayerInfoData.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing"))
        {
            PlayerInfoData.instance.playerAnim.SetBool("walk", false);
            PlayerInfoData.instance.playerAnim.SetBool("jump", false);
        }
        if (!speakerImg.enabled)
        {
            DlgRT.offsetMin = new Vector2(640f, DlgRT.offsetMin.y);
            DlgRT.offsetMax = new Vector2(-185f, DlgRT.offsetMax.y);
        }
        else
        {
            DlgRT.offsetMin = new Vector2(350f, DlgRT.offsetMin.y);
            DlgRT.offsetMax = new Vector2(-350f, DlgRT.offsetMax.y);
        }
        DlgBg.enabled = true;
        dlgState = DlgState.Start;
    }
    //대화창 숨기기
    public void DialogueHide()
    {
        isShowDlg = false;
        DlgBg.enabled = false;
        
        dlgText.text = "";
        speaker.text = "";
        nextArrow.enabled = false;
        dlgState = DlgState.End;
        HideChrImg();

        SetNextDlg(); 

        if (TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
        {
            TimelineManager.instance.timelineController.SetTimelineResume();
        }
        else
        {
            if(TimelineManager.instance._Tlstate != TimelineManager.TlState.End)
            {
                TimelineManager.instance._Tlstate = TimelineManager.TlState.Resume;
            }
        }



        if(curDlg.sentences[setenceIdx].eventType == Dialogue.EventType.RunGameEventAfterDlg)
        {
            curDlg.gmEvent.Raise();
        }

        isSingleDlg = false;
    }
    //대화창 숨기기
    public void DialogueHide(Dialogue dlg)
    {
        isShowDlg = false;
        DlgBg.enabled = false;
        
        dlgText.text = "";
        speaker.text = "";
        nextArrow.enabled = false;
        dlgState = DlgState.End;
        HideChrImg();

        if (dlg.sentences[setenceIdx-1].eventType == Dialogue.EventType.RunGameEventAfterDlg)
        {
            dlg.gmEvent.Raise();
        }
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
        Dialogue dlg;
        if(!isSingleDlg)
        {
            dlg = curDlg;
        }
        else
        {
            dlg = singleDlg;
        }

        //이미지 설정
        //이미지가 있는 경우에만 설정
        if(setenceIdx < dlg.sentences.Count && dlg.sentences[setenceIdx].speakerIdx < dlg.speakers.Count && dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerSprite != null)
        {
            if(!speakerImg.enabled){speakerImg.enabled = true;}
            speakerImg.sprite = dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerSprite;
            speakerImg.color = dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerColor;
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
            speakerImg.color = dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerColor;
        }
        else
        {
            speakerImg.enabled = false;
        }
    }

    //처음 대화 시작
    public void PlayDlg()
    {
        if (SmartphoneManager.instance != null)
            SmartphoneManager.instance.phone.IsOKSendTalk=false;

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
        setenceIdx = 0;
        isSingleDlg = true;
        singleDlg = dlg;
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
        if(dlgState != DlgState.TYPING)
        {
            if (curDlg.sentences[setenceIdx].eventType == Dialogue.EventType.Timeline && TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
            {
                TimelineManager.instance.timelineController.SetTimelineResume();
            }

            SetChrImg();

            //사람인경우
            if (curDlg.sentences[setenceIdx].speakerIdx != -1)
            {
                typingIEnumerator = TypingDlg(curDlg.sentences[setenceIdx].dlgTexts, curDlg.speakers[curDlg.sentences[setenceIdx].speakerIdx].speakerName);
                StartCoroutine(typingIEnumerator);
            }
            //사람 아닌경우
            else
            {
                typingIEnumerator = TypingDlg(curDlg.sentences[setenceIdx].dlgTexts, "");
                StartCoroutine(typingIEnumerator);
            }
            setenceIdx++;
        }
    }

    //다음 대화 진행
    public void PlaySentence(Dialogue dlg)
    {
        if (dlgState != DlgState.TYPING)
        {
            DlgRT.anchoredPosition = dlgPos;
            if (dlg.sentences[setenceIdx].speakerIdx == -1)
            {
                DlgRT.anchoredPosition -= (Vector2.right * 300);
            }

            if (dlg.sentences[setenceIdx].eventType == Dialogue.EventType.Timeline && TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
            {
                TimelineManager.instance.timelineController.SetTimelineResume();
            }

            SetChrImg();

            //사람인경우
            if (dlg.sentences[setenceIdx].speakerIdx != -1)
            {
                typingIEnumerator = TypingDlg(dlg.sentences[setenceIdx].dlgTexts, dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerName);
                StartCoroutine(typingIEnumerator);
            }
            //사람 아닌경우
            else
            {
                typingIEnumerator = TypingDlg(dlg.sentences[setenceIdx].dlgTexts, "");
                StartCoroutine(typingIEnumerator);
            }
            setenceIdx++;
        }
    }

    public void OutPutDialogue(Dialogue dlg)
    {
        if(SmartphoneManager.instance != null)
            SmartphoneManager.instance.phone.IsOKSendTalk=false;
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
            TimelineManager.instance.timelineController.SetTimelineResume();
        }

        SetChrImg();
        //사람인경우
        if(dlg.sentences[setenceIdx].speakerIdx!=-1){
            typingIEnumerator = TypingDlg(dlg.sentences[setenceIdx].dlgTexts, dlg.speakers[dlg.sentences[setenceIdx].speakerIdx].speakerName);
            StartCoroutine(typingIEnumerator);
        }
        //사람 아닌경우
        else
        {
            typingIEnumerator = TypingDlg(dlg.sentences[setenceIdx].dlgTexts, "");
            StartCoroutine(typingIEnumerator);
        }
        setenceIdx++;
    }
}
