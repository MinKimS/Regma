using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    int selectionOne = 0;
    int selectionTwo = 0;

    public void SetSelection(bool isTwo)
    {
        if(!isTwo)
        {
            selectionOne++;
        }
        else
        {
            selectionTwo++;
        }
    }

    //엔딩 루트 확정
    public void SetEndingRoute()
    {
        if(selectionOne > selectionTwo)
        {
            GameManager.instance.SetEndingRoute(false);
        }
        else if(selectionTwo>= selectionOne)
        {
            GameManager.instance.SetEndingRoute(true);
        }
    }
}
