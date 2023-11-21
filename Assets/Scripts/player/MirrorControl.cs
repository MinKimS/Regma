using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MirrorControl : MonoBehaviour
{
    public float vibrationDistance = 0.1f;
    public float vibrationSpeed = 1.0f;
    public float vibrationDuration = 2.0f;

    private Vector3 originalPosition;
    private Vector3 shakingOriginalPosition;
    private GameObject shakingObject;

    public Animator MirrorAnimator;
    public AnimationClip mirrorAnimation;

    int count = 0;
    public Image MirrorImage;
    public GameObject Mirrorcanvas;

    private bool hasOpened = false;

    [SerializeField] GameObject shakingTarget;

    public GameObject glassItem;
    Coroutine vibrate;
    public Dialogue[] dlg;

    private void Start()
    {
        originalPosition = transform.position;
        shakingOriginalPosition = shakingTarget.transform.position;
        Mirrorcanvas.SetActive(false);
        MirrorImage.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasOpened)
        {
            hasOpened = true;
            StartMirror();
        }
    }

    void StartMirror()
    {
        StartCoroutine(IEMirror());
    }
    IEnumerator IEMirror()
    {
        TimelineManager.instance.timelineController.SetCutScene(true);
        TimelineManager.instance.tlstate = TimelineManager.TlState.Play;
        DialogueManager.instance.PlayDlg(dlg[0]);
        yield return new WaitUntil(() => DialogueManager.instance._dlgState == DialogueManager.DlgState.End);

        StartVibration();
        count++;
        yield return new WaitForSeconds(1f);

        AudioManager.instance.SFXPlay("Restroom voice 2");
        yield return new WaitForSeconds(10f);
        ShowImage();
        MirrorAnimator.SetBool("Broken", true);
        yield return new WaitForSeconds(10.625f);
        AudioManager.instance.StopSFX("Restroom voice 2");
        ExitImage();
        MirrorAnimator.SetBool("Broken", false);

        DialogueManager.instance.PlayDlg(dlg[1]);

        yield return new WaitUntil(() => DialogueManager.instance._dlgState == DialogueManager.DlgState.End);
        if (glassItem != null)
        {
            glassItem.SetActive(true);
        }
        TimelineManager.instance.timelineController.SetCutScene(false);
        TimelineManager.instance.tlstate = TimelineManager.TlState.End;
    }

    public void StartVibration()
    {
        if (shakingTarget != null)
        {
            shakingObject = shakingTarget;
            vibrate = StartCoroutine(Vibrate());
        }
    }

    private void StopVibration()
    {
        if (shakingObject != null)
        {
            StopCoroutine(vibrate);
            shakingObject.transform.position = shakingOriginalPosition;
        }
    }

    private IEnumerator Vibrate()
    {
        float elapsedTime = 0f;

        while (elapsedTime < vibrationDuration)
        {
            Vector3 targetPosition = shakingOriginalPosition + new Vector3(Random.Range(-vibrationDistance, vibrationDistance), 0f, 0f);
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * vibrationSpeed;
                shakingObject.transform.position = Vector3.Lerp(shakingOriginalPosition, targetPosition, t);
                yield return null;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shakingObject.transform.position = shakingOriginalPosition;
    }

    public void ShowImage()
    {
        MirrorAnimator.SetBool("Broken", true);

        Mirrorcanvas.SetActive(true);
        MirrorImage.enabled = true;
        gameObject.SetActive(true);
        StopVibration();
        PlayMirrorAnimation();
        
    }

    public void ExitImage()
    {
        Mirrorcanvas.SetActive(false);
        MirrorImage.enabled = false;
        StopVibration();
    }

    // 이미지 애니메이션을 재생하는 메서드
    private void PlayMirrorAnimation()
    {
        if (MirrorAnimator != null && mirrorAnimation != null)
        {
            MirrorAnimator.Play(mirrorAnimation.name);
        }
    }
}
