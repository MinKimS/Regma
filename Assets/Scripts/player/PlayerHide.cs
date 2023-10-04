using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    [HideInInspector]
    public bool isHide = false;
    [HideInInspector]
    public bool isTryHiding = false;
    CapsuleCollider2D col;
    Rigidbody2D rb;

    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }


}
