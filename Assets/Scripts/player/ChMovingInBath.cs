using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChMovingInBath : MonoBehaviour
{
    public Animator animator;

    public AudioClip jumpSound; // 점프 사운드
    public AudioSource walkAudioSource; // 걷는 소리 소스

    private float moveSpeed = 5f;
    private float runSpeed = 10f;
    public float jumpForce = 9f;
    private float PushSpeed = 20f;
    private float currentMoveSpeed = 0f;

    private bool isRunning = false;
    private Rigidbody2D rb;
    private bool isMoving = false;
    private bool isJumpingWithMovement = false;
    private bool inWater = false;
    public int JumpCount;


    int jumpCnt; // 0이 되면 더 이상 점프 x


    [HideInInspector]
    public bool isGround;
    [SerializeField] Transform pos;
    float checkRadius = 0.10f;
    [SerializeField] LayerMask islayer;
    bool isJumping = false;

    private void Start()
    {
        animator = GetComponent<Animator>(); // 'Animator' 컴포넌트 초기화
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 5.0f;
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);

        bool canMove = DialogueManager.instance._dlgState == DialogueManager.DlgState.End &&
                       !SmartphoneManager.instance.phone.IsOpenPhone &&
                       TimelineManager.instance._Tlstate == TimelineManager.TlState.End &&
                       !TutorialController.instance.IsTutorialShowing;
        if(canMove)
        {
            if (isGround && Input.GetKeyDown(KeyCode.Space) && jumpCnt > 0)
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
                PlayJumpSound();
                //jumpCnt--;
            }

            if (!isGround && Input.GetKeyDown(KeyCode.Space) && jumpCnt > 0)
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
                PlayJumpSound();
                //jumpCnt--;
            }

            if (isGround)
            {
                jumpCnt = JumpCount;

            }

            if (Input.GetKeyUp(KeyCode.Space) && isJumping)
            {
                jumpCnt--;
                isJumping = false;
            }

            if (isJumping)
            {
                animator.SetBool("jump", true);
            }

            else
            {
                animator.SetBool("jump", false);
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.SetBool("isSit", true);
            }
            else
            {
                animator.SetBool("isSit", false);
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
                    rb.gravityScale = 3.0f;

                    if (isJumpingWithMovement)
                    {
                        print("dd");
                        animator.SetBool("walk", false);
                    }
                }
                else
                {
                    if (!isJumpingWithMovement && isGround)
                    {
                        PlayWalkSound();
                    }

                    isJumpingWithMovement = false;
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    isRunning = true;
                    currentMoveSpeed = runSpeed * moveInputX;
                    animator.SetBool("walk", true);
                    print("달리는 중");
                }
                else
                {
                    isRunning = false;
                    currentMoveSpeed = moveSpeed * moveInputX;
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
    }

    private void LateUpdate()
    {
        if (isMoving)
        {
            transform.localScale = new Vector3(Mathf.Sign(currentMoveSpeed), transform.localScale.y, transform.localScale.z);
        }
    }

    void PlayJumpSound()
    {
        if (jumpSound != null && !isJumpingWithMovement)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
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
}
