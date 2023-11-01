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
    Rigidbody2D rb;
    public float jumpForce = 10f;
    float lastJumpTime;
    public float jumpCoolTime = 2f;
    public float fromPosToGroundDist = 0.1f;
    public LayerMask groundLayer;
    //ù ������ �ٷ� �����ϰ� �ϱ� ����
    //bool isJumped = false;
    //public float disToNode;

    public bool isTrace = false;

    public bool IsTrace
    {
        get { return isTrace; }
        set { isTrace = value; }
    }

    private void Awake()
    {
        pathFinder = GetComponentInChildren<PathFinder>();
        rb = GetComponentInChildren<Rigidbody2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
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
            //Debug.DrawRay(pos.position, Vector2.down, Color.yellow);
            //bool isOnGround = Physics2D.Raycast(pos.position, Vector2.down, fromPosToGroundDist, groundLayer);
            TraceToTarget();

            
            //disToNode = (node.transform.position - pos.position).magnitude;

            //���� ��� �ϴ� ����
            //if (node.transform.position.y > pos.transform.position.y)
            //{
            //    if (Time.time - lastJumpTime >= jumpCoolTime)
            //    {
            //        Jump();
            //    }
            //}
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce * 2);
        lastJumpTime = Time.time;      
    }

    public void AppearMob()
    {
        gameObject.SetActive(true);
    }

    public void StopMob()
    {
        isTrace = false;
    }
}
