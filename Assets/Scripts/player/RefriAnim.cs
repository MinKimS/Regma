using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefriAnim : MonoBehaviour
{
    public Animator RefriAnimation;



    private bool isCollisionActive = false;
    private bool hasOpened = false; // E 키로 이미 캔버스를 열었는지 확인하는 변수



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {
            //print("ddd");
            isCollisionActive = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {

            hasOpened = false;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollisionActive && Input.GetKeyDown(KeyCode.E) && !hasOpened)
        {
            RefriAnimation.SetBool("Open", true);
            hasOpened = true; // 캔버스를 열었음을 표시

            
        }


    }
}
