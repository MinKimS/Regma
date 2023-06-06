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
        // isActive ������ ���� gameObject�� Ȱ��ȭ/��Ȱ��ȭ�� �����մϴ�.
        gameObject.SetActive(false); // �浹 �������� ��Ȱ��ȭ ���·� ����

        // activeBook�� Ȱ��ȭ�Ǿ� �ִ� ��쿡�� gameObject�� Ȱ��ȭ�մϴ�.
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

        yield return new WaitForSeconds(2f); // 2�� ���

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
        gameObject.SetActive(true); // ĵ���� Ȱ��ȭ
    }
}
