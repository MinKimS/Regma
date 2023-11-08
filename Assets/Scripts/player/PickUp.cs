using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public GameObject ItemHp;
    public Image HpScreen;


    void Start()
    {
        if(HpScreen != null)
            HpScreen.color = Color.clear;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if(HpScreen != null)
            {
                ShowHpScreen();
                return;
            }

            AudioManager.instance.SFXPlay("Game Sound_Item get");

            //인벤에 아이템 저장
            ItemData item = GetComponent<ItemData>();

            SmartphoneManager.instance.SetInvenItem(item);

            Destroy(this.gameObject);
        }
    }
    public void ShowHpScreen()
    {
        HpScreen.color = new Color(1f, 1f, 1f, 1f);
    }
}
