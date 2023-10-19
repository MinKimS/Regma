using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{
    float randomX = 0f;
    public float startX = 3f;
    public float endX = 3f;
    public int spawnObjectIndex = 0;
    public float fallingInterval = 1f;
    
    public PoolingManager poolingManager;

    public Sprite[] sp;

    private void Start()
    {
        StartCoroutine(SetFallingPosition());
    }

    IEnumerator SetFallingPosition()
    {
        WaitForSeconds wait = new WaitForSeconds(fallingInterval);

        while(true)
        {
            randomX = Random.Range(transform.position.x - startX, transform.position.x + endX);
            GameObject obj = poolingManager.SpawnObject(spawnObjectIndex);
            obj.transform.position = new Vector2(randomX, transform.position.y);
            obj.GetComponent<SpriteRenderer>().sprite = sp[Random.Range(0, sp.Length)];

            yield return wait;

        }


    }
}
