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

    Transform pos;

    //for jump
    Rigidbody2D rb;
    public float jumpForce = 10f;
    float lastJumpTime;
    public float jumpCoolTime = 2f;
    public float fromPosToGroundDist = 0.1f;
    public LayerMask groundLayer;

    //sound
    public float soundRecogArea = 2f;

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        targetPos = pathFinder.targetPos;
        pos = pathFinder.pos;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(TraceTarget());
    }

    IEnumerator TraceTarget()
    {
        var wait = new WaitForSeconds(1f);

        while (true)
        {
            //Ÿ�� Ȯ��
            //�Ҹ� �νĹ��� �ȿ��� �Ҹ��� ���� �Ҹ��� ���� ��ġ�� targetPos�� ���� 

            Stack<Node> path = pathFinder.PathFinding(targetPos.position);
            if (path.Count > 0)
            {
                node = path.Pop();
            }

            yield return wait;
        }
    }

    private void Update()
    {
        if (node != null)
        {
            //y������ �̵� ���ϰ� �ٲ� ����
            transform.position = Vector3.MoveTowards(transform.position, node.transform.position, traceSpeed * Time.deltaTime);

            if ((node.transform.position.y > pos.position.y) && (targetPos.position.y > pos.position.y))
            {
                Debug.DrawRay(rb.position, Vector2.down, Color.yellow);
                bool isOnGround = Physics2D.Raycast(pos.position, Vector2.down, fromPosToGroundDist, groundLayer);

                if(isOnGround && Time.time - lastJumpTime >= jumpCoolTime) { Jump(); }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.CompareTag("Player");
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        lastJumpTime = Time.time;
    }
}
