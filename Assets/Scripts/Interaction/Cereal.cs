using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cereal : MonoBehaviour
{
    Animator pAnim;
    Vector3 originPos;
    Transform anchor;
    Rigidbody2D rb;


    public float fallPoint = 3f;
    bool isFall = false;

   

    private void Awake()
    {
        anchor = GetComponentInParent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        originPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFall)
        {
            pAnim = collision.gameObject.GetComponent<Animator>();
            pAnim.SetBool("isPush", true);


        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            pAnim.SetBool("isPush", false);
        }
    }

    private void Update()
    {
        if(!isFall)
        {
            float dis = Vector3.Distance(transform.position, originPos);
            if (dis > fallPoint)
            {
                anchor.rotation = Quaternion.Euler(0, 0, -90);
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector3.zero;
                isFall = true;
            }
        }
    }

}
