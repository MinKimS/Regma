using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    void FixedUpdate()
    {
        if(RespawnManager.isGameOver)
        {
            go_BaseUI.SetActive(true);
            AudioManager.instance.StopSFXAll();
            if(!
            AudioManager.instance.CheckAudioPlaying("주방_괴생명체1 도원발견"))
            {
                AudioManager.instance.SFXPlay("주방_괴생명체1 도원발견");
            }    
            //print("게임오버");
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
        //SceneManager.LoadScene("Title");
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
        SceneMapMenuController.instance.MoveScene("Title");
        Debug.Log("나가기");
    }
}
