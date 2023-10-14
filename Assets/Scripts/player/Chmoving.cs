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
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);

        // 플레이어가 움직일 수 있는 조건
        bool canMove = DialogueManager.instance._dlgState == DialogueManager.DlgState.End &&
                       !SmartphoneManager.instance.phone.IsOpenPhone &&
                       TimelineManager.instance._Tlstate == TimelineManager.TlState.End;

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
            jumpCnt--;
        }
        else if (!isGround && Input.GetKeyDown(KeyCode.Space) && jumpCnt > 0)
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            PlayJumpSound();
            jumpCnt--;
            
        }
        if (isGround)
        {
            jumpCnt = JumpCount;
            //isJumping = true;
            
        }
        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            jumpCnt++;
            isJumping = false;
            //isJumpingWithMovement = true;
        }

        //  if (Input.GetKey(KeyCode.Space) && moveInputX != 0)
        //  {

        //  }
    }

    private void HandleRunInput()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void HandleMovementInput()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");

        if (moveInputX != 0)
        {
            isMoving = true;
            currentMoveSpeed = isRunning ? runSpeed * moveInputX : moveSpeed * moveInputX;

            if (isJumping)
            {
                StopWalkSound();
                //isJumpingWithMovement = true;
                //animator.SetBool("jump", true);
                //print("걷는중");
                rb.gravityScale = 5.0f;
            }
            else if (!isJumpingWithMovement && isGround)
            {
                PlayWalkSound();
            }

            else if(!isGround)
            {
                animator.SetBool("walk", false);
                print("걷는중");
            }
        }
        else
        {
            isMoving = false;
            currentMoveSpeed = 0f;
            StopWalkSound();
        }
        rb.velocity = new Vector2(currentMoveSpeed, rb.velocity.y);
    }

    private void UpdateAnimation()
    {
        if (isMoving)
        {
            transform.localScale = new Vector3(Mathf.Sign(currentMoveSpeed), transform.localScale.y, transform.localScale.z);
        }

        animator.SetBool("walk", isMoving);
        animator.SetBool("jump", isJumping);
        //animator.SetBool("walk", isJumpingWithMovement);
        animator.SetBool("isSit", Input.GetKey(KeyCode.LeftControl));
        animator.SetBool("isPush", animator.GetBool("isPush"));

        if (inWater && currentMoveSpeed == 0)
        {
            animator.SetBool("WetIdle", true);
        }
        else if (inWater)
        {
            animator.SetBool("Wet", true);
            animator.SetBool("WetIdle", false);
        }
        else
        {
            animator.SetBool("Wet", false);
            animator.SetBool("WetIdle", false);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowObj"))
        {
            moveSpeed = 2f;
            AudioManager.instance.SFXPlay("주방_개수대 입장");
            inWater = true;
            animator.SetBool("Wet", true);
        }
        else
        {
            animator.SetBool("Wet", false);
            animator.SetBool("walk", true);
        }
    }
}
