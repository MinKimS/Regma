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

    //손 초기 위치
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
        //장난감을 잡은 경우
        if(collision.CompareTag("Toy") && !bmc.IsMobInWater)
        {
            bc.enabled = false;

            toy = collision.gameObject;
            toy.transform.SetParent(transform);

            //물 효과 영향 안받게
            toy.GetComponent<BoxCollider2D>().enabled = false;
            toy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            toy.GetComponent<Rigidbody2D>().gravityScale = 0f;

            //물고기 튀어오르는거 멈추기
            FishToy fToy = toy.GetComponent<FishToy>();

            if (fToy != null)
            {
                fToy.StopAllCoroutines();
            }

            targetPosY = transform.position.y-7f;
            targetPos = new Vector2(transform.position.x, targetPosY);
            //장난감 끌어내리기
            StartCoroutine(MoveHandDown());
        }

        //낚시대를 잡은 경우
        if(collision.CompareTag("FishingRod"))
        {

        }
    }

    //플레이어 다음 이동시, 물 속에 몬스터 있을때
    //장난감을 향해 손 이동
    public IEnumerator GoCatchToy()
    {
        bc.enabled = true;
        //장난감으로 가기
        while (Vector2.Distance(transform.position, ToyList[TargetToyIdx].position)>0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, ToyList[TargetToyIdx].position, 0.1f);
            yield return null;
        }

        isMoveHand = false;
        TargetToyIdx++;
    }

    //낚시대를 향해 손 이동
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

    //장난감 잡아서 물로 끌고가기
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
