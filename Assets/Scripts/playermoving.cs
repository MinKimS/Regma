using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermoving : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float runSpeed = 7f;

    private bool isRunning = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // 중력 비활성화
    }

    private void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveInputX * GetSpeed(), moveInputY * GetSpeed());

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
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
}
