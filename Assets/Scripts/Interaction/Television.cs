using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Television : MonoBehaviour
{
    Animator tvAnim;
    public AudioSource audioSource;
    public Light2D tvLight;

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
        tvLight.intensity = 0f;
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
            ChgColor();

            if (channelIndex >= 2)
            {
                AudioManager.instance.StopSFX("Game Sound_TV");
                isOnTv = false;
                DialogueManager.instance.PlayDlg(dlg[1]);
                door.checkWorkDo++;
                door.isOpen = true;
                interactionData.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void ChgColor()
    {
        switch(channelIndex)
        {
            case 0:
                tvLight.color = Color.white;
                break;
            case 1:
                tvLight.color = Color.green;
                break;
            case 2:
                tvLight.color = Color.blue;
                break;
            default:
                tvLight.color = Color.red;
                break;
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
        tvLight.intensity = 2.5f;
        interactionData.IsRunInteraction = true;
        tvAnim.SetBool("Tv", true);
        isOnTv = true;
        AudioManager.instance.SFXPlayLoop("Game Sound_TV");
    }

    // TV 끄기
    public void TVOff()
    {
        interactionData.IsRunInteraction = false;
        tvAnim.SetBool("Tv", false);
        isOnTv = false;
        AudioManager.instance.StopSFX("Game Sound_TV");
    }
}
