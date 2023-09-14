using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisMonster : MonoBehaviour
{
    public Animator animator;

    [SerializeField] GameObject[] monsterObjects; // ���� ���� ������Ʈ �迭

    [SerializeField] float proximityDistance = 5.0f; // �Ÿ� ���ذ�

    // Update is called once per frame
    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal"); // �¿� ����Ű �Է��� �޽��ϴ�.

        float closestDistance = proximityDistance; // ���� ����� ���Ϳ��� �Ÿ��� �ʱ�ȭ�մϴ�.

        // ��� ���� ������Ʈ�� �˻��Ͽ� ���� ����� ���͸� ã���ϴ�.
        foreach (GameObject monsterObject in monsterObjects)
        {
            if (monsterObject.activeSelf) // ���� ������Ʈ�� Ȱ��ȭ�� ��쿡�� ó���մϴ�.
            {
                float distanceToMonster = Vector2.Distance(transform.position, monsterObject.transform.position);

                // ���� ���Ϳ��� �Ÿ��� ���� ����� ���Ϳ��� �Ÿ����� �����ٸ�
                if (distanceToMonster < closestDistance)
                {
                    closestDistance = distanceToMonster; // ���� ����� ���ͷ� �����մϴ�.
                }
            }
        }

        // ���� ����� ���Ϳ��� �Ÿ��� proximityDistance �̳���� �ִϸ��̼��� Ȱ��ȭ�մϴ�.
        if (closestDistance < proximityDistance)
        {
            animator.SetBool("Monster", true); // "Monster" �ִϸ��̼��� Ȱ��ȭ�մϴ�.

            if (moveInputX != 0)
            {
                animator.SetBool("MonChWalk", true);
                animator.SetBool("Monster", false);
            }
        }
        else
        {
            animator.SetBool("Monster", false); // "Monster" �ִϸ��̼��� ��Ȱ��ȭ�մϴ�.
            animator.SetBool("MonChWalk", false);
        }
    }
}
