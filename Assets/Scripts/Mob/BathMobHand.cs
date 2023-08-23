using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobHand : MonoBehaviour
{
    BathMobController bmc;
    BoxCollider2D bc;

    Vector2 targetPos;
    float targetPosY;
    GameObject toy;

    //�� �ʱ� ��ġ
    public Transform handOriginPos;

    public Transform[] ToyList;
    int TargetToyIdx = 0;

    bool isMoveHand = false;

    public Transform FishingRodPos;

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
        //�峭���� ���� ���
        if(collision.CompareTag("Toy") && !bmc.IsMobInWater)
        {
            bc.enabled = false;

            toy = collision.gameObject;
            toy.transform.SetParent(transform);

            //�� ȿ�� ���� �ȹް�
            toy.GetComponent<BoxCollider2D>().enabled = false;
            toy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            toy.GetComponent<Rigidbody2D>().gravityScale = 0f;

            //����� Ƣ������°� ���߱�
            FishToy fToy = toy.GetComponent<FishToy>();

            if (fToy != null)
            {
                fToy.StopAllCoroutines();
            }

            targetPosY = transform.position.y-7f;
            targetPos = new Vector2(transform.position.x, targetPosY);
            //�峭�� �������
            StartCoroutine(MoveHandDown());
        }

        //���ô븦 ���� ���
        if(collision.CompareTag("FishingRod"))
        {

        }
    }

    //�÷��̾� ���� �̵���, �� �ӿ� ���� ������
    //�峭���� ���� �� �̵�
    public IEnumerator GoCatchToy()
    {
        bc.enabled = true;
        //�峭������ ����
        while (Vector2.Distance(transform.position, ToyList[TargetToyIdx].position)>0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, ToyList[TargetToyIdx].position, 0.1f);
            yield return null;
        }

        isMoveHand = false;
        TargetToyIdx++;
    }

    //���ô븦 ���� �� �̵�
    public IEnumerator GoCatchFishingRod()
    {
        bc.enabled = true;

        while(Vector2.Distance(transform.position, FishingRodPos.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, FishingRodPos.position, 0.1f);
            yield return null;
        }

        isMoveHand = false;
    }

    //�峭�� ��Ƽ� ���� ������
    IEnumerator MoveHandDown()
    {
        while (transform.position.y >= targetPosY + 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos, 0.05f);
            yield return null;
        }

        toy.transform.SetParent(null);
        toy.SetActive(false);

        yield return new WaitForSeconds(1f);
        BackOriginPos();
    }

    void BackOriginPos()
    {
        StartCoroutine(BackHandOrigin());
        print("back");
    }

    IEnumerator BackHandOrigin()
    {
        while(Vector2.Distance(transform.position, handOriginPos.position) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, handOriginPos.position, 0.01f);
            yield return null;
        }
        bmc.IsMobTryCatch = false;
    }
}
