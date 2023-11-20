using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    //start btn
    int btnSelectIdx = 2;
    public Image[] btnLists;
    public Transform howToScreen;

    private void Start() {
        SelectBtn();
    }
    void Update()
    {
        //왼쪽의 버튼으로 이동
        if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))&&!GameManager.instance.isMenuOpen && !GameManager.instance.isHowtoOpen)
        {
            if(btnSelectIdx < btnLists.Length-1)
            {
                if (GameManager.instance.GetLastScene() == SceneManager.GetActiveScene().name)
                {
                    btnSelectIdx += 2;
                    SelectBtn();
                }
                else
                {
                    btnSelectIdx++;
                    SelectBtn();
                }
            }
        }
        //오른쪽의 버튼으로 이동
        if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !GameManager.instance.isMenuOpen && !GameManager.instance.isHowtoOpen)
        {
            if(btnSelectIdx > 0)
            {
                if(GameManager.instance.GetLastScene() == SceneManager.GetActiveScene().name)
                {
                    btnSelectIdx -= 2;
                    SelectBtn();
                }
                else
                {
                    btnSelectIdx--;
                    SelectBtn();
                }
            }
        }
        //선택된 버튼 클릭
        if(Input.GetKeyDown(KeyCode.Return)&& !GameManager.instance.isMenuOpen&& !GameManager.instance.isHowtoOpen)
        {
            if(btnSelectIdx == 2)
            {
                LoadingManager.LoadScene("Intro");
            }
            else if(btnSelectIdx == 1)
            {
                if(GameManager.instance.GetLastScene() != SceneManager.GetActiveScene().name)
                {
                    LoadingManager.LoadScene(GameManager.instance.GetLastScene());
                }
            }
            else
            {
                Application.Quit();
                print("quit");
            }
        }
    }

    void SelectBtn()
    {
        btnLists[btnSelectIdx].color = Color.white;
        for(int i = 0; i < btnLists.Length; i++)
        {
            if(i == btnSelectIdx)
            {continue;}
            btnLists[i].color = Color.gray;
        }
    }
}
