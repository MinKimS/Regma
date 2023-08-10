using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static GameEventListener;

public class GameEventListener : MonoBehaviour
{
    [System.Serializable]
    public struct GmEventResponse
    {
        public GameEvent gmEvent;
        public UnityEvent response;
    }

    public GmEventResponse[] gmEventResponses;

    private void OnEnable()
    {
        foreach(var gmEventResponse in gmEventResponses)
        {
            gmEventResponse.gmEvent.RegisterListener(gmEventResponse.response.Invoke);
        }
    }
    private void OnDisable()
    {
        foreach (var gmEventResponse in gmEventResponses)
        {
            gmEventResponse.gmEvent.UnregisterListener(gmEventResponse.response.Invoke);
        }
    }
}
