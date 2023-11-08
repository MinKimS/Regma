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
        SetInitialBtn();
        SetFullScreen();
    }
    private void Update()
    {
        //화면에 보이기/숨기기
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "LoadingScene" && SceneManager.GetActiveScene().name != "Title")
        {
            if(!controlExplainScreen.activeSelf)
            {
                menuPanel.gameObject.SetActive(!menuPanel.gameObject.activeSelf);
            }
            else
            {
                SetShowOrHideHowToControlling();
            }
        }

        //변경하고자 하는 세팅 선택
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if(btnIdx > 0)
            {
                SetBtn(false);
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if(btnIdx < btnList.Length-1)
            {
                SetBtn(true);
            }
        }
        //선택한 세팅 변경
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SetSetting(false);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            SetSetting(true);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(btnIdx == 2)
            {
                SetShowOrHideHowToControlling();
            }
            else if(btnIdx == btnList.Length-1)
            {
                menuPanel.gameObject.SetActive(false);
                LoadingManager.LoadScene("Title");
            }    
        }
    }

    void SetBtn(bool isPlus)
    {
        btnList[btnIdx].color = Color.white;

        if (isPlus) { btnIdx++; }
        else { btnIdx--; }

        btnList[btnIdx].color = Color.gray;
    }

    void SetInitialBtn()
    {
        btnIdx = btnList.Length-1;
        btnList[btnIdx].color = Color.gray;
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
            print("full " + fullIdx);
            Screen.fullScreen = true;
            fullScreenText.text = "FullScreen";
        }
        else if(fullIdx == 1)
        {
            print("windowed " + fullIdx);
            Screen.fullScreen = false;
            fullScreenText.text = "Windowed";
        }
    }

    void SetResolution()
    {
        print("function2 " + resolutionIdx);

        ResolutionText.text = resolutionList[resolutionIdx];
        
        Resolution resolution = resolutions[resolutionIdx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //조작법 숨기기 or 보이기
    void SetShowOrHideHowToControlling()
    {
        print("howto");

        controlExplainScreen.SetActive(!controlExplainScreen.activeSelf);
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
