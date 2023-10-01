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
        // PTimeLine ��ũ��Ʈ�� �ν��Ͻ��� ã�� ����
        pTimeLine = FindObjectOfType<PTimeLine>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("Refri2"))
        {
            // PTimeLine ��ũ��Ʈ�� IsInteractionCompleted() �޼��带 ȣ���Ͽ� Interaction1 ���� �޾ƿ�
            bool isInteractionCompleted = pTimeLine != null ? pTimeLine.IsInteractionCompleted() : false;

            print(isInteractionCompleted);

            if (isInteractionCompleted || Input.GetKeyDown(KeyCode.E))
            {
                // �̺�Ʈ �߻�
                //onCollisionWithAObject.Invoke();

                // Ÿ�Ӷ��� ����
                playableDirector.gameObject.SetActive(true);
                playableDirector.Play();

                gameObject.SetActive(false);
            }
        }
    }

   
}

