using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanging : MonoBehaviour
{
    public Transform swingAnchor;
    public float swingForce = 10f;
    public float maxSwingAngle = 30f;
    public float jumpMoveForce = 20f;
    public float gravity = 9.8f;

    private HingeJoint2D hinge;
    private Rigidbody2D rb;
    private bool isHaging = false;

    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!isHaging)
            {
                isHaging = true;
            }
        }
    }

    private void Update()
    {
        if(isHaging)
        {
            float h = Input.GetAxis("Horizontal");
            rb.AddTorque(h * swingForce);

            //매달리고 있던걸 놓음
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isHaging = false;
                rb.velocity = Vector2.zero;
                //rb.AddForce((Vector2.right + Vector2.up) * jumpMoveForce, ForceMode2D.Impulse);
            }
        }

        if(!isHaging)
        {
            rb.AddForce(Vector2.down * gravity);
            Invoke("ResetRotation", 5f);
        }
    }

    void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }
}
