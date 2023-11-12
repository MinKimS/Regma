using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Water : MonoBehaviour
{
    public float waterDes;
    public float drainSpeed = 0.5f;

    public Transform targetPos;
    public float drownPosY;
    public Animator animator;

    public Volume volume;
    public VolumeProfile volumeProfile;
    DepthOfField dph;


    //익사여부
    [HideInInspector] public bool isDrwon = false;
    bool isDrainageHoleOpen = false;
    bool isGameOverWaterLevel = false;

    public bool IsDrainageHoleOpen
    {
        get { return  isDrainageHoleOpen; }
        set { isDrainageHoleOpen = value; }
    }
    public bool IsGameOverWaterLevel
    {
        get { return isGameOverWaterLevel; }
    }
    public float gmOverWaterLevel;

    private void Start()
    {
        volumeProfile = volume.profile;
        DepthOfField tmp;
        if (volumeProfile.TryGet(out tmp))
        {
            dph = tmp;
        }
    }
    void Update()
    {
        if(targetPos.position.y < -3)
        {
            dph.focalLength.value = 18f;
        }
        else
        {
            dph.focalLength.value = 1f;
        }

        //물에 빠지면 게임오버
        if(!isDrwon && !isDrainageHoleOpen && targetPos.position.y < drownPosY)
        {
            isDrwon = true;
            Rigidbody2D targetRb = targetPos.GetComponent<Rigidbody2D>();
            targetRb.velocity = Vector2.down;
            targetRb.gravityScale = 0f;
            print("Over____");
            StartCoroutine(GameOverAfterDelay(5f));
            //RespawnManager.Instance.OnGameOverScene.Invoke();
            
        }

        if(targetPos.position.y < drownPosY)
        {
            animator.SetBool("DieInWater", true);
        }

    }

    IEnumerator GameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnManager.Instance.OnGameOverScene.Invoke(); // 게임 오버 씬으로 전환
    }


}
