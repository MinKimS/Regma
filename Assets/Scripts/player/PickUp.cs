using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public GameObject ItemHp;
    public Image HpScreen;

    bool isActive = false;

    void Start()
    {
        HpScreen.color = Color.clear;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
            //Damage HpControl = collision.GetComponent<Damage>();
            //if (HpControl != null)
            //{
            //    HpControl.ShowHpScreen();
            //    ItemHp.SetActive(true);
            //    HpScreen.enabled = true;
            //    gameObject.SetActive(true);
            //}
            ShowHpScreen();
        }
    }

    public void ShowHpScreen()
    {
        HpScreen.color = new Color(1f, 1f, 1f, 1f);
    }


}
