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
        // isActive ������ ���� gameObject�� Ȱ��ȭ/��Ȱ��ȭ�� �����մϴ�.
        gameObject.SetActive(false); // �浹 �������� ��Ȱ��ȭ ���·� ����

        // activeBook�� Ȱ��ȭ�Ǿ� �ִ� ��쿡�� gameObject�� Ȱ��ȭ�մϴ�.
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
        gameObject.SetActive(true); // ĵ���� Ȱ��ȭ

     
    }
}
