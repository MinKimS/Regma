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
            print("���ӿ���");
            Time.timeScale = 0f;
        }
    }

    public void OnClickReStart()
    {
        //theSaveNLoad.SaveData();
        RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(transform);
        Time.timeScale = 1f;
        go_BaseUI.SetActive(false);
        Debug.Log("�����");
    }

    public void OnClickExit()
    {
        //���θ޴������� �̵�? 
        Debug.Log("������");
    }
}
