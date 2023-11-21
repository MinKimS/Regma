using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bookcontrol : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip tearingSound;
    public GameObject book;
    public Image bookImage;

    public Animator bookAnimator;
    private int clickCount = 0;

    private bool isAnimationPlaying = false;
    private bool isActive = true; 

    public Door door;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();

        // ����� �ҽ��� ���� ��쿡�� ������Ʈ�� �߰��մϴ�.
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !isAnimationPlaying)
        //{
        //    if (clickCount == 0)
        //    {
        //        TearingSound(); // �Ҹ� ���
        //    }

        //    clickCount++;

        //    if (clickCount >= 4)
        //    {
        //        bookAnimator.SetBool("cut", true);
        //        StartCoroutine(WaitForAnimation());
        //    }
        //    else
        //    {
        //        bookAnimator.SetBool("cut", false);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.E) && !isAnimationPlaying
            && !GameManager.instance.isMenuOpen
            && !GameManager.instance.isHowtoOpen)
        {
            if (clickCount == 3)
            {
                TearingSound(); // 찢기 소리 재생
            }

            clickCount++;

            if (clickCount >= 5) // 5번째 연타 시
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

        //---

        door.checkWorkDo++;
        door.isOpen = true;

        //---

        gameObject.SetActive(isActive); // ��Ȱ��ȭ ���·� �����մϴ�.
    }

    //void OnCollisionEnter2D(Collision2D collision) // 8.16
    //{
    //    if (isActive && collision.gameObject.CompareTag("Book") && gameObject.CompareTag("player"))
    //    {
    //        ShowImage();
    //    }
    //}

    //public void ShowImage() // 8,16
    //{
    //    book.SetActive(true);
    //    bookImage.enabled = true;
    //    gameObject.SetActive(true); // ĵ���� Ȱ��ȭ
    //}

    void TearingSound()
    {
        if (clickCount == 3 && tearingSound != null)
        {
            audioSource.PlayOneShot(tearingSound);
        }
    }
}
