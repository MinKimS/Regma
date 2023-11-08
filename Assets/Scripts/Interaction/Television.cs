using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Television : MonoBehaviour
{
    Animator tvAnim;
    public AudioSource audioSource;
    public Light2D tvLight;

    public Sprite[] channelSprite;
    public GlitchController glitch;
    public InteractionObjData interactionData;
    bool isOnTv = false;
    public Door door;

    bool isInteraction = false;

    public Transform jumpscareEvent;

    private void Awake()
    {
        tvAnim = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        interactionData.IsOkInteracting = true;
        tvLight.intensity = 0f;
    }

    // 책 읽기 전 TV 켜기
    public void TVOnBeforeReadingDiary()
    {
        if(!isInteraction)
        {
            DialogueManager.instance.PlayDlg(interactionData.objDlg[0]);
            tvLight.intensity = 2.5f;
            tvAnim.SetBool("Tv", true);
            isOnTv = true;
            jumpscareEvent.gameObject.SetActive(true);
            AudioManager.instance.SFXPlay("Game Sound_TV");
            isInteraction = true;
        }
        else
        {
            DialogueManager.instance.PlayDlg(interactionData.objDlg[1]);
        }
    }

    // 책 읽은 후 TV 켜기
    public void TVOnAfterReadingDairy()
    {
        glitch.SetGlitchActiveTime(1f);
        tvLight.color = Color.red;
        AudioManager.instance.SFXPlay("Game Sound_TV");
        jumpscareEvent.gameObject.SetActive(false);
    }

    //// TV 끄기
    //public void TVOff()
    //{
    //    tvLight.intensity = 0f;
    //    interactionData.IsRunInteraction = false;
    //    tvAnim.SetBool("Tv", false);
    //    isOnTv = false;
    //    AudioManager.instance.StopSFX("Game Sound_TV");
    //}
}
