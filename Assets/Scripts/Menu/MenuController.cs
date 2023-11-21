using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    Transform menuPanel;

    public Image[] btnList;
    int btnIdx = 0;

    Color selectColor = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
    Color unSelectColor = new Vector4(1f, 1f, 1f, 0.5f);

    int resolutionIdx = 0;
    int fullIdx = 0;
    public GameObject controlExplainScreen;
    public TextMeshProUGUI fullScreenText;
    public TextMeshProUGUI ResolutionText;

    Resolution[] resolutions;

    List<string> resolutionList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

        menuPanel = GetComponentsInChildren<Transform>()[1];
        resolutionList = new List<string>();
    }
    private void Start()
    {
        SetInitialResolution();
        menuPanel.gameObject.SetActive(false);
        GameManager.instance.isMenuOpen = false;
        GameManager.instance.isHowtoOpen = false;
        SetInitialBtn();
        SetFullScreen();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadSceneEvent;
    }

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        SetInitialBtn();
        GameManager.instance.isMenuOpen = false;
        GameManager.instance.isHowtoOpen = false;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    private void Update()
    {
        //화면에 보이기/숨기기
        bool canInput = Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "LoadingScene"
            && SceneManager.GetActiveScene().name != "Ending"
            && !SmartphoneManager.instance.phone.IsOpenPhone
            && !SmartphoneManager.instance.inven.IsOpenInven
            && TimelineManager.instance.tlstate == TimelineManager.TlState.End;


        if (canInput)
        {
            if(!controlExplainScreen.activeSelf)
            {
                if (!DialogueManager.instance.isShowDlg)
                {
                    menuPanel.gameObject.SetActive(!menuPanel.gameObject.activeSelf);
                    GameManager.instance.isMenuOpen = menuPanel.gameObject.activeSelf ? true : false;

                    if (SceneManager.GetActiveScene().name == "Title")
                    {
                        btnList[btnList.Length - 1].gameObject.SetActive(false);
                    }
                    else
                    {
                        btnList[btnList.Length - 1].gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                SetShowOrHideHowToControlling();
            }
        }

        if(menuPanel.gameObject.activeSelf)
        {
            //변경하고자 하는 세팅 선택
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (btnIdx > 0)
                {
                    SetBtn(false);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (btnIdx < btnList.Length - 1 && btnList[btnList.Length - 1].gameObject.activeSelf || btnIdx < btnList.Length -2 && !btnList[btnList.Length - 1].gameObject.activeSelf)
                {
                    SetBtn(true);
                }
            }
            //선택한 세팅 변경
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                SetSetting(false);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                SetSetting(true);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (btnIdx == 2)
                {
                    SetShowOrHideHowToControlling();
                }
                else if (btnIdx == btnList.Length - 1)
                {
                    menuPanel.gameObject.SetActive(false);
                    GameManager.instance.isMenuOpen = false;
                    GameManager.instance.isHowtoOpen = false;
                    AudioManager.instance.StopSFXAll();
                    SmartphoneManager.instance.phone.HidePhone();
                    SmartphoneManager.instance.phone.DeleteTalkAll();
                    SmartphoneManager.instance.phone.HideSendTalk();
                    SmartphoneManager.instance.phone.SetCurTalk(0);
                    if(DialogueManager.instance.isShowDlg)
                    {
                        DialogueManager.instance.DialogueHide();
                    }
                    DialogueManager.instance.SetCurDlg(0);
                    TutorialController.instance.CloseTutorialScreen();
                    GameManager.instance.SetLastScene(SceneManager.GetActiveScene().name);

                    LoadingManager.LoadScene("Title");
                }
            }
        }
    }

    void SetBtn(bool isPlus)
    {
        btnList[btnIdx].color = unSelectColor;

        if (isPlus) { btnIdx++; }
        else { btnIdx--; }

        btnList[btnIdx].color = selectColor;
    }

    void SetInitialBtn()
    {
        btnIdx = 0;
        for(int i =1; i<btnList.Length; i++)
        {
            btnList[i].color = unSelectColor;
        }
        btnList[btnIdx].color = selectColor;
    }

    void SetSetting(bool isPlus)
    {
        if(!isPlus)
        {
            if (btnIdx == 0 && fullIdx > 0)
            {
                fullIdx--;
                SetFullScreen();
            }
            else if (btnIdx == 1 && resolutionIdx > 0)
            {
                resolutionIdx--;
                SetResolution();
            }
        }
        else
        {
            if (btnIdx == 0 && fullIdx < 1)
            {
                fullIdx++;
                SetFullScreen();
            }
            else if (btnIdx == 1 && resolutionIdx < resolutionList.Count-1)
            {
                resolutionIdx++;
                SetResolution();
            }
        }
    }

    void SetFullScreen()
    {
        if(fullIdx == 0)
        {
            Screen.fullScreen = true;
            fullScreenText.text = "FullScreen";
        }
        else if(fullIdx == 1)
        {
            Screen.fullScreen = false;
            fullScreenText.text = "Windowed";
        }
    }

    void SetResolution()
    {
        ResolutionText.text = resolutionList[resolutionIdx];
        
        Resolution resolution = resolutions[resolutionIdx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //조작법 숨기기 or 보이기
    void SetShowOrHideHowToControlling()
    {
        controlExplainScreen.SetActive(!controlExplainScreen.activeSelf);
        GameManager.instance.isHowtoOpen = controlExplainScreen.activeSelf ? true : false;
    }

    void SetInitialResolution()
    {
        resolutions = Screen.resolutions;
        resolutionList.Clear();
        resolutionIdx = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionList.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionIdx = i;
            }
        }

        SetResolution();
    }
}
