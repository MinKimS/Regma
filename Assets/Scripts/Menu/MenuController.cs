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
        //ȭ�鿡 ���̱�/�����
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
