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
            //���� Ŭ�������� ���ӿ��� ���� �ڵ����� ���ͽ�Ű�� �Լ� ����
        });

        ChangeUpdatingMethod(ChangeMethod.DamageBased);

    }
    public void ChangeUpdatingMethod(ChangeMethod type)
    {
        currentMethod = type;
        print(type);
    }
}
