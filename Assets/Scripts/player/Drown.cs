using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{
    bool drown = false;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -6 && !drown )
        {
            // 플레이어가 -8 이하로 떨어졌을 때 drown 변수를 true로 설정
            // 이 때, 필요에 따라 다른 작업을 수행할 수 있습니다.
            Debug.Log("Player has drowned!");
            drown = true;
            animator.SetBool("DieInWater", true);
            StartCoroutine(GameOverAfterDelay(5.0f));
            //RespawnManager.Instance.OnGameOver.Invoke();
        }
    }

    public void ResetDrownFlag()
    {
        StartCoroutine(SwitchDrownFlag(false));
        animator.SetTrigger("Revive");
        animator.SetBool("DieInWater", false);
    }

    IEnumerator SwitchDrownFlag(bool isDrown)
    { 
    yield return new WaitForSeconds(1);
        drown = isDrown;
    }

    IEnumerator GameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnManager.Instance.OnGameOver.Invoke(RespawnManager.ChangeMethod.MobBased); // 게임 오버 씬으로 전환
    }
}
