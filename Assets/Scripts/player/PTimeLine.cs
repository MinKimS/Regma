using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PTimeLine : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject refri2Object; // Refri2 �±׸� ���� ���� ������Ʈ

    private bool isCollisionActive = false;
    private bool isCollisionActive2 = false;
    private bool Interaction1 = false; // E Ű�� �̹� ĵ������ �������� Ȯ���ϴ� ����
    private bool isTimelinePlayed = false; // Ÿ�Ӷ����� ���� ������ ���θ� Ȯ���ϴ� ����

    //����� ������ ��� �����ϰ�
    public REFRIGPower power;

    void Start()
    {
        // Refri2 �±׸� ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
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
            // Ÿ�Ӷ��� ����
            playableDirector.gameObject.SetActive(true);
            playableDirector.Play();

            isTimelinePlayed = true; // Ÿ�Ӷ����� ���� ������ ǥ���մϴ�.

            power.isBroken = true;
        }
    }
}
