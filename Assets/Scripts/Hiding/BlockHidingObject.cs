using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾ ���� �� �ڿ� ���̻� ������ ���� ���� ���ϰ� �ϴ� �޼ҵ��
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
