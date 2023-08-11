using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisMonster : MonoBehaviour
{
    public Animator animator;

    [SerializeField] Transform monsterTransform; // ������ Transform�� �����ؾ� �մϴ�.
    [SerializeField] float proximityDistance = 3.0f; // �Ÿ� ���ذ�


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMonster = Vector2.Distance(transform.position, monsterTransform.position);

        if (distanceToMonster < proximityDistance)
        {
            animator.SetBool("Monster", true); // "Monster" �ִϸ��̼��� Ȱ��ȭ�մϴ�.
        }
        else
        {
            animator.SetBool("Monster", false); // "Monster" �ִϸ��̼��� ��Ȱ��ȭ�մϴ�.
        }
    }
}
