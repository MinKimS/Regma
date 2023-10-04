using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    bool grabBox, noGrabBox;
    bool isGrab;
    
    Rigidbody2D rb;

    public LayerMask groundMask;

    public float noGrabXOffset, noGrabYOffset, noGrabXSize, noGrabYSize, grabXOffset, grabYOffset, grabXSize, grabYSize;

    Chmoving moving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moving = GetComponent<Chmoving>();
    }

    private void Update()
    {

        grabBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (grabXOffset * transform.localScale.x), transform.position.y + grabYOffset), new Vector2(grabXSize, grabYSize), 0f, groundMask);
        noGrabBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (noGrabXOffset * transform.localScale.x), transform.position.y + noGrabYOffset), new Vector2(noGrabXSize, noGrabYSize), 0f, groundMask);

        if (grabBox && !noGrabBox && !isGrab && !moving.isGround)
        {
            isGrab = true;
            ClimbPos();
        }

        if(isGrab)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }

    public void ClimbPos()
    {
        transform.position = new Vector2(transform.position.x , transform.position.y + (1.5f * transform.localScale.y));
        transform.position = new Vector2(transform.position.x + (1f * transform.localScale.x), transform.position.y);
        rb.gravityScale = 1f;
        isGrab = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (noGrabXOffset * transform.localScale.x), transform.position.y + noGrabYOffset),new Vector2(noGrabXSize, noGrabYSize));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (grabXOffset * transform.localScale.x), transform.position.y + grabYOffset), new Vector2(grabXSize, grabYSize));

    }
}
