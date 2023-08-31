using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    [HideInInspector]
    public bool isHide = false;

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.E))
        //{
        //    if(!isHide)
        //    {
        //        isHide = true;
        //        print("hide");
        //    }
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isHide)
        {
            isHide = false;
        }
    }
}
