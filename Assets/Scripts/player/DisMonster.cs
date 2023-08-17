using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisMonster : MonoBehaviour
{
    public Animator animator;

    [SerializeField] Transform monsterTransform; // 몬스터의 Transform을 연결해야 합니다.
    [SerializeField] float proximityDistance = 20.0f; // 거리 기준값


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal"); // 좌우 방향키 입력을 받습니다.


        float distanceToMonster = Vector2.Distance(transform.position, monsterTransform.position);

        if (distanceToMonster < proximityDistance)
        {
            animator.SetBool("Monster", true); // "Monster" 애니메이션을 활성화합니다.

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
            animator.SetBool("Monster", false); // "Monster" 애니메이션을 비활성화합니다.
            animator.SetBool("MonChWalk", false);
        }
    }
}

