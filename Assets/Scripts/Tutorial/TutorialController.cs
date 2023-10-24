using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController instance;

    bool isTutorialShowing = false;
    public bool IsTutorialShowing
    {
        get { return isTutorialShowing; }
    }

    public TextMeshProUGUI tutorialText;

    Transform TutorialArea;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        TutorialArea = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            print("close tutorial screen");
            CloseTutorialScreen();
        }
    }

    public void OpenTutorialScreen()
    {
        TutorialArea.gameObject.SetActive(true);
        isTutorialShowing=true;
        SetTutorialText("hjklsdf");
    }

    void SetTutorialText(string str)
    {
        tutorialText.text = "";

        tutorialText.text = str;
    }

    void CloseTutorialScreen()
    {
        TutorialArea.gameObject.SetActive(false);
        isTutorialShowing=false;
        tutorialText.text = "";
    }
}
