using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisMonster : MonoBehaviour
{
    public Animator animator;

    [SerializeField] Transform monsterTransform; // ������ Transform�� �����ؾ� �մϴ�.
    [SerializeField] float proximityDistance = 20.0f; // �Ÿ� ���ذ�


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal"); // �¿� ����Ű �Է��� �޽��ϴ�.


        float distanceToMonster = Vector2.Distance(transform.position, monsterTransform.position);

        if (distanceToMonster < proximityDistance)
        {
            animator.SetBool("Monster", true); // "Monster" �ִϸ��̼��� Ȱ��ȭ�մϴ�.

            if (moveInputX != 0)
            {

                animator.SetBool("MonChWalk", true);
                animator.SetBool("Monster", false);
            }

            //else if(moveInputX < 0)
            //{
            //    animator.SetBool("MonChWalk", true);
            //    animator.SetBool("Monster", false);
            //}
        }
        else
        {
            animator.SetBool("Monster", false); // "Monster" �ִϸ��̼��� ��Ȱ��ȭ�մϴ�.
            animator.SetBool("MonChWalk", false);
        }
    }
}

