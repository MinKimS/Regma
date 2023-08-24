using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MirrorControl : MonoBehaviour
{
    public float vibrationDistance = 0.1f;
    public float vibrationSpeed = 1.0f;
    public float vibrationDuration = 2.0f;

    private Vector3 originalPosition;

    //[SerializeField] GameObject Mirrorcontact;

    //Collider2D collidingObject;

    private void Start()
    {
        originalPosition = transform.position;
        //StartVibration();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartVibration();
        }
    }

    private void StartVibration()
    {
        StartCoroutine(Vibrate());
    }

    private IEnumerator Vibrate()
    {
        float elapsedTime = 0f;

        while (elapsedTime < vibrationDuration)
        {
            Vector3 targetPosition = originalPosition + new Vector3(Random.Range(-vibrationDistance, vibrationDistance), 0f, 0f);
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * vibrationSpeed;
                transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
                yield return null;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset position after vibration ends
        transform.position = originalPosition;
    }
}

