using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RecogEvent : MonoBehaviour
{
    public UnityEvent ActiveRecogEvent;
    //이벤트 설명 용(나중엔 제거)
    [TextArea]
    public string descriptionEvent;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveEvent();
        }
    }

    private void ActiveEvent()
    {
        ActiveRecogEvent.Invoke();
        gameObject.SetActive(false);
    }
}
