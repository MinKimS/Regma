using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverMng : MonoBehaviour
{
    [SerializeField] private SaveLoad theSaveNLoad;
    [SerializeField] private GameObject go_BaseUI;

    // Start is called before the first frame update
    void Start()
    {
        go_BaseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(RespawnManager.isGameOver)
        {
            go_BaseUI.SetActive(true);
            print("게임오버");
            Time.timeScale = 0f;
        }
    }

    public void OnClickReStart()
    {
        //theSaveNLoad.SaveData();
        RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(transform);
        Time.timeScale = 1f;
        go_BaseUI.SetActive(false);
        Debug.Log("재시작");
    }

    public void OnClickExit()
    {
        //메인메뉴씬으로 이동? 
        Debug.Log("나가기");
    }
}
