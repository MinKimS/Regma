using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobAppearance : MonoBehaviour
{
    public BathMobController bmc;
    public Dialogue dlg;
    public bool isOnTrigger = false;
    //bool isInputReturn = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!isOnTrigger)
            {
                StartCoroutine(AppearMob());
            }
            else
            {
                bmc.StartMoving();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator AppearMob()
    {
        bmc.movement.SetMobPosInitialPos();
        bmc.Appearance();
        DialogueManager.instance.PlayDlg(dlg);

        yield return new WaitWhile(() =>DialogueManager.instance._dlgState == DialogueManager.DlgState.End);

        //isInputReturn = true;
        bmc.hand.MoveHandToToy(8);
        Destroy(gameObject);
    }

    //private void Update()
    //{
    //    if(!isOnTrigger && !isInputReturn && Input.GetKeyDown(KeyCode.Return))
    //    {
    //        isInputReturn = true;
    //        bmc.hand.MoveHandToToy(8);
    //        Destroy(gameObject);
    //    }
    //}
}
