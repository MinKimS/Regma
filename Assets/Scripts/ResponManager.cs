using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResponManager : MonoBehaviour
{
    public enum ChangeMethod { MobBased, DamageBased }

    public ChangeMethod currentMethod = ChangeMethod.DamageBased;

    public static ResponManager Instance;
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
            //respawnPosition
            chmoving.StartCoroutine(chmoving.RespawnCharacterAfterWhile(respawnPosition, 3f));
            //여기 클래스에서 게임오버 이후 자동으로 복귀시키는 함수 구현
        });

        ChangeUpdatingMethod(ChangeMethod.DamageBased);

    }
    public void ChangeUpdatingMethod(ChangeMethod type)
    {
        currentMethod = type;
        print(type);
    }
}
