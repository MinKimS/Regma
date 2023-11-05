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
            // 이후 게임 오버 시 다른 씬으로 전환하는 함수 호출

        });

        ChangeUpdatingMethod(ChangeMethod.DamageBased);
    }

    public void ChangeUpdatingMethod(ChangeMethod type)
    {
        currentMethod = type;
        print(type);
    }

    // 게임 오버 시 호출하여 다른 씬으로 전환하는 함수
    void GoToGameOverScene()
    {
        // 여기에 게임 오버 시 전환할 씬의 이름을 입력합니다.
        string gameOverSceneName = "GameOverScene"; // 예시: 실제 씬 이름으로 변경

        // 실제 씬 이름으로 변경된 부분입니다.
        SceneManager.LoadScene(gameOverSceneName);
    }
}