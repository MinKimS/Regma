using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobHand : MonoBehaviour
{
    BathMobController bmc;
    BoxCollider2D bc;

    //Vector2 targetPos;
    //float targetPosY;
    public Transform dragPos;
    Vector2 dragPosY;
    GameObject player;
    public Transform playerPos;

    //�� �ʱ� ��ġ
    public Transform handOriginPos;

    public Transform[] ToyList;
    int TargetToyIdx = 0;

    bool isMoveHand = false;

    public bool IsMoveHand
    {
        get { return isMoveHand; }
        set { isMoveHand = value; }
    }

    private void Awake()
    {
        bmc = GetComponentInParent<BathMobController>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾ ���� ���
        if(collision.CompareTag("Player") && !bmc.IsMobInWater)
        {
            bc.enabled = false;

            player = collision.gameObject;
            player.transform.SetParent(transform);

            //�� ȿ�� ���� �ȹް�
            player.GetComponent<CapsuleCollider2D>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().gravityScale = 0f;

            //targetPosY = transform.position.y-10f;
            //targetPos = new Vector2(transform.position.x, targetPosY);
            dragPosY = new Vector2(transform.position.x, dragPos.position.y);
            //�÷��̾� �������
            bmc.StopMoving();
            StartCoroutine(MoveHandDown());
            bmc.isCatchPlayer = true;
        }

        //���ô븦 ���� ���
        if(collision.CompareTag("FishingRod"))
        {
            bc.enabled = false;

            GameObject fRod = collision.gameObject;
            fRod.transform.SetParent(transform);

            //targetPosY = transform.position.y -8f;
            dragPosY = new Vector2(transform.position.x, dragPos.position.y);
            StartCoroutine(DropPlayer());
        }
    }

    //�÷��̾� ���� �̵���, �� �ӿ� ���� ������
    //�峭���� ���� �� �̵�
    public IEnumerator GoCatchToy()
    {
        //�峭������ ����
        while (Vector2.Distance(transform.position, ToyList[TargetToyIdx].position)>0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, ToyList[TargetToyIdx].position, 0.1f);
            yield return null;
        }

        bc.enabled = true;
        isMoveHand = false;
        TargetToyIdx++;
    }

    public IEnumerator GoCatchPlayer()
    {
        while (Vector2.Distance(transform.position, playerPos.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, 1f);
            yield return null;
        }

        bc.enabled = true;
        isMoveHand = false;
    }

    //���ô븦 ���� �� �̵�
    public IEnumerator GoCatchFishingRod()
    {
        bmc.fRod.gameObject.GetComponent<FishingRod>().IsCaught = true;

        while (Vector2.Distance(transform.position, bmc.fRod.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, bmc.fRod.position, 0.1f);
            yield return null;
        }
        bc.enabled = true;

        isMoveHand = false;
    }

    //���� ������
    IEnumerator MoveHandDown()
    {
        while (transform.position.y >= dragPos.position.y + 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, dragPosY, 0.1f);
            yield return null;
        }

        player.transform.SetParent(null);
        player.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        BackOriginPos();
    }

    //�÷��̾ �������
    IEnumerator DropPlayer()
    {
        //�Ʒ��� ������ �� ����
        while (transform.position.y >= dragPos.position.y + 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, dragPosY, 0.05f);
            yield return null;
        }

        //�÷��̾ �Ʒ��� ������
        bmc.PlayerPos.position = transform.position + Vector3.left * 3;
        bmc.PlayerPos.rotation = Quaternion.Euler(0f, 0f, 180f);
        yield return new WaitForSeconds(1.5f);
        Rigidbody2D pRb = bmc.PlayerPos.GetComponent<Rigidbody2D>();
        pRb.gravityScale = 0f;
        pRb.velocity = Vector3.zero;
    }

    void BackOriginPos()
    {
        StartCoroutine(BackHandOrigin());
        print("back");
    }

    //���� �� ��ġ�� �̵�
    IEnumerator BackHandOrigin()
    {
        while(Vector2.Distance(transform.position, handOriginPos.position) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, handOriginPos.position, 0.1f);
            yield return null;
        }
        bmc.IsMobTryCatch = false;
    }
}
