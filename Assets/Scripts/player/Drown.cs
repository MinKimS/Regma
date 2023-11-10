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
        if (transform.position.y <= -6)
        {
            // �÷��̾ -8 ���Ϸ� �������� �� drown ������ true�� ����
            // �� ��, �ʿ信 ���� �ٸ� �۾��� ������ �� �ֽ��ϴ�.
            Debug.Log("Player has drowned!");
            drown = true;
            animator.SetTrigger("DieInWater");
            StartCoroutine(GameOverAfterDelay(5.0f));
            //RespawnManager.Instance.OnGameOver.Invoke();
        }
    }

    IEnumerator GameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnManager.Instance.OnGameOver.Invoke(); // ���� ���� ������ ��ȯ
    }
}
