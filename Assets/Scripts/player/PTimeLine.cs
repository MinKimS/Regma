using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class PTimeLine : MonoBehaviour
{

    

    public Animator Refrianim;

    
    public GameObject refri2Object; // Refri2 �±׸� ���� ���� ������Ʈ
   
    private bool Interaction1 = false; // E Ű�� �̹� ĵ������ �������� Ȯ���ϴ� ����

    public GameObject mobAppear;
    public GameObject respawnPoint_Power;

    //����� ������ ��� �����ϰ�
    public REFRIGPower power;

    bool isActive = false;
    public MoveAlongThePath mob;

    public Dialogue dlg;

    void Start()
    {
        
        // Refri2 �±׸� ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        if (refri2Object != null)
        {
            refri2Object.SetActive(false);

        }
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            if (!isActive)
    //            {
    //                print("충돌");
    //                Refrianim.SetBool("OFF", true);
    //                AudioManager.instance.SFXPlay("Kitchen_power off");
    //                Interaction1 = true;
    //                refri2Object.SetActive(true);
    //                power.isBroken = true;
    //                if (mobAppear != null)
    //                {
    //                    StartCoroutine(IEMobAppear());
    //                }
    //                isActive = true;
    //            }
    //            else
    //            {
    //                mob.AppearMob();
    //                respawnPoint_Power.gameObject.SetActive(true);
    //            }
    //        }
    //    }
    //}

    public void InteractionPower()
    {
        if (!isActive)
        {
            print("충돌");
            Refrianim.SetBool("OFF", true);
            AudioManager.instance.SFXPlay("Kitchen_power off");
            Interaction1 = true;
            refri2Object.SetActive(true);
            power.isBroken = true;
            if (mobAppear != null)
            {
                StartCoroutine(IEMobAppear());
            }
            isActive = true;
        }
        else
        {
            mob.AppearMob();
            respawnPoint_Power.gameObject.SetActive(true);
        }
    }

    IEnumerator IEMobAppear()
    {
        DialogueManager.instance.PlayDlg(dlg);

        yield return new WaitUntil(() => DialogueManager.instance._dlgState == DialogueManager.DlgState.End);
        yield return new WaitForSeconds(0.2f);

        mobAppear.SetActive(true);
        respawnPoint_Power.SetActive(true);
    }

    public bool IsInteractionCompleted()
    {
        return Interaction1;
    }
}
