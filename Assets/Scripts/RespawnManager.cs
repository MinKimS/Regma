using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static RespawnManager;

[System.Serializable]
public class RespawnUpdateEvent : UnityEvent<RespawnManager.ChangeMethod, Transform>
{
}

public class RespawnManager : MonoBehaviour
{
    public enum ChangeMethod { MobBased, DamageBased }

    public static RespawnManager Instance;
    public RespawnUpdateEvent OnUpdateRespawnPoint = new RespawnUpdateEvent();
    public UnityEvent<ChangeMethod> OnGameOver = new UnityEvent<ChangeMethod>();
    public UnityEvent OnGameOverScene = new UnityEvent();
    //public Transform respawnPosition;
    [SerializeField] Chmoving chmoving;
    public static bool isGameOver = false;
    public static bool isGameOverScene = false;
    public Dictionary<ChangeMethod, Transform> respawnPositionByType = new Dictionary<ChangeMethod, Transform>()
    {
        {   ChangeMethod.MobBased, null},
        { ChangeMethod.DamageBased, null},
    };


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnUpdateRespawnPoint.AddListener((method, targetPosition) => {
            respawnPositionByType[method] = targetPosition;
            isGameOver = false;
        });

        OnGameOver.AddListener((method) =>
        {
            isGameOver = true;
            chmoving.StartCoroutine(chmoving.RespawnCharacterAfterWhile(respawnPositionByType[method], 0.1f));
            
        });

        OnGameOverScene.AddListener(() => // 욕조맵에서 다시 리셋용
        {
            isGameOver = true;
            GoToGameOverScene();

        });



    }

    /*
    public void ChangeUpdatingMethod(ChangeMethod type)
    {
        currentMethod = type;
        print(type);



    }
    */

    void GoToGameOverScene()
    {
        // 게임 오버 시 전환할 씬의 이름을 입력합니다.
        string gameOverSceneName = "Bath"; // 실제 씬 이름으로 변경
        AudioManager.instance.StopSFXAll();
        // 실제 씬 이름으로 변경된 부분입니다.
        SceneManager.LoadScene(gameOverSceneName);
    }

}