using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : MonoBehaviour
{
    Animator tvAnim;
    public AudioSource audioSource;
    

    public Dialogue[] dlg;
    public Sprite[] channelSprite;
    int channelIndex = 0;
    public InteractionObjData interactionData;
    bool isOnTv = false;
    public Door door;

    private void Awake()
    {
        tvAnim = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        interactionData.IsOkInteracting = true;
    }

    private void Update()
    {
        if (isOnTv)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                channelIndex++;
                if (channelIndex >= channelSprite.Length)
                {
                    channelIndex = 0;
                    //PlayTvSound();
                }
                print(channelIndex);

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                channelIndex--;
                if (channelIndex < 0)
                {
                    channelIndex = channelSprite.Length - 1;
                    
                }
                print(channelIndex);

            }

            if (channelIndex >= 2)
            {
                isOnTv = false;
                DialogueManager.instance.PlayDlg(dlg[1]);
                door.checkWorkDo++;
                door.isOpen = true;
                interactionData.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    // 책 읽기 전 TV 켜기
    public void TVOnBeforeReadingDiary()
    {
        DialogueManager.instance.PlayDlg(dlg[0]);
    }

    // 책 읽은 후 TV 켜기
    public void TVOnAfterReadingDairy()
    {
        interactionData.IsRunInteraction = true;
        tvAnim.SetBool("Tv", true);
        isOnTv = true;
        PlayTvSound();
    }

    // TV 끄기
    public void TVOff()
    {
        interactionData.IsRunInteraction = false;
        tvAnim.SetBool("Tv", false);
        isOnTv = false;
    }

    void PlayTvSound()
    {
        // 오디오 소스의 클립을 TvSound로 설정
        audioSource.Play();  // 오디오 소스 재생
    }
}
