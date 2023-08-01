using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobMoveToDestination : MonoBehaviour
{
    public Transform destinationPos;
    public float speed = 2f;

    void Start()
    {
        StartCoroutine(MoveToDestination());
    }

    IEnumerator MoveToDestination()
    {
        while(true)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPos.position, speed * Time.deltaTime);
            yield return null;
        }
    }
}
