using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterDes;
    public float drainSpeed = 0.5f;

    public Transform targetPos;
    public float drownPosY;
    public Animator animator;

    //�ͻ翩��
    [HideInInspector] public bool isDrwon = false;
    bool isDrainageHoleOpen = false;
    bool isGameOverWaterLevel = false;

    public bool IsDrainageHoleOpen
    {
        get { return  isDrainageHoleOpen; }
        set { isDrainageHoleOpen = value; }
    }
    public bool IsGameOverWaterLevel
    {
        get { return isGameOverWaterLevel; }
    }
    public float gmOverWaterLevel;

    void Update()
    {
        //�� ������
        if (isDrainageHoleOpen)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, waterDes), drainSpeed * Time.deltaTime);
        }

        if (transform.position.y < gmOverWaterLevel)
        {
            print("over");
            isGameOverWaterLevel = true;

        }

        //���� ������ ���ӿ���
        if(!isDrwon && !isDrainageHoleOpen && targetPos.position.y < drownPosY)
        {
            isDrwon = true;
            Rigidbody2D targetRb = targetPos.GetComponent<Rigidbody2D>();
            targetRb.velocity = Vector2.down;
            targetRb.gravityScale = 0f;
            print("Over____");
            StartCoroutine(GameOverAfterDelay(0.2f));
            //RespawnManager.Instance.OnGameOverScene.Invoke();
            
        }

        if(targetPos.position.y < drownPosY+6f)
        {
            animator.SetTrigger("DieInWater");
        }

    }

    IEnumerator GameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnManager.Instance.OnGameOverScene.Invoke(); // ���� ���� ������ ��ȯ
    }


}
