using System.Collections;
using System.Collections.Generic;
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
        if (spawnPos != null)
        {
            transform.position = spawnPos.position;
            StartCoroutine(IETraceMob());
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

    IEnumerator IETraceMob()
    {
        WaitUntil waitGameOver = new WaitUntil(() => RespawnManager.isGameOver);
        WaitUntil waitUnGameOver = new WaitUntil(() => !RespawnManager.isGameOver);
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        WaitForSeconds wait2 = new WaitForSeconds(0.1f);

        yield return new WaitUntil(() => isTrace);
        while (true)
        {
            yield return wait;
            SetIsTrace(true);
            AudioManager.instance.SFXPlay("Exit_Monster");
            yield return waitGameOver;
            SetIsTrace(false);
            AudioManager.instance.StopSFX("Exit_Monster");
            yield return waitUnGameOver;
            yield return wait2;
            transform.position = new Vector3(target.transform.position.x - 25f, transform.position.y);

        }
    }

    public void SetMobPos()
    {
        transform.position = new Vector3(target.transform.position.x - 25f, transform.position.y);
    }

    public void SetIsTrace(bool value)
    {
        isTrace = value;
    }

    public void SetMobVisible(bool value)
    {
        mob.gameObject.SetActive(value);
        AudioManager.instance.SFXPlay("주방_괴생명체 등장");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SetMobVisible(false);
        }
    }
}
