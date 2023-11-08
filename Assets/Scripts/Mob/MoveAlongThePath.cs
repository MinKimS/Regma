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
    Node nextNode = null;

    SpriteRenderer sp;

    //for jump
    public float fromPosToGroundDist = 0.1f;
    public LayerMask groundLayer;

    public Transform spawnPos;

    public bool isTrace = false;

    public bool IsTrace
    {
        get { return isTrace; }
        set { isTrace = value; }
    }

    private void Awake()
    {
        pathFinder = GetComponentInChildren<PathFinder>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        transform.position = spawnPos.position;
        AppearMob();
    }

    private void OnDisable()
    {
        transform.position = spawnPos.position;
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
                if(path.Count > 0)
                {
                    nextNode = path.Peek();
                }
            }

            yield return wait;
        }
    }

    void TraceToTarget()
    {
        //Ÿ�� Ȯ��
        Stack<Node> path = pathFinder.PathFinding(targetPos.position);
        if (path.Count > 0)
        {
            node = path.Pop();
            if (path.Count > 0)
            {
                nextNode = path.Peek();
            }
        }
    }

    private void FixedUpdate()
    {
        //move
        if (isTrace && node != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, node.transform.position, traceSpeed * Time.fixedDeltaTime);

            //�ٶ󺸴� ����
            if(node.transform.position.x > transform.position.x)
            {
                sp.flipX = true;
            }
            else
            {
                sp.flipX = false;
            }
        }
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
            AudioManager.instance.SFXPlay("�ֹ�_������ü ����");
            AudioManager.instance.SFXPlay("�ֹ�_������ü1 ����");
            gameObject.SetActive(true);
            isTrace = true;
        }
    }
    public void AppearMobNoTrace()
    {
        AudioManager.instance.SFXPlay("�ֹ�_������ü ����");
        AudioManager.instance.SFXPlay("�ֹ�_������ü1 ����");
        gameObject.SetActive(true);
    }

    public void StopMob()
    {
        isTrace = false;
    }
}
