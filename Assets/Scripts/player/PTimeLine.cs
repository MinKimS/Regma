using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PTimeLine : MonoBehaviour
{
    public PlayableDirector playableDirector;

    private bool isCollisionActive = false;
    private bool isCollisionActive2 = false;
    private bool hasOpened = false; // E Ű�� �̹� ĵ������ �������� Ȯ���ϴ� ����

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {
            isCollisionActive = true;
           // print("isCollisionActive" + isCollisionActive);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {

            hasOpened = false;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
            if (other.CompareTag("Player") && gameObject.CompareTag("Refri2"))
            {
                isCollisionActive2 = true;
                print("isCollisionActive2" + isCollisionActive2);
            }

        
        
    }

    void Update()
    {
        if (isCollisionActive && Input.GetKeyDown(KeyCode.E))
        {
            hasOpened = true;
            print("hasOpened" + hasOpened);

            
        }

        if (!hasOpened && isCollisionActive2 && Input.GetKeyDown(KeyCode.E))
        {
            //print("dd");
            // Ÿ�Ӷ��� ����

            playableDirector.gameObject.SetActive(true);
            playableDirector.Play();

            GameObject[] refri2Objects = GameObject.FindGameObjectsWithTag("Refri2");
            foreach (var refri2Object in refri2Objects)
            {
                refri2Object.SetActive(false);
            }


        }
    }
}
