using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecogEvent : MonoBehaviour
{
    public UnityEvent playerRecogEvent;
    private void OnTriggerEnter2D(Collider2D other) {
        ActiveEvent();
        gameObject.SetActive(false);
    }

    private void ActiveEvent()
    {
        playerRecogEvent.Invoke();
    }
}
