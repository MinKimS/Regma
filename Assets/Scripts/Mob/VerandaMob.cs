using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class VerandaMob : MonoBehaviour
{
    public Transform spawnPos;
    public Transform mob;
    public Transform target;
    public UpdateRespawnPoint respawnPoint;

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
    }

    private void FixedUpdate()
    {
        if (isTrace && !SmartphoneManager.instance.phone.IsOpenPhone && !RespawnManager.isGameOver)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), traceSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if(RespawnManager.isGameOver)
        {
            print("set position");
            transform.position = new Vector3(target.transform.position.x - 10f, transform.position.y);
            if(isTrace)
            {
                SetIsTrace(false);
            }
        }
        else
        {
            if(!isTrace)
            {
                SetIsTrace(true);
            }
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
