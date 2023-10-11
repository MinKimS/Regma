using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    Transform menuPanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

        menuPanel = GetComponentsInChildren<Transform>()[1];
    }
    private void Start()
    {
        menuPanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        //화면에 보이기/숨기기
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.gameObject.SetActive(!menuPanel.gameObject.activeSelf);
        }
    }

    public void Btn1()
    {
        print("function1");
    }

    public void Btn2()
    {
        print("function2");
    }
}
