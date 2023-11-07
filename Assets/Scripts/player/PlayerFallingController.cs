using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingController : MonoBehaviour
{
    [HideInInspector]
    public bool isNoFallingDamage = false;
    bool isJump = false;
    bool isCheckInTheAir = false;

    Animator anim;
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
            
            //���� ���� ����
            if(Mathf.Abs(jumpedPosY-transform.position.y) > damageHeight)
            {
                Debug.LogWarning("DAMAGE!!!!");
                damaging.Damage();
            }
        }

        //�ִϸ��̼� ��ȯ �ֱ�
        //���� ���°� �ֱ�
        //õõ�� ��������
        if(isNoFallingDamage && Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = 0.2f;
            rb.velocity = Vector2.zero;
            isJump = true;
            print(anim);
        }

        //if(movingBath != null)
        //{
        //    if (movingBath.isGround && isJump)
        //    {
        //        rb.gravityScale = 1f;
        //        isJump = false;
        //        isNoFallingDamage = false;
        //    }
        //}

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
