using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingController : MonoBehaviour
{
    [HideInInspector]
    public bool isNoFallingDamage = false;
    bool isJump = false;

    Animator anim;
    Rigidbody2D rb;

    public Chmovingbath movingBath;
    public Chmoving moving;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //애니메이션 전환 넣기
        //낙뎀 막는거 넣기
        //천천히 떨어지기
        if(isNoFallingDamage && Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = 0.2f;
            rb.velocity = Vector2.zero;
            isJump = true;
            print(anim);
        }

        if(movingBath != null)
        {
            if (movingBath.isGround && isJump)
            {
                rb.gravityScale = 1f;
                isJump = false;
                isNoFallingDamage = false;
            }
        }
        if(moving != null)
        {
            if (moving.isGround && isJump)
            {
                rb.gravityScale = 1f;
                isJump = false;
                isNoFallingDamage = false;
            }
        }
        
    }
}
