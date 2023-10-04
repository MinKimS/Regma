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
    private bool isActive = true; // 충돌 이후에는 비활성화하기 위한 변수

    public Door door;

    void Start()
    {
        //// isActive 변수에 따라 gameObject의 활성화/비활성화를 설정합니다. 8.16
        //gameObject.SetActive(isActive); // 충돌 이전에는 isActive 변수에 따라 활성화 상태를 설정합니다.

        //// activeBook가 활성화되어 있는 경우에만 gameObject를 활성화합니다.
        //if (book.activeSelf)
        //{
        //    gameObject.SetActive(isActive);
        //}

        // AudioSource 컴포넌트를 가져옵니다.
        audioSource = GetComponent<AudioSource>();

        // 오디오 소스가 없는 경우에는 컴포넌트를 추가합니다.
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAnimationPlaying)
        {
            if (clickCount == 0)
            {
                TearingSound(); // 소리 재생
            }

            clickCount++;

            if (clickCount >= 4)
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

        yield return new WaitForSeconds(1f); // 2초 대기

        bookImage.enabled = false;
        isAnimationPlaying = false;
        clickCount = 0;
        isActive = false; // 애니메이션이 종료되면 isActive 변수를 false로 설정하여 비활성화합니다.

        //---

        door.checkWorkDo++;
        door.isOpen = true;

        //---

        gameObject.SetActive(isActive); // 비활성화 상태로 설정합니다.
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
    //    gameObject.SetActive(true); // 캔버스 활성화
    //}

    void TearingSound()
    {
        if (clickCount == 0 && tearingSound != null)
        {
            audioSource.PlayOneShot(tearingSound);
        }
    }
}
