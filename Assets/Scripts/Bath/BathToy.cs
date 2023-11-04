using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BathToy : MonoBehaviour
{
    public BathToy preToy;
    public BathMobController bmc;
    public float drawnSpeed = 10f;
    [HideInInspector]
    public bool isBeingTarget = false;
    [HideInInspector]
    public bool isDrawning = false;
    [HideInInspector] public BoxCollider2D toyBC;

    private void Awake()
    {
        toyBC = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(transform.position.y < -20)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            bmc.hand.bathToy = this;
            if(bmc.data.state != BathMobData.State.RuningWild && preToy != null && preToy.gameObject.activeSelf && !preToy.isBeingTarget)
            {
                preToy.GetComponent<Collider2D>().enabled = false;
                preToy.GetComponent<Rigidbody2D>().gravityScale = 0f;
                StartCoroutine(IEDrawn());
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            bmc.hand.bathToy = null;
    }

    IEnumerator IEDrawn()
    {
        isDrawning = true;
        float disPosY = preToy.transform.position.y - 4f;
        while (preToy!=null && preToy.gameObject.activeSelf && preToy.transform.position.y > disPosY)
        {
            preToy.transform.position = Vector2.MoveTowards(preToy.transform.position, preToy.transform.position + Vector3.down, drawnSpeed * Time.deltaTime);
            yield return null;
        }
        preToy.gameObject.SetActive(false);
    }
}
