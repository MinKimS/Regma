using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chmoving : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float runSpeed = 5f;
    private float jumpForce = 5f; // 점프 힘을 두 배로 증가시킴

    private bool isRunning = false;
    //private bool isJumping = false;
    private Rigidbody2D rb;

    bool isGround;
    [SerializeField] Transform pos;
    float checkRadius = 0.35f;
    [SerializeField] LayerMask islayer;
    int JumpCnt;
    int JumpCount = 5;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpCnt = JumpCount;
    }

    private void FixedUpdate()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        //float moveInputY = Input.GetAxis("Vertical");

        // 좌우 이동
        rb.velocity = new Vector2(moveInputX * GetSpeed(), rb.velocity.y);

    }


    private void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);


        if (isGround == true && Input.GetKeyDown(KeyCode.Space) && JumpCnt > 0) // 이중 점프 허용
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isGround == false && Input.GetKeyDown(KeyCode.Space) && JumpCnt > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpCnt--;
        }

        if (isGround)
        {
            JumpCnt = JumpCount;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private float GetSpeed()
    {
        if (isRunning)
        {
            return moveSpeed * runSpeed;
        }
        else
        {
            return moveSpeed;
        }
    }


}