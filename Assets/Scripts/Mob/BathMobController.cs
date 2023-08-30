using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobController : MonoBehaviour
{
    BathMobHand hand;
    BathMobEye eye;
    BathMobMovement movement;
    public Water water;

    bool isMobInWater = true;
    bool isMobTryCatch = false;
    bool isMobSeeFishingRod = false;
    bool isMobStuck = false;

    Transform playerPos;
    public Transform fRod;

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
        //Appearance();

        //test
        isMobSeeFishingRod = true;
        SetMobSeeFishingRod();

        StartCoroutine(eye.RollEye());
    }

    private void Update()
    {
        if(!isMobSeeFishingRod)
        {
            float h = Input.GetAxisRaw("Horizontal");
            if (!isMobTryCatch && h != 0 && (eye.IsFindPlayer || isMobInWater) && !hand.IsMoveHand)
            {
                isMobTryCatch = true;
                print("gacha");
                hand.IsMoveHand = true;
                StartCoroutine(hand.GoCatchToy());
            }
        }
        else
        {
            float v = Input.GetAxisRaw("Vertical");
            if(!isMobTryCatch && eye.IsFindPlayer && v != 0 && !hand.IsMoveHand)
            {
                isMobTryCatch = true;
                print("Fgacha");
                hand.IsMoveHand = true;
                StartCoroutine(hand.GoCatchFishingRod());
            }
        }

        //������� ���̱�
        if(!isMobStuck && water.IsDrainageHoleOpen)
        {
            isMobStuck = true;
            transform.rotation = Quaternion.identity;
            transform.position = fRod.position;
            hand.gameObject.SetActive(false);
        }

        if(water.IsGameOverWaterLevel)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -20f), 0.01f);
        }
    }

    void SetMobSeeFishingRod()
    {
        transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        StartCoroutine(movement.SeeingFishingRod());
    }

    //��� ����
    void Appearance()
    {
        StartCoroutine(InOutWater());
    }

    //�� �ۿ� ������
    void OutWater()
    {
        isMobInWater = false;
        StartCoroutine(movement.GoOutOfTheWater());
        StartCoroutine(eye.RollEye());
    }

    //�� �ȿ� ����
    void IntoWater()
    {
        isMobInWater = true;
        StartCoroutine(movement.GoIntoTheWater());
        eye.StopRolling();
    }

    public IEnumerator InOutWater()
    {
        float waitTime = Random.Range(5f, 8f);

        while(!isMobStuck)
        {
            OutWater();
            yield return new WaitForSeconds(waitTime*2);

            IntoWater();
            yield return new WaitForSeconds(waitTime);
        }
    }
}
