using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chmoving : MonoBehaviour
{
    public Animator animator;

    private float moveSpeed = 5f;
    private float runSpeed = 20f;
    private float jumpForce = 7f;
    private float currentMoveSpeed = 0f;

    private bool isRunning = false;
    private Rigidbody2D rb;
    private bool isMoving = false;

    bool isGround;
    [SerializeField] Transform pos;
    float checkRadius = 0.35f;
    [SerializeField] LayerMask islayer;
    bool isJumping = false;
    int JumpCount = 5;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);
      
        if(DialogueManager.instance._dlgState == DialogueManager.DlgState.End && !SmartphoneManager.instance.IsOpenPhone&&TimelineManager.instance._Tlstate == TimelineManager.TlState.End)
        {
            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
            }

            if (!isGround && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
            }

            if (isGround)
            {
                JumpCount = 0;
            }

            if (Input.GetKeyUp(KeyCode.Space) && isJumping)
            {
                JumpCount++;
                isJumping = false;
            }

            if (JumpCount > 0)
            {
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("jump", false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            float moveInputX = Input.GetAxisRaw("Horizontal");

            if (moveInputX != 0)
            {
                isMoving = true;
                currentMoveSpeed = moveSpeed * moveInputX;
                animator.SetBool("walk", true);
            }
            else
            {
                isMoving = false;
                currentMoveSpeed = 0f;
                animator.SetBool("walk", false);
            }

            rb.velocity = new Vector2(currentMoveSpeed, rb.velocity.y);
        }

       

            float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0)
        {
            // 위 방향키를 누르면 "Tv on / off" 애니메이션을 재생합니다.
            animator.SetBool("Tv", true);
        }
        else if (verticalInput < 0)
        {
            
            animator.SetBool("Tv", true);
        }
        else
        {
            // 방향키를 누르지 않으면 모든 애니메이션을 정지합니다.
            animator.SetBool("Tv", false);
        }
    }

    private void LateUpdate()
    {
        if (isMoving)
        {
            transform.localScale = new Vector3(Mathf.Sign(currentMoveSpeed), transform.localScale.y, transform.localScale.z);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Book"))
        {
            Bookcontrol bookControl = collision.GetComponent<Bookcontrol>();
            if (bookControl != null)
            {
                bookControl.ShowImage();
            }
        }
    }
}
