using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Damage : MonoBehaviour
{
    public GameObject ItemHp;
    public Image HpScreen;

    void Start()
    {
        HpScreen.color = Color.clear;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.CompareTag("Item"))
        {
            ShowHpScreen();
        }
    }

    public void ShowHpScreen()
    {
        HpScreen.color = new Color(1f, 1f, 1f, 1f);
    }
}
