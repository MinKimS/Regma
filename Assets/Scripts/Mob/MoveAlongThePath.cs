using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveAlongThePath : MonoBehaviour
{
    PathFinder pathFinder;
    Transform targetPos;
    public float traceSpeed = 3f;
    Node node = null;
    Node nextNode = null;

    Transform pos;

    //for jump
    Rigidbody2D rb;
    public float jumpForce = 10f;
    float lastJumpTime;
    public float jumpCoolTime = 2f;
    public float fromPosToGroundDist = 0.1f;
    public LayerMask groundLayer;
    //첫 점프때 바로 점프하게 하기 위해
    bool isJumped = false;
    float disToNode;

    bool isTrace = false;

    public bool IsTrace
    {
        get { return isTrace; }
        set { isTrace = value; }
    }

    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Time.fixedDeltaTime = 0.01f;
        targetPos = pathFinder.targetPos;
        pos = pathFinder.pos;
        gameObject.SetActive(false);
    }

    IEnumerator TraceTarget()
    {
        var wait = new WaitForSeconds(1f);

        while (true)
        {
            //타겟 확인
            //소리 인식범위 안에서 소리가 나면 소리가 나는 위치를 targetPos로 설정 
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
        //타겟 확인
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
        }
    }

    private void Update()
    {
        //jump
        if(isTrace)
        {
            TraceToTarget();

            disToNode = (node.transform.position - pos.position).magnitude;

            if ((nextNode.transform.position.y > node.transform.position.y) && (disToNode < 1f))
            {
                Debug.DrawRay(rb.position, Vector2.down, Color.yellow);
                bool isOnGround = Physics2D.Raycast(pos.position, Vector2.down, fromPosToGroundDist, groundLayer);
                if (!isJumped || (isOnGround && Time.time - lastJumpTime >= jumpCoolTime))
                {
                    Jump();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHide pHide = collision.GetComponentInParent<PlayerHide>();
            if(!pHide.isHide)
            {
                print("catch!");
            }
        }
    }

    void Jump()
    {
        print("jump");
        rb.AddForce(Vector2.up * jumpForce * 2);
        lastJumpTime = Time.time;
        if(!isJumped) { isJumped = true; }        
    }
}
