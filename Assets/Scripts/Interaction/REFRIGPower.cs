using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REFRIGPower : MonoBehaviour
{
    bool isBroken = false;
    bool isOkPressKey = false;

    public SpriteRenderer[] sp;
    int[] lightState = { 0, 0, 0, 0, 0, 0 };
    int pressNum = 0;

    public GameEvent PlayerMoveTrue;
    public GameEvent PlayerMoveFalse;

    InteractionObjData iod;

    public bool IsBroken
    { get { return isBroken; } }

    public void Manipulation()
    {
        print("manipulation");

        //�÷��̾� ������ ����
        PlayerMoveFalse.Raise();

        StartCoroutine(Blink());
    }

    public void CancelManipulation()
    {
        LightOffAll();
        PlayerMoveTrue.Raise();
    }

    private void Start()
    {
        iod = GetComponent<InteractionObjData>();
    }

    private void Update()
    {
        if (isOkPressKey && iod.IsInteracting)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                CancelManipulation();
                iod.IsInteracting = false;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                LightOn(pressNum);
                pressNum += 2;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                LightOn(pressNum + 1);
                pressNum += 2;
            }

            if(pressNum>5)
            {
                isOkPressKey = false;
                if (lightState[0] == 1 && lightState[3] == 1 && lightState[5] == 1)
                {
                    isBroken = true;
                    iod.IsInteracting = true;

                    print("success");
                }

                Invoke("CancelManipulation", 1f);
            }

        }
    }

    void LightOn(int lightNum)
    {
        sp[lightNum].color = Color.white;
        lightState[lightNum] = 1;
    }
    void LightOff(int lightNum)
    {
        sp[lightNum].color = Color.black;
        lightState[lightNum] = 0;
    }
    void LightOffAll()
    {
        for(int i = 0; i < sp.Length; i++)
        {
            LightOff(i);
        }
        pressNum = 0;
    }

    IEnumerator Blink()
    {
        var wait = new WaitForSeconds(0.5f);
        var nextLightWait = new WaitForSeconds(1f);

        LightOn(0);
        yield return wait;
        LightOff(0);

        yield return nextLightWait;
        LightOn(3);
        yield return wait;
        LightOff(3);

        yield return nextLightWait;
        LightOn(5);
        yield return wait;
        LightOff(5);

        isOkPressKey = true;
    }
}