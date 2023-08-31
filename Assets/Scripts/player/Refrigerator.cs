using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refrigerator : MonoBehaviour
{
    public Image RefriImage;
    public GameObject refricanvas;

    private bool isCollisionActive = false;
    private bool hasOpened = false; // E Ű�� �̹� ĵ������ �������� Ȯ���ϴ� ����

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
            hasOpened = false; // �浹�� ����� �� ĵ������ �ٽ� �� �� �ֵ��� ���� �ʱ�ȭ
            ExitImage(); // �浹�� ����� �� ExitImage �޼��� ȣ���Ͽ� ĵ������ ���� ������Ʈ ��Ȱ��ȭ
        }
    }

    void Update()
    {
        //if (isCollisionActive && Input.GetKeyDown(KeyCode.E) && !hasOpened)
        //{
        //    ShowImage();
        //    hasOpened = true; // ĵ������ �������� ǥ��
        //}
        //else if (hasOpened && Input.GetKeyDown(KeyCode.E))
        //{
        //    ExitImage(); // �̹� ĵ������ ������ E�� ������ ĵ������ ���� ������Ʈ�� ��Ȱ��ȭ
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
