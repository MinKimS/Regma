using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChmovingKitchen : MonoBehaviour
{
    public Animator animator;
   // public Animator RefriAnimation;

    public AudioClip jumpSound; // 점프 사운드
    public AudioSource walkAudioSource; // 걷는 소리 소스

    private float moveSpeed = 5f;
    private float runSpeed = 20f;
    private float jumpForce = 7f;
    private float PushSpeed = 20f;
    private float currentMoveSpeed = 0f;

    private bool isRunning = false;
    private Rigidbody2D rb;
    private bool isMoving = false;
    private bool isJumpingWithMovement = false;

    private bool isCollisionActive = false;
    //private bool hasOpened = false;

    bool isGround;
    [SerializeField] Transform pos;
    float checkRadius = 0.35f;
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



        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);

        if (DialogueManager.instance._dlgState == DialogueManager.DlgState.End && !SmartphoneManager.instance.phone.IsOpenPhone && TimelineManager.instance._Tlstate == TimelineManager.TlState.End) //&& !pHanging.IsHanging && isOkPlayerMove)
        {
            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
                PlayJumpSound(); // 점프 사운드 재생
            }

            if (!isGround && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
                PlayJumpSound(); // 점프 사운드 재생
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
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            StopWalkSound();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("isSit", true); // 컨트롤 키 입력 시 isSit 애니메이션 트리거 활성화
        }
        else
        {
            animator.SetBool("isSit", false);
        }

        if (animator.GetBool("isPush"))
        {
            //currentMoveSpeed = PushSpeed * Input.GetAxisRaw("Horizontal"); // isPush 애니메이션이 작동 중일 때 PushSpeed를 사용
            StopWalkSound();
        }
        //else
        //{
        //    currentMoveSpeed = moveSpeed * Input.GetAxisRaw("Horizontal"); // 일반적인 움직임 속도
        //}

        print("일반 속도" + currentMoveSpeed);

        if (isCollisionActive && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("RefriSwitchOn", true);
            //hasOpened = true; // 캔버스를 열었음을 표시
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

    //물에서 속도 감소

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowObj"))
        {
            moveSpeed = 2f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowObj"))
        {
            moveSpeed = 5f;
        }
    }

   



    



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {

            isCollisionActive = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {

            //hasOpened = false;

        }
    }

    

}
