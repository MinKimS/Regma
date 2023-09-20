using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PTimeLine : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject refri2Object; // Refri2 태그를 가진 단일 오브젝트

    private bool isCollisionActive = false;
    private bool isCollisionActive2 = false;
    private bool Interaction1 = false; // E 키로 이미 캔버스를 열었는지 확인하는 변수
    private bool isTimelinePlayed = false; // 타임라인이 실행 중인지 여부를 확인하는 변수

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {
            isCollisionActive = true;
            //print("isCollisionActive" + isCollisionActive);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {
            //hasOpened = false;
            Interaction1 = false;
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
            Interaction1 = true;
            refri2Object.SetActive(true);
        }

        if (!isTimelinePlayed && !Interaction1 && Input.GetKeyDown(KeyCode.E) && isCollisionActive2)
        {
            // 타임라인 실행
            playableDirector.gameObject.SetActive(true);
            playableDirector.Play();

            isTimelinePlayed = true; // 타임라인이 실행 중임을 표시합니다.

            power.isBroken = true;
        }
    }
}
