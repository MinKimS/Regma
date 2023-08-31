using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refrigerator : MonoBehaviour
{
    public Image RefriImage;
    public GameObject refricanvas;

    private bool isCollisionActive = false;
    private bool hasOpened = false; // E 키로 이미 캔버스를 열었는지 확인하는 변수

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {
            isCollisionActive = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RefriObject"))
        {
            isCollisionActive = false;
            hasOpened = false; // 충돌이 종료될 때 캔버스를 다시 열 수 있도록 변수 초기화
            ExitImage(); // 충돌이 종료될 때 ExitImage 메서드 호출하여 캔버스와 게임 오브젝트 비활성화
        }
    }

    void Update()
    {
        //if (isCollisionActive && Input.GetKeyDown(KeyCode.E) && !hasOpened)
        //{
        //    ShowImage();
        //    hasOpened = true; // 캔버스를 열었음을 표시
        //}
        //else if (hasOpened && Input.GetKeyDown(KeyCode.E))
        //{
        //    ExitImage(); // 이미 캔버스를 열었고 E를 누르면 캔버스와 게임 오브젝트를 비활성화
        //}
    }

    public void ShowImage()
    {
        refricanvas.SetActive(true);
        RefriImage.enabled = true;
        gameObject.SetActive(true);
    }

    public void ExitImage()
    {
        refricanvas.SetActive(false);
        RefriImage.enabled = false;
        //gameObject.SetActive(false);
    }
}
