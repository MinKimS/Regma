using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHanging : MonoBehaviour
{
    Rigidbody2D rb;
    Transform hangedTr;
    bool isHaging = false;
    HangedObj ho;

    [HideInInspector] public Transform hangingPos;

    public bool IsHanging
    { get { return isHaging; } }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(isHaging)
        {
            transform.position = hangingPos.position;

            //�Ŵ޸��� �ִ��� ����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EndHanging();
            }
        }
    }
    public void StartHanging(Transform tr)
    {
        isHaging = true;

        //�÷��̾� �������ų� �������� �ʰ� ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;

        hangedTr = tr;
        ho = hangedTr.GetComponent<HangedObj>();
        transform.position = hangingPos.position;
    }

    public void EndHanging()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        rb.velocity = Vector2.zero;

        transform.rotation = Quaternion.identity;

        ho.EndSwing();

        isHaging = false;
    }
}
