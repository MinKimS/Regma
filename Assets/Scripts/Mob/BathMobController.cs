using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobController : MonoBehaviour
{
    BathMobHand hand;
    BathMobEye eye;
    BathMobMovement movement;
    public Water water;

    bool isMobAppear = false;
    bool isMobInWater = true;
    bool isMobTryCatch = false;
    bool isMobSeeFishingRod = false;
    bool isMobStuck = false;
    [HideInInspector]
    public bool isCatchPlayer = false;
    bool isPlayerBeDrawn = false;

    Transform playerPos;
    public Transform fRod;
    public Transform drawnPos;

    IEnumerator inOutCoroutine;

    public Transform PlayerPos
    { get { return playerPos; } }

    public bool IsMobInWater
    {
        get { return isMobInWater; }
        set { isMobInWater = value; }
    }

    public bool IsMobTryCatch
    {
        get { return isMobTryCatch; }
        set { isMobTryCatch = value; }
    }
    public bool IsMobSeeFishingRod
    {
        get { return isMobSeeFishingRod; }
    }
    public bool IsMobStuck
    {
        get { return isMobStuck; }
    }

    private void Awake()
    {
        hand = GetComponentInChildren<BathMobHand>();
        eye = GetComponentInChildren<BathMobEye>();
        movement = GetComponent<BathMobMovement>();
        playerPos = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        inOutCoroutine = InOutWater();
        //Appearance();

        //test
        //isMobSeeFishingRod = true;
        //SetMobSeeFishingRod();

        //StartCoroutine(eye.RollEye());
    }

    private void Update()
    {
        if (!isMobSeeFishingRod)
        {
            float h = Input.GetAxisRaw("Horizontal");
            if (isMobAppear && !isMobTryCatch && (h != 0 || Input.GetKeyDown(KeyCode.Space)) && (eye.IsFindPlayer || isMobInWater && !eye.isPlayerHide) && !hand.IsMoveHand)
            {
                isMobTryCatch = true;
                print("gacha");
                hand.IsMoveHand = true;
                StartCoroutine(hand.GoCatchPlayer());
            }
            if (isCatchPlayer)
            {
                isCatchPlayer = false;
                movement.MoveDownDeepIntoWater();
            }
            //if(!isMobTryCatch && h != 0 )
            //{
            //    isMobTryCatch = true;
            //    print("gachaPlayer");
            //    hand.IsMoveHand = true;
            //    StartCoroutine(hand.GoCatchPlayer());
            //}
        }
        else
        {
            float v = Input.GetAxisRaw("Vertical");
            if (!isMobTryCatch && eye.IsFindPlayer && v != 0 && !hand.IsMoveHand)
            {
                isMobTryCatch = true;
                print("Fgacha");
                hand.IsMoveHand = true;
                StartCoroutine(hand.GoCatchFishingRod());
            }
        }

        //배수구에 끼이기
        if (!isMobStuck && water.IsDrainageHoleOpen)
        {
            print("stuck");
            isMobStuck = true;
            eye.StopRolling();
            transform.rotation = Quaternion.identity;
            transform.position = fRod.position;
            hand.gameObject.SetActive(false);
            fRod.gameObject.SetActive(false);
        }

        if (water.IsGameOverWaterLevel)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - 20f), 0.01f);
            if (!isPlayerBeDrawn)
            {
                PlayerBeDrawn();
            }
            playerPos.position = Vector2.MoveTowards(playerPos.position, new Vector2(transform.position.x, transform.position.y - 20f), 0.01f);
        }
    }

    void PlayerBeDrawn()
    {
        isPlayerBeDrawn = true;
        playerPos.position = drawnPos.position;
        playerPos.GetComponent<Rigidbody2D>().isKinematic = true;
        playerPos.GetComponent<CapsuleCollider2D>().isTrigger = true;
    }

    //낚시대 사용
    public void StartFishing()
    {
        fRod.gameObject.SetActive(true);
        isMobSeeFishingRod = true;
        StopMoving();
        SetMobSeeFishingRod();

        StartCoroutine(eye.RollEye());
    }

    public void StopMoving()
    {
        StopCoroutine(movement.MoveNextPos());
        StopCoroutine(inOutCoroutine);
        StopCoroutine(movement.GoOutOfTheWater());
        StopCoroutine(movement.GoIntoTheWater());
    }

    //낚시대를 보는 위치로 이동
    void SetMobSeeFishingRod()
    {
        StartCoroutine(movement.SeeingFishingRod());
    }

    //모습 등장
    public void Appearance()
    {
        StartCoroutine(inOutCoroutine);
    }

    //물 밖에 나가기
    void OutWater()
    {
        isMobInWater = false;
        StartCoroutine(movement.GoOutOfTheWater());
        StartCoroutine(eye.RollEye());
    }

    //물 안에 들어가기
    void IntoWater()
    {
        isMobInWater = true;
        StartCoroutine(movement.GoIntoTheWater());
        eye.StopRolling();
    }

    public IEnumerator InOutWater()
    {
        float waitTime = Random.Range(5f, 8f);

        if(!isMobAppear)
        {
            yield return new WaitForSeconds(1f);
            isMobAppear = true;
        }

        while(!isMobStuck)
        {
            OutWater();
            yield return new WaitForSeconds(waitTime*2);
            print("sfdfsg");
            if(isMobSeeFishingRod)
            {
                StopCoroutine(inOutCoroutine);
            }
            IntoWater();
            print("sfdfsg");
            yield return new WaitForSeconds(waitTime);
            if (isMobSeeFishingRod)
            {
                StopCoroutine(inOutCoroutine);
            }
        }
    }
}
