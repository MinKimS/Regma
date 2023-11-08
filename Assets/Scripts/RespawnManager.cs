using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public enum ChangeMethod { MobBased, DamageBased }

    public ChangeMethod currentMethod = ChangeMethod.DamageBased;

    public static RespawnManager Instance;
    public UnityEvent<Transform> OnUpdateRespawnPoint = new UnityEvent<Transform>();
    public UnityEvent OnGameOver = new UnityEvent();
    public UnityEvent OnGameOverScene = new UnityEvent();
    public Transform respawnPosition;
    [SerializeField] Chmoving chmoving;
    public static bool isGameOver = false;
    public static bool isGameOverScene = false;


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnUpdateRespawnPoint.AddListener((targetPosition) => {
            respawnPosition = targetPosition;
            isGameOver = false;
        });

        OnGameOver.AddListener(() =>
        {
            isGameOver = true;
            
            chmoving.StartCoroutine(chmoving.RespawnCharacterAfterWhile(respawnPosition, 0.1f));
            
        });

        OnGameOverScene.AddListener(() => // �����ʿ��� �ٽ� ���¿�
        {
            isGameOver = true;
            GoToGameOverScene();

        });



    }

    public void ChangeUpdatingMethod(ChangeMethod type)
    {
        currentMethod = type;
        print(type);



    }

    void GoToGameOverScene()
    {
        // ���� ���� �� ��ȯ�� ���� �̸��� �Է��մϴ�.
        string gameOverSceneName = "Bath"; // ���� �� �̸����� ����

        // ���� �� �̸����� ����� �κ��Դϴ�.
        SceneManager.LoadScene(gameOverSceneName);
    }

}