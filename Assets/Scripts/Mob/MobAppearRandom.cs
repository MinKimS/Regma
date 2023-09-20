using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAppearRandom : MonoBehaviour
{
    public GameObject mob;
    public MoveAlongThePath matp;

    public float minDisappearTime = 3f;
    public float maxDisappearTime = 5f;
    bool isDisappear = false;

    BoxCollider2D col;

    float time = 0f;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    //랜덤한 위치에 등장
    void RandomAppear()
    {
        time = 0f;
        MobAppearRandonController.isVisibleRandomMob = true;
        mob.transform.position = PlayerInfoData.instance.playerTr.position + new Vector3(Random.Range(3f, 5f), Random.Range(5f, 8f));
        mob.SetActive(true);
        matp.IsTrace = true;
        AudioManager.instance.SFXPlayLoop("주방_괴생명체1 도원 추격");
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!MobAppearRandonController.isVisibleRandomMob && collision.CompareTag("Player"))
        {
            RandomAppear();
        }
    }

    private void Update()
    {
        if (mob.activeSelf)
        {
            time += Time.deltaTime;
        }
        if (!isDisappear && MobAppearRandonController.isVisibleRandomMob && time >= Random.Range(minDisappearTime, maxDisappearTime))
        {
            DisappearMob();
        }
    }

    void DisappearMob()
    {
        isDisappear = true;
        mob.SetActive(false);
        matp.IsTrace=false;
        AudioManager.instance.StopSFX("주방_괴생명체1 도원 추격");
        gameObject.SetActive(false);
    }
}
