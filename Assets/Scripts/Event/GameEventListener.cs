using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    //event에 저장시킬 이벤트를 지정
    public GameEvent Event;
    public UnityEvent Response;
    private void OnEnable()
    { Event.RegisterListener(this); }
    private void OnDisable()
    { Event.UnregisterListener(this); }
    public void OnEventRaised()
    { Response.Invoke(); }
}
