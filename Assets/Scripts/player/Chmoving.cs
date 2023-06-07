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
    int JumpCnt;
    int JumpCount = 5;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        JumpCnt = JumpCount;
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);
        //��ȭ��, �ڵ��� Ų ���¿����� �������� �ʰ� ����
        if(DialogueManager.instance._dlgState == DialogueManager.DlgState.End && !SmartphoneManager.instance.IsOpenPhone&&TimelineManager.instance._Tlstate == TimelineManager.TlState.Stop)
        {
            if (isGround && Input.GetKeyDown(KeyCode.Space) && JumpCnt > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            if (!isGround && Input.GetKeyDown(KeyCode.Space) && JumpCnt > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpCnt--;
                animator.SetBool("jump", true);
            }

            if (isGround)
            {
                JumpCnt = JumpCount;
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

            // ?��? ???
            rb.velocity = new Vector2(currentMoveSpeed, rb.velocity.y);
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
        if (collision.tag.Equals("Book"))
        {
            Bookcontrol bookControl = collision.GetComponent<Bookcontrol>();
            if (bookControl != null)
            {
                bookControl.ShowImage();
            }
        }

        //if (collision.gameObject.CompareTag("shake") && gameObject.CompareTag("player"))
        //{
        //    // ī�޶� ��Ʈ�ѷ��� StartCameraShake �޼��� ȣ��
        //    cameraController.StartCameraShake();
        //}

    }


 
}
