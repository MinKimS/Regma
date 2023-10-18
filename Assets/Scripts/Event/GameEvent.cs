using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Game/GameEvent", order = 0)]
public class GameEvent : ScriptableObject {
    //실행시킬 이벤트가 저장된 곳
    private UnityEvent listeners = new UnityEvent();

    public bool GetListeners()
    {
        return listeners != null ? true : false;
    }

    public void Raise() 
    {
        listeners.Invoke();
    }  
    public void RegisterListener(UnityAction listener) 
    { 
        listeners.AddListener(listener); 
    }  
    public void UnregisterListener(UnityAction listener) 
    { 
        listeners.RemoveListener(listener); 
    } 
}
