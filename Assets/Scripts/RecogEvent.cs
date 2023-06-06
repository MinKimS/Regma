using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecogEvent : MonoBehaviour
{
    public UnityEvent ActiveRecogEvent;
    //나중엔 제거
    [TextArea]
    public string descriptionEvent;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag.Equals("Player"))
        {
            ActiveEvent();
            gameObject.SetActive(false);
        }
    }

    private void ActiveEvent()
    {
        ActiveRecogEvent.Invoke();
    }
}
