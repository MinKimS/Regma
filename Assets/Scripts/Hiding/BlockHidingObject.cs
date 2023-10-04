using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 숨고 난 뒤에 더이상 숨었던 곳에 숨지 못하게 하는 메소드들
public class BlockHidingObject : MonoBehaviour
{
    public bool isFirstHiding = false;
    public GameEvent firstEvent;

    public void BlockDeskPot()
    {
        if(!isFirstHiding)
        {
            isFirstHiding = true;
            firstEvent.Raise();
        }
    }
}
