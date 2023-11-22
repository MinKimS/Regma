using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHanging : MonoBehaviour
{
    Rigidbody2D rb;
    Transform hangedTr;
    bool isHaging = false;
    HangedObj ho;
    public Animator animator;
    bool hangingMob = false;

    [HideInInspector] public Transform hangingPos;

    public bool IsHanging
    { get { return isHaging; } }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isHaging)
        {
            transform.position = hangingPos.position;
            StartPlayerAnimation();

            //�Ŵ޸��� �ִ��� ����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EndHanging();
            }
        }

        
    }

    public void StartPlayerAnimation()
    {
        if (animator.GetBool("jump") || animator.GetBool("walk"))
        {
            animator.SetBool("walk", false);
            animator.SetBool("jump", false);
            animator.SetBool("IsHanging", true);
        }


        //animator.SetBool("IsHanging", true);
    }

    public void StartHanging(Transform tr)
    {
        isHaging = true;

        //�÷��̾� �������ų� �������� �ʰ� ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;

        //if (animator.GetBool("jump"))
        //{
        //    animator.SetBool("jump", false);
        //    animator.SetBool("IsHanging", true);
        //}
            
        
        hangedTr = tr;
        ho = hangedTr.GetComponent<HangedObj>();
        transform.position = hangingPos.position;
    }



        public void EndHanging()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 5f;
            rb.velocity = Vector2.zero;

            transform.rotation = Quaternion.identity;
            animator.SetBool("IsHanging", false);
            //animator.SetBool("Idle", true);
            ho.EndSwing();

            isHaging = false;
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Mob"))
            {

                hangingMob = true;
                print("�Ŵ޸��� �ִµ� ���� ����ħ");

            if (hangingMob && isHaging)
            {
                EndHanging();
            }

        }
        }

}