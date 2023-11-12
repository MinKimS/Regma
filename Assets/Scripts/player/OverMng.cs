using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OverMng : MonoBehaviour
{
    [SerializeField] private SaveLoad theSaveNLoad;
    [SerializeField] private GameObject go_BaseUI;
    [SerializeField] private RespawnManager respawnManager;
    

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
        //RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(transform);
        //respawnManager.OnUpdateRespawnPoint.Invoke(respawnManager.respawnPosition);

        if (respawnManager != null && respawnManager.respawnPositionByType[RespawnManager.ChangeMethod.MobBased] != null) // Null 체크를 추가하여 오류 방지
        {
            respawnManager.OnUpdateRespawnPoint.Invoke(RespawnManager.ChangeMethod.MobBased, respawnManager.respawnPositionByType[RespawnManager.ChangeMethod.MobBased]);
        }
        else
        {
            RespawnManager.Instance.OnUpdateRespawnPoint.Invoke(RespawnManager.ChangeMethod.MobBased, transform);
            Debug.LogWarning("RespawnManager나 respawnPosition이 null입니다.");
        }

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
