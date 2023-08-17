using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangedObj : MonoBehaviour
{
    public Transform swingAnchor;
    public float swingForce = 10f;
    public float maxSwingAngle = 30f;
    public float gravity = 9.8f;

    HingeJoint2D joint;
    Rigidbody2D rb;
    bool isSwing = false;
    bool isStopping = false;
    bool isOkHanging = true;

    SpriteRenderer sp;

    public bool IsSwing
    {
        set { isSwing = value; }
    }

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<HingeJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHanging pHanging = collision.GetComponent<PlayerHanging>();
            if (!isSwing && isOkHanging)
            {
                isSwing = true;
                isOkHanging = false;
                joint.useMotor = false;
                pHanging.StartHanging(transform);
            }
        }
    }

    private void Update()
    {
        if (isSwing)
        {
            float h = Input.GetAxis("Horizontal");
            rb.AddTorque(h * swingForce);
        }

        if (isStopping)
        {
            rb.AddForce(Vector2.down * gravity);
        }
    }

    IEnumerator ResetRotation()
    {
        yield return new WaitForSeconds(5f);
        transform.rotation = Quaternion.identity;
        joint.useMotor = true;
        isStopping = false;
        isOkHanging = true;
    }

    public float GetLightSizeY()
    {
        return sp.bounds.size.y;
    }

    public void EndSwing()
    {
        isSwing = false;
        rb.velocity = Vector2.zero;
        isStopping = true;
        StartCoroutine(ResetRotation());
    }
}
