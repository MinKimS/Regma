using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPatrol : MonoBehaviour
{
    //turning Point
    public Transform LTurningPos;
    public Transform RTurningPos;

    public float moveSpeed = 2f;

    bool isGoRight = false;
    SpriteRenderer sp;


    MobTrace mobTrace;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        mobTrace = GetComponent<MobTrace>();

        transform.position = LTurningPos.position;
    }

    private void Update()
    {
        //Rotate at a turning Point
        if (LTurningPos.position.x >= transform.position.x)
        {
            print("turn Right");
            isGoRight = true;
            sp.flipX = !sp.flipX;
        }
        if (RTurningPos.position.x <= transform.position.x)
        {
            print("turn Left");
            isGoRight = false;
            sp.flipX = !sp.flipX;
        }

        //Move
        if (isGoRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }
}
