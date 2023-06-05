using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollAmount;
    [SerializeField]
    private float moveSpeed;

    private float spriteWidth;

    private void Start()
    {
        // 배경 스프라이트의 너비를 구합니다.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // 캐릭터의 움직임에 따라 배경을 스크롤합니다.
        float moveAmount = target.position.x * scrollAmount;
        float offsetX = moveAmount % spriteWidth;

        transform.position = new Vector3(offsetX, transform.position.y, transform.position.z);
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * Mathf.Sign(target.localScale.x);
    }
}
