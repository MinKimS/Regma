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

        OnGameOverScene.AddListener(() => // �����ʿ��� �ٽ� ���¿�
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
        // ���� ���� �� ��ȯ�� ���� �̸��� �Է��մϴ�.
        string gameOverSceneName = "Bath"; // ���� �� �̸����� ����
        AudioManager.instance.StopSFXAll();
        // ���� �� �̸����� ����� �κ��Դϴ�.
        SceneManager.LoadScene(gameOverSceneName);
    }

}