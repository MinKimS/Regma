using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameEvent runEvent;
    public Transform playerPos;
    public Transform startPos;
    private void Start() {
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            runEvent.Raise();
            SetPlayerPos(startPos.position);
        }
    }

    public void SetPlayerPos(Vector3 pos)
    {
        playerPos.position = pos;
    }
}
