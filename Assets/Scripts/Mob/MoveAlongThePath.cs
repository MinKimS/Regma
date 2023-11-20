using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveAlongThePath : MonoBehaviour
{
    PathFinder pathFinder;
    Transform targetPos;
    public float traceSpeed = 3f;
    Node node = null;

    SpriteRenderer sp;
    BoxCollider2D bc;

    //for jump
    public float fromPosToGroundDist = 0.1f;
    public LayerMask groundLayer;

    public Transform spawnPos;

    public bool isTrace = false;
    bool isReadyToSpawn = false;
    bool isNeedChangePos = false;

    public bool IsTrace
    {
        get { return isTrace; }
        set { isTrace = value; }
    }

    private void Awake()
    {
        pathFinder = GetComponentInChildren<PathFinder>();
        sp = GetComponentInChildren<SpriteRenderer>();
        bc = GetComponentInChildren<BoxCollider2D>();
    }

    private void OnEnable()
    {
        transform.position = spawnPos.position;
        if (isReadyToSpawn)
        {
            //AppearMob();
        }
        isReadyToSpawn = true;
    }

    private void OnDisable()
    {
        if(spawnPos != null)
        {
            transform.position = spawnPos.position;
        }
    }

    private void Start()
    {
        Time.fixedDeltaTime = 0.01f;
        targetPos = pathFinder.targetPos;
        gameObject.SetActive(false);
    }

    IEnumerator TraceTarget()
    {
        var wait = new WaitForSeconds(1f);

        while (true)
        {
            Stack<Node> path = pathFinder.PathFinding(targetPos.position);
            if (path.Count > 0)
            {
                node = path.Pop();
            }

            yield return wait;
        }
    }

    void TraceToTarget()
    {
        //타겟 확인
        Stack<Node> path = pathFinder.PathFinding(targetPos.position);
        if (path.Count > 0)
        {
            node = path.Pop();
        }
    }

    private void FixedUpdate()
    {
        //move
        if (isTrace && node != null)
        {
            //노드에 도착했을 때 플레이어를 추적
            transform.position = Vector2.MoveTowards(transform.position, node.transform.position, traceSpeed * Time.fixedDeltaTime);

            //바라보는 방향
            if (node.transform.position.x > transform.position.x)
            {
                sp.flipX = false;
            }
            else
            {
                sp.flipX = true;
            }
        }

        //플레이어 리스폰 시 위치 재설정
        if(!RespawnManager.isGameOver)
        {
            if(isNeedChangePos)
            {
                Invoke("InvokeChangePos", 0.15f);
            }
        }
        else
        {
            isNeedChangePos = true;
            bc.enabled = false;
        }
    }

    void InvokeChangePos()
    {
        isNeedChangePos = false;
        bc.enabled = true;
        transform.position = PlayerInfoData.instance.playerTr.position + Vector3.right * 20f;
    }

    private void Update()
    {
        //jump
        if (isTrace)
        {
            TraceToTarget();
        }
    }

    public void AppearMob()
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.SFXPlay("주방_괴생명체 등장");
            AudioManager.instance.SFXPlay("주방_괴생명체1 음성");
            gameObject.SetActive(true);
        }
    }
    public void AppearMobNoTrace()
    {
        AudioManager.instance.SFXPlay("주방_괴생명체 등장");
        AudioManager.instance.SFXPlay("주방_괴생명체1 음성");
        gameObject.SetActive(true);
    }

    public void StopMob()
    {
        isTrace = false;
        //AudioManager.instance.StopSFX("주방_괴생명체 등장");
        AudioManager.instance.StopSFX("주방_괴생명체1 음성");
    }
}
