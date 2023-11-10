using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class VerandaMob : MonoBehaviour
{
    public Transform spawnPos;
    public Transform mob;
    Transform target;

    bool isTrace = false;

    public float traceSpeed = 7f;

    private void OnEnable()
    {
        if(spawnPos != null)
        {
            transform.position = spawnPos.position;
        }
    }

    private void Start()
    {
        transform.position = spawnPos.position;

        mob.gameObject.SetActive(false);

        target = PlayerInfoData.instance.playerTr;
    }

    private void Update()
    {
        if(isTrace && !SmartphoneManager.instance.phone.IsOpenPhone)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), traceSpeed * Time.fixedDeltaTime);
        }
    }

    public void SetIsTrace(bool value)
    {
        isTrace = value;
    }

    public void SetMobVisible(bool value)
    {
        mob.gameObject.SetActive(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SetMobVisible(false);
        }
    }
}
