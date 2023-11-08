using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class PTimeLine : MonoBehaviour
{

    

    public Animator Refrianim;

    
    public GameObject refri2Object; // Refri2 �±׸� ���� ���� ������Ʈ

    private bool isCollisionActive = false;
   
    private bool Interaction1 = false; // E Ű�� �̹� ĵ������ �������� Ȯ���ϴ� ����

    public GameObject mobAppear;
    public GameObject respawnPoint_Power;

    //����� ������ ��� �����ϰ�
    public REFRIGPower power;

    bool isActive = false;
    public MoveAlongThePath mob;

    void Start()
    {
        
        // Refri2 �±׸� ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        if (refri2Object != null)
        {
            refri2Object.SetActive(false);

        }
    }

    

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(!isActive)
                {
                    print("충돌");
                    Refrianim.SetBool("OFF", true);
                    Interaction1 = true;
                    refri2Object.SetActive(true);
                    power.isBroken = true;
                    if (mobAppear != null)
                    {
                        mobAppear.SetActive(true);
                        respawnPoint_Power.SetActive(true);
                    }
                    isActive = true;
                }
                else
                {
                    mob.AppearMob();
                    respawnPoint_Power.gameObject.SetActive(true);
                }
            }
        }
    }

    public bool IsInteractionCompleted()
    {
        return Interaction1;
    }
}
