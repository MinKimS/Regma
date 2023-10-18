using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{
    float randomX = 0f;
    float startX = -3f;
    float endX = 3f;
    public float fallingInterval = 1f;
    public PoolingManager poolingManager;

    private void Start()
    {
        //StartCoroutine(SetFallingPosition());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            poolingManager.SpawnObject(0);
            print("pool");
        }
    }

    IEnumerator SetFallingPosition()
    {
        WaitForSeconds wait = new WaitForSeconds(fallingInterval);

        while(true)
        {
            randomX = Random.Range(startX, endX);

            print("set falling object position : " + randomX);

            yield return wait;

        }


    }
}
