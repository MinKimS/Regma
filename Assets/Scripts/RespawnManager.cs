using System.Collections;
using System.Collections.Generic;
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
    public Transform respawnPosition;
    [SerializeField] Chmoving chmoving;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnUpdateRespawnPoint.AddListener((targetPosition) => {
            respawnPosition = targetPosition;
        });

        OnGameOver.AddListener(() =>
        {
            GoToGameOverScene();
            chmoving.StartCoroutine(chmoving.RespawnCharacterAfterWhile(respawnPosition, 3f));
            // ���� ���� ���� �� �ٸ� ������ ��ȯ�ϴ� �Լ� ȣ��

        });

        ChangeUpdatingMethod(ChangeMethod.DamageBased);
    }

    public void ChangeUpdatingMethod(ChangeMethod type)
    {
        currentMethod = type;
        print(type);
    }

    // ���� ���� �� ȣ���Ͽ� �ٸ� ������ ��ȯ�ϴ� �Լ�
    void GoToGameOverScene()
    {
        // ���⿡ ���� ���� �� ��ȯ�� ���� �̸��� �Է��մϴ�.
        string gameOverSceneName = "GameOverScene"; // ����: ���� �� �̸����� ����

        // ���� �� �̸����� ����� �κ��Դϴ�.
        SceneManager.LoadScene(gameOverSceneName);
    }
}