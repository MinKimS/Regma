using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bookcontrol : MonoBehaviour
{
    public GameObject book;
    public Image bookImage;

    public Animator bookAnimator;
    private int clickCount = 0;

    private bool isAnimationPlaying = false;

    void Start()
    {
        // isActive 변수에 따라 gameObject의 활성화/비활성화를 설정합니다.
        gameObject.SetActive(false); // 충돌 이전에는 비활성화 상태로 시작

        // activeBook가 활성화되어 있는 경우에만 gameObject를 활성화합니다.
        if (book.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAnimationPlaying)
        {
            clickCount++;
            if (clickCount >= 10)
            {
                bookAnimator.SetBool("cut", true);
                StartCoroutine(WaitForAnimation());
            }
            else
            {
                bookAnimator.SetBool("cut", false);
            }
        }
    }

    IEnumerator WaitForAnimation()
    {
        isAnimationPlaying = true;
        yield return new WaitForSeconds(bookAnimator.GetCurrentAnimatorStateInfo(0).length);

        yield return new WaitForSeconds(2f); // 2초 대기

        bookImage.enabled = false;
        isAnimationPlaying = false;
        clickCount = 0;
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
        book.SetActive(true);
        bookImage.enabled = true;
        gameObject.SetActive(true); // 캔버스 활성화
    }
}
