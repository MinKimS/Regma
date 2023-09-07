using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangedObj : MonoBehaviour
{
    public Transform swingAnchor;
    public float swingForce = 10f;
    public float maxSwingAngle = 30f;
    public float gravity = 9.8f;
    //public Animator animator;

    HingeJoint2D joint;
    Rigidbody2D rb;
    bool isSwing = false;
    bool isStopping = false;
    bool isOkHanging = true;

    SpriteRenderer sp;

    Transform playerHangPos;
    float pDir = 1f;

    public bool IsSwing
    {
        set { isSwing = value; }
    }

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<HingeJoint2D>();
        playerHangPos = GetComponentsInChildren<Transform>()[1];
        //animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            PlayerHanging pHanging = collision.GetComponent<PlayerHanging>();
            

            if (!isSwing && isOkHanging)
            {
                rb.angularDrag = 1f;
                pHanging.hangingPos = playerHangPos;
                isSwing = true;
                isOkHanging = false;
                joint.useMotor = false;
                pHanging.StartHanging(transform);
                pHanging.StartPlayerAnimation();

            }

        }
    }

    private void Update()
    {
        if (isSwing)
        {
            pDir = Input.GetAxis("Horizontal");
            rb.AddTorque(pDir * swingForce);
        }

        if (isStopping)
        {
            rb.AddForce(Vector2.down * gravity);
        }
    }

    IEnumerator ResetRotation()
    {
        yield return new WaitForSeconds(5f);
        isStopping = false;
        isOkHanging = true;
    }

    public void EndSwing()
    {
        rb.angularDrag = 50f;
        isSwing = false;
        rb.velocity = Vector2.zero;
        isStopping = true;
        StartCoroutine(ResetRotation());
    }
}