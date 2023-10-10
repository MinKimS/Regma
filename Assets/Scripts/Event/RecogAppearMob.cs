using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogAppearMob : MonoBehaviour
{
    public GameEvent gmEvent;

    public MobAppear appear;

    public enum ActivateTag
    {
        None,
        Player,
        Mob,
    }
    public ActivateTag aTag = ActivateTag.None;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((aTag == ActivateTag.Player && collision.CompareTag("Player") || aTag == ActivateTag.Mob && collision.CompareTag("Mob")) && !appear.isMobAppear)
        {
            gmEvent.Raise();
            Destroy(this.gameObject);
        }
    }
}
