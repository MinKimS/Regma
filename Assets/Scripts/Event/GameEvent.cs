using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Game/GameEvent", order = 0)]
public class GameEvent : ScriptableObject {
    //실행시킬 이벤트가 저장된 곳
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise() 
    {  
        for(int i = listeners.Count -1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(); 
        }
    }  
    public void RegisterListener(GameEventListener listener) 
    { 
        listeners.Add(listener); 
    }  
    public void UnregisterListener(GameEventListener listener) 
    { 
        listeners.Remove(listener); 
    } 
}
