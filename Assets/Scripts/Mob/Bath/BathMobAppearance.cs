using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathMobAppearance : MonoBehaviour
{
    public BathMobController bmc;
    public Dialogue dlg;
    public bool isOnTrigger = false;
    public GameObject block;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!isOnTrigger)
            {
                block.SetActive(true);
                collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                StartCoroutine(AppearMob());
            }
            else
            {
                bmc.StartTracingPlayer();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator AppearMob()
    {
        print("appear");
        bmc.movement.SetMobPosInitialPos();
        bmc.Appearance();
        DialogueManager.instance.PlayDlg(dlg);

        yield return new WaitWhile(() => DialogueManager.instance._dlgState != DialogueManager.DlgState.End);

        bmc.hand.SetTargetToy(0);
        bmc.hand.AttackTarget(0.1f*Time.deltaTime);
        
        Destroy(gameObject);
    }
}
