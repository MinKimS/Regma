using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermoving : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float runSpeed = 7f;
    public float jumpForce = 20f;

    private bool isRunning = false;
    private bool isJumping = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // �߷� Ȱ��ȭ
    }

    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        // ���� �̵�
        rb.velocity = new Vector2(moveInputX * GetSpeed(), rb.velocity.y);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // ����
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // ���� �ӵ� �ʱ�ȭ
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
