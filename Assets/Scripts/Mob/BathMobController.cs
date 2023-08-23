using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobController : MonoBehaviour
{
    BathMobHand hand;
    BathMobEye eye;
    BathMobMovement movement;

    bool isMobInWater = true;
    bool isMobTryCatch = false;
    bool isMobSeeFishingRod = false;
    bool isMobStuck = false;

    public bool IsMobInWater
    {
        get { return isMobInWater; }
        set { isMobInWater = value; }
    }

    public bool IsMobTryCatch
    {
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
            if(eye.IsFindPlayer)
            {
                isMobTryCatch = true;
                print("Fgacha");
                hand.IsMoveHand = true;
                StartCoroutine(hand.GoCatchFishingRod());
            }
        }
    }

    void SetMobSeeFishingRod()
    {
        transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        StartCoroutine(movement.SeeingFishingRod());
    }

    //모습 등장
    void Appearance()
    {
        StartCoroutine(InOutWater());
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

        while(!isMobStuck)
        {
            OutWater();
            yield return new WaitForSeconds(waitTime*2);

            IntoWater();
            yield return new WaitForSeconds(waitTime);
        }
    }
}
