using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingController : MonoBehaviour
{
    [HideInInspector]
    public bool isNoFallingDamage = false;
    bool isJump = false;
    bool isCheckInTheAir = false;

    [HideInInspector] public Animator anim;
    Rigidbody2D rb;

    //public Chmovingbath movingBath;
    public Chmoving moving;

    public Damaging damaging;

    float jumpedPosY = 0;
    public float damageHeight = 8;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(!isCheckInTheAir && !moving.isGround)
        {
            isCheckInTheAir = true;
            jumpedPosY = transform.position.y;
        }
        
        if(moving.isGround && isCheckInTheAir)
        {
            isCheckInTheAir = false;
            
            //낙뎀 부하 여부
            if(!isNoFallingDamage && Mathf.Abs(jumpedPosY-transform.position.y) > damageHeight)
            {
                Debug.LogWarning("DAMAGE!!!!");
                damaging.Damage();
            }
            isNoFallingDamage = false;
        }


        //애니메이션 전환 넣기
        //낙뎀 막는거 넣기
        //천천히 떨어지기
        if (isNoFallingDamage && Input.GetKeyUp(KeyCode.Space))
        { 
            rb.gravityScale = 0.2f;
            rb.velocity = Vector2.zero;
            isJump = true;
        }
        if (moving != null)
        {
            if (moving.isGround && isJump)
            {
                rb.gravityScale = 5f;
                isJump = false;
                anim.SetBool("isJumpWithBlanket", false);
            }
        }

    }
}
