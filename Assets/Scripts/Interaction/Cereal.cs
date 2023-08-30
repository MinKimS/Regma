using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cereal : MonoBehaviour
{
    Animator pAnim;
    Vector3 originPos;
    Transform anchor;

    public float fallPoint = 3f;
    bool isFall = false;

    private void Start()
    {
        anchor = GetComponentInParent<Transform>();
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
        if(collision.gameObject.CompareTag("Player") && isFall)
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
                isFall = true;
            }
        }
    }

}
