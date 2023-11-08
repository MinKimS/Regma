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
    public Transform respawnPosition;
    [SerializeField] Chmoving chmoving;
    public static bool isGameOver = false;


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
            //GoToGameOverScene();
            chmoving.StartCoroutine(chmoving.RespawnCharacterAfterWhile(respawnPosition, 0.1f));
            
            
        });

        //ChangeUpdatingMethod(ChangeMethod.DamageBased);
    }

    public void ChangeUpdatingMethod(ChangeMethod type)
    {
        currentMethod = type;
        print(type);



    }
}