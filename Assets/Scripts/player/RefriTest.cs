using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class RefriTest : MonoBehaviour
{
   // public UnityEvent onCollisionWithAObject;
    public PlayableDirector playableDirector;
    private PTimeLine pTimeLine;
    

    private void Start()
    {
        // PTimeLine 스크립트의 인스턴스를 찾아 저장
        pTimeLine = FindObjectOfType<PTimeLine>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("Refri2"))
        {
            // PTimeLine 스크립트의 IsInteractionCompleted() 메서드를 호출하여 Interaction1 값을 받아옴
            bool isInteractionCompleted = pTimeLine != null ? pTimeLine.IsInteractionCompleted() : false;

            print(isInteractionCompleted);

            if (isInteractionCompleted || Input.GetKeyDown(KeyCode.E))
            {
                // 이벤트 발생
                //onCollisionWithAObject.Invoke();

                // 타임라인 실행
                playableDirector.gameObject.SetActive(true);
                playableDirector.Play();

                gameObject.SetActive(false);
            }
        }
    }

   
}

