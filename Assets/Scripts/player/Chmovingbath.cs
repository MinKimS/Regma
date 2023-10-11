using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chmovingbath : MonoBehaviour
{
    public Animator animator;

    public AudioClip jumpSound; // 점프 사운드
    public AudioSource walkAudioSource; // 걷는 소리 소스

    private float moveSpeed = 5f;
    private float runSpeed = 20f;
    private float jumpForce = 7f;
    private float currentMoveSpeed = 0f;

    private bool isRunning = false;
    private Rigidbody2D rb;
    private bool isMoving = false;
    private bool isJumpingWithMovement = false;
    [HideInInspector]
    public bool isGround;
    [SerializeField] Transform pos;
    float checkRadius = 0.10f;
    [SerializeField] LayerMask islayer;
    bool isJumping = false;
    int JumpCount = 5;

    // PlayerHanging pHanging;

    //bool isOkPlayerMove = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //pHanging = GetComponent<PlayerHanging>();
    }

    private void Update()
    {
        
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);//

            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce; // 캐릭터 현재 속도 , 위쪽으로
            //animator.SetBool("jump", true);
                print("점프중");
                PlayJumpSound(); // 점프 사운드 재생
            }


        if (!isGround && Input.GetKeyDown(KeyCode.Space)) // 계속 점프를 방지
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
            //animator.SetBool("jump", true);
                print("공중점프");
                PlayJumpSound(); // 점프 사운드 재생
            }

            if (isGround)
            {
                JumpCount = 0;//
            }

            if (Input.GetKeyUp(KeyCode.Space) && isJumping)
            {
                JumpCount++;
                //print("JumpCount :" + JumpCount);
                //isJumping = false;
            }

        //if()

            if (JumpCount > 0)
            {
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("jump", false);
            
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.SetBool("isSit", true); // 컨트롤 키 입력 시 isSit 애니메이션 트리거 활성화
            }
            else
            {
                animator.SetBool("isSit", false);
            }

        //print("현재 점프 : " + JumpCount);
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    isRunning = true;
        //}
        //else
        //{
        //    isRunning = false;
        //}

        float moveInputX = Input.GetAxisRaw("Horizontal");

            if (moveInputX != 0)
            {
                isMoving = true;
                currentMoveSpeed = moveSpeed * moveInputX;
                animator.SetBool("walk", true);
                if (isJumping)
                {
                    StopWalkSound();
                    isJumpingWithMovement = true;
                }
                else
                {
                    if (!isJumpingWithMovement && isGround) // 추가된 조건
                    {
                        PlayWalkSound();
                    }


                    isJumpingWithMovement = false;
                }
            }

            else
            {
                isMoving = false;
                currentMoveSpeed = 0f;
                animator.SetBool("walk", false);
                StopWalkSound();
            }

            rb.velocity = new Vector2(currentMoveSpeed, rb.velocity.y);
       

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

    //void OnTriggerEnter2D(Collider2D collision) 8.16
    //{
    //    if (collision.CompareTag("Book"))
    //    {
    //        Bookcontrol bookControl = collision.GetComponent<Bookcontrol>();
    //        if (bookControl != null)
    //        {
    //            bookControl.ShowImage();
    //        }
    //    }
    //}

    void PlayJumpSound()
    {
        if (jumpSound != null && !isJumpingWithMovement)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position); // 점프 사운드 재생
        }
    }

    void PlayWalkSound()
    {
        if (walkAudioSource != null && walkAudioSource.clip != null && !walkAudioSource.isPlaying && !isJumpingWithMovement)
        {
            walkAudioSource.Play();
        }
    }

    void StopWalkSound()
    {
        if (walkAudioSource != null && walkAudioSource.clip != null && walkAudioSource.isPlaying && !isJumpingWithMovement)
        {
            walkAudioSource.Stop();
        }
    }
    //public void SetIsOkPlayerMove(bool value)
    //{
    //    isOkPlayerMove = value;
    //}
}
