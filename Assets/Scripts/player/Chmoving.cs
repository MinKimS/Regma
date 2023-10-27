using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chmoving : MonoBehaviour
{
    public Animator animator;
    public AudioClip jumpSound;
    public AudioSource walkAudioSource;

    private float moveSpeed = 5f;
    private float runSpeed = 10f;
    public float jumpForce = 12f;
    private float PushSpeed = 20f;
    private float currentMoveSpeed = 0f;

    private bool isRunning = false;
    private Rigidbody2D rb;
    private bool isMoving = false;
    private bool isJumpingWithMovement = false;
    private bool inWater = false;
    private bool MovinginWater = false;

    private float moveInputX;


    int jumpCnt;

    [HideInInspector]
    public bool isGround;
    [SerializeField] Transform pos;
    float checkRadius = 0.35f;
    [SerializeField] LayerMask islayer;
    bool isJumping = false;
    public int JumpCount;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpCnt = JumpCount;
        rb.gravityScale = 5.0f;
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);

        // 플레이어가 움직일 수 있는 조건
        bool canMove = DialogueManager.instance._dlgState == DialogueManager.DlgState.End &&
                       !SmartphoneManager.instance.phone.IsOpenPhone &&
                       TimelineManager.instance._Tlstate == TimelineManager.TlState.End &&
                       !TutorialController.instance.IsTutorialShowing;

        if (canMove)
        {
            // 점프 관련 입력 처리
            HandleJumpInput();

            // 달리기 입력 처리
            HandleRunInput();

            // 이동 관련 입력 처리
            HandleMovementInput();

            // 애니메이션 업데이트
            UpdateAnimation();
        }
        else
        {
            // 플레이어가 움직일 수 없을 때 정지
            rb.velocity = new Vector2(0f, rb.velocity.y);
            StopWalkSound();
        }
    }

    private void HandleJumpInput()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space) && jumpCnt > 0)
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            PlayJumpSound();
            //jumpCnt--;
        }
        else if (!isGround && Input.GetKeyDown(KeyCode.Space) && jumpCnt > 0)
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            PlayJumpSound();
            //jumpCnt--;

        }
        if (isGround)
        {
            jumpCnt = JumpCount;
            //isJumping = true;

        }
        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            jumpCnt--;
            isJumping = false;
            //isJumpingWithMovement = true;
        }


    }

    private void HandleRunInput()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void HandleMovementInput()
{
    moveInputX = Input.GetAxisRaw("Horizontal");

    if (moveInputX != 0)
    {
        isMoving = true;
        currentMoveSpeed = isRunning ? runSpeed * moveInputX : moveSpeed * moveInputX;

        if (isJumping)
        {
            StopWalkSound();
            rb.gravityScale = 5.0f;
        }
        else if (!isJumpingWithMovement && isGround)
        {
            PlayWalkSound();
        }
        else if (!isGround)
        {
            animator.SetBool("walk", false);
            rb.gravityScale = 5.0f;
        }

        if (MovinginWater)
        {
            isMoving = false;
            // SlowObj와 충돌하면 움직임을 제어
            transform.localScale = new Vector3(Mathf.Sign(currentMoveSpeed), transform.localScale.y, transform.localScale.z);
            currentMoveSpeed = moveSpeed * moveInputX;
            animator.SetBool("Walk", false);
            animator.SetBool("WetIdle", false);
            // "Wet" 애니메이션을 활성화합니다.
            animator.SetBool("Wet", true);
        }
        
    }




    else
    {

        if (inWater)
        {
            // SlowObj와 충돌하고 움직이지 않을 때 WetIdle 애니메이션을 활성화합니다.
            if (!isMoving)
            {
                animator.SetBool("WetIdle", true);
                animator.SetBool("walk", false);
            }
        }
            else
            {
                // 방향키 입력이 없을 때 "Wet" 애니메이션을 비활성화합니다.
                animator.SetBool("Wet", false);
                animator.SetBool("WetIdle", false);
            }


            // 방향키 입력이 없을 때 "Wet" 애니메이션을 비활성화합니다.
            animator.SetBool("Wet", false);
        isMoving = false;
        currentMoveSpeed = 0f;
        StopWalkSound();

        // if(!isMoving && inWater)
        // {
        //     print("테스트");
        // animator.SetBool("Idle", false);
        // animator.SetBool("WetIdle", true);
        // animator.SetBool("Wet", false);
        // }
    }

    rb.velocity = new Vector2(currentMoveSpeed, rb.velocity.y);
}

    

    private void UpdateAnimation()
    {
        if (isMoving)
        {
            transform.localScale = new Vector3(Mathf.Sign(currentMoveSpeed), transform.localScale.y, transform.localScale.z);
        }

        if (inWater)
            //animator.SetBool("Wet", true);
            //print(inWater);
            print(MovinginWater);

        else
            animator.SetBool("walk", isMoving);

        //animator.SetBool("wet", inWater);
        animator.SetBool("jump", isJumping);

        // 발 젖는 애니메이션
        //animator.SetBool("Wet", MovinginWater);


        animator.SetBool("isSit", Input.GetKey(KeyCode.LeftControl));
        animator.SetBool("isPush", animator.GetBool("isPush"));
        //animator.SetBool("isPush", animator.GetBool("isPush"));
    }

    private void PlayJumpSound()
    {
        if (jumpSound != null && !isJumpingWithMovement)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }
    }

    private void PlayWalkSound()
    {
        if (walkAudioSource != null && walkAudioSource.clip != null && !walkAudioSource.isPlaying && !isJumpingWithMovement)
        {
            walkAudioSource.Play();
        }
    }

    private void StopWalkSound()
    {
        if (walkAudioSource != null && walkAudioSource.clip != null && walkAudioSource.isPlaying && !isJumpingWithMovement)
        {
            walkAudioSource.Stop();
        }
    }

    //발 젖는 애니메이션

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowObj"))
        {
            moveSpeed = 2f;
            AudioManager.instance.SFXPlay("주방_개수대 입장");
            inWater = true;
            //MovinginWater = true;

            // if (moveInputX != 0 && isMoving)
            // {
            //     MovinginWater = true;
            //     //animator.SetBool("Wet", true);
            //     //animator.SetBool("walk", false);
            //     //print("물 속에 있음");
            // }
            // else if(moveInputX == 0)
            // {
            //     //MovinginWater = false;
            //     print("else if문 false");
            // }

            // if (MovinginWater)
            // {
            // // SlowObj와 충돌하면 움직임을 제어
            // transform.localScale = new Vector3(Mathf.Sign(currentMoveSpeed), transform.localScale.y, transform.localScale.z);
            // currentMoveSpeed = moveSpeed * moveInputX;
            // animator.SetBool("Wet", isMoving);
            // //isMoving = false;

            // }
            // else
            // {
            // // 그 외의 경우는 정상적인 이동 속도 적용
            // currentMoveSpeed = isRunning ? runSpeed * moveInputX : moveSpeed * moveInputX;
            // }
        }
        //else
        //{
        //    inWater = false;
        //    MovinginWater = false;
        //    animator.SetBool("Wet", false);
        //    animator.SetBool("walk", true);
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowObj"))
        {

            if (moveInputX != 0 && isMoving)
            {
                MovinginWater = true;
                //animator.SetBool("Wet", true);
                animator.SetBool("walk", false);
                //print("물 속에 있음");
            }


            inWater = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowObj"))
        {
            moveSpeed = 5f;
            inWater = false;
            MovinginWater = false;
            animator.SetBool("Wet", false);
            animator.SetBool("walk", true);
        }
    }
}