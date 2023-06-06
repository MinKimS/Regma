using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bookcontrol : MonoBehaviour
{
    public GameObject activeBook;
    public Image bookImage;

    void Start()
    {
        // isActive 변수에 따라 gameObject의 활성화/비활성화를 설정합니다.
        gameObject.SetActive(false); // 충돌 이전에는 비활성화 상태로 시작

        // activeBook가 활성화되어 있는 경우에만 gameObject를 활성화합니다.
        if (activeBook.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Book") && gameObject.CompareTag("player"))
        {
            ShowImage();
        }
    }

    public void ShowImage()
    {
        activeBook.SetActive(true);
        bookImage.enabled = true;
        gameObject.SetActive(true); // 캔버스 활성화

     
    }
}
