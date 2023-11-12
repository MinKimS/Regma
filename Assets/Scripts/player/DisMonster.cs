using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisMonster : MonoBehaviour
{
    public Animator animator;

    [SerializeField] GameObject[] monsterObjects; // 여러 몬스터 오브젝트 배열

    [SerializeField] float proximityDistance = 5.0f; // 거리 기준값

    // Update is called once per frame
    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal"); // 좌우 방향키 입력을 받습니다.

        float closestDistance = proximityDistance; // 가장 가까운 몬스터와의 거리를 초기화합니다.

        // 모든 몬스터 오브젝트를 검사하여 가장 가까운 몬스터를 찾습니다.
        foreach (GameObject monsterObject in monsterObjects)
        {
            float distanceToMonster = Vector2.Distance(transform.position, monsterObject.transform.position);

            // 활성화된 몬스터 오브젝트만 고려합니다.
            if (monsterObject.activeSelf && distanceToMonster < closestDistance)
            {
                closestDistance = distanceToMonster; // 가장 가까운 활성화된 몬스터로 설정합니다.
            }
        }

        // 가장 가까운 몬스터와의 거리가 proximityDistance 이내라면 애니메이션을 활성화합니다.
        if (closestDistance < proximityDistance)
        {
            animator.SetBool("Monster", true); // "Monster" 애니메이션을 활성화합니다.

            if (moveInputX != 0)
            {
                animator.SetBool("MonChWalk", true);
                animator.SetBool("Monster", false);
            }
        }
        else
        {
            animator.SetBool("Monster", false); // "Monster" 애니메이션을 비활성화합니다.
            animator.SetBool("MonChWalk", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob") || collision.gameObject.CompareTag("RandMob"))
        {
            print("몹 베이스 방식에 의한 게임오버");
            RespawnManager.Instance.OnGameOver.Invoke(RespawnManager.ChangeMethod.MobBased);
        }
    }
}
