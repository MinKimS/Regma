using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("dd");
            CameraShakeTrigger cameraShakeTrigger = FindObjectOfType<CameraShakeTrigger>();
            if (cameraShakeTrigger != null)
            {
                cameraShakeTrigger.StartCoroutine(cameraShakeTrigger.ShakeCoroutine());
            }
        }
    }
}
