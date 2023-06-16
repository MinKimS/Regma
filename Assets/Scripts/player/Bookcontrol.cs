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
    private bool isActive = true; // �浹 ���Ŀ��� ��Ȱ��ȭ�ϱ� ���� ����

    void Start()
    {
        // isActive ������ ���� gameObject�� Ȱ��ȭ/��Ȱ��ȭ�� �����մϴ�.
        gameObject.SetActive(isActive); // �浹 �������� isActive ������ ���� Ȱ��ȭ ���¸� �����մϴ�.

        // activeBook�� Ȱ��ȭ�Ǿ� �ִ� ��쿡�� gameObject�� Ȱ��ȭ�մϴ�.
        if (book.activeSelf)
        {
            gameObject.SetActive(isActive);
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

        yield return new WaitForSeconds(1f); // 2�� ���

        bookImage.enabled = false;
        isAnimationPlaying = false;
        clickCount = 0;
        isActive = false; // �ִϸ��̼��� ����Ǹ� isActive ������ false�� �����Ͽ� ��Ȱ��ȭ�մϴ�.
        gameObject.SetActive(isActive); // ��Ȱ��ȭ ���·� �����մϴ�.
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive && collision.gameObject.CompareTag("Book") && gameObject.CompareTag("player"))
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
