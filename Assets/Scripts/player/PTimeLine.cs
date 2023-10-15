using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class PTimeLine : MonoBehaviour
{

    

    public Animator Refrianim;

    
    public GameObject refri2Object; // Refri2 태그를 가진 단일 오브젝트

    private bool isCollisionActive = false;
   
    private bool Interaction1 = false; // E 키로 이미 캔버스를 열었는지 확인하는 변수

    public GameObject mobAppear;

    //냉장고 아이템 얻기 가능하게
    public REFRIGPower power;

    void Start()
    {
        
        // Refri2 태그를 가진 오브젝트를 비활성화합니다.
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
                print("충돌");
                Refrianim.SetBool("OFF", true);
                Interaction1 = true;
                refri2Object.SetActive(true);
                power.isBroken = true;
                if(mobAppear != null)
                {
                    mobAppear.SetActive(true);
                }


            }

            


        }




    }

    public bool IsInteractionCompleted()
    {
        return Interaction1;
    }


   

}
