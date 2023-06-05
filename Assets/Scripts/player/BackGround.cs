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
        // ��� ��������Ʈ�� �ʺ� ���մϴ�.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // ĳ������ �����ӿ� ���� ����� ��ũ���մϴ�.
        float moveAmount = target.position.x * scrollAmount;
        float offsetX = moveAmount % spriteWidth;

        transform.position = new Vector3(offsetX, transform.position.y, transform.position.z);
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * Mathf.Sign(target.localScale.x);
    }
}
