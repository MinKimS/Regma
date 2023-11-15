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

    private bool isCollisionActive = false;
    private bool hasOpened = false;

    [SerializeField] GameObject shakingTarget;

    public GameObject glassItem;
    Coroutine vibrate;

    private void Start()
    {
        originalPosition = transform.position;
        shakingOriginalPosition = shakingTarget.transform.position;
        Mirrorcanvas.SetActive(false);
        MirrorImage.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !hasOpened)
        {
            hasOpened = true;
            StartMirror();
        }

        //if (isCollisionActive && Input.GetKeyDown(KeyCode.E) && !hasOpened)
        //{
        //    ShowImage();
        //    hasOpened = true;
        //    MirrorAnimator.SetBool("Broken", true);

        //    if(glassItem != null)
        //    {
        //        glassItem.SetActive(true);
        //    }

        //    PlayMirrorAnimation();
        //}
        //else if (hasOpened && Input.GetKeyDown(KeyCode.E))
        //{
        //    ExitImage();
        //    MirrorAnimator.SetBool("Broken", false);
        //    StopMirrorAnimation();
        //}

        //if(hasOpened){
        //    MirrorAnimator.SetBool("Broken", true);
        //}
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Mirror"))
    //    {
    //        if (count == 0) // 1�� �浹 �Ŀ��� ���� ����
    //        {
    //            StartVibration();
    //        }

    //        isCollisionActive = true;
    //        count++;
    //    }
    //}

    void StartMirror()
    {
        StartCoroutine(IEMirror());
    }
    IEnumerator IEMirror()
    {
        TimelineManager.instance.timelineController.SetCutScene(true);
        TimelineManager.instance.tlstate = TimelineManager.TlState.Play;
        DialogueManager.instance.PlayDlg();
        yield return new WaitUntil(() => DialogueManager.instance._dlgState == DialogueManager.DlgState.End);

        StartVibration();
        isCollisionActive = true;
        count++;
        yield return new WaitForSeconds(1f);

        AudioManager.instance.SFXPlay("Restroom voice 2");
        DialogueManager.instance.PlayDlg();

        yield return new WaitUntil(() => DialogueManager.instance._dlgState == DialogueManager.DlgState.End);
        AudioManager.instance.StopSFX("Restroom voice 2");
        ShowImage();
        MirrorAnimator.SetBool("Broken", true);

        yield return new WaitForSeconds(2f);
        ExitImage();
        MirrorAnimator.SetBool("Broken", false);

        DialogueManager.instance.PlayDlg();

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
        //StopMirrorAnimation();
    }

    // 이미지 애니메이션을 재생하는 메서드
    private void PlayMirrorAnimation()
    {
        if (MirrorAnimator != null && mirrorAnimation != null)
        {
            MirrorAnimator.Play(mirrorAnimation.name);
        }
    }

    //이미지 애니메이션을 정지하는 메서드
    // private void StopMirrorAnimation()
    // {
    //     if (MirrorAnimator != null)
    //     {
    //         MirrorAnimator.Rebind(); // 애니메이션을 초기 상태로 되돌립니다.
    //     }
    // }
}
