using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerHiding : MonoBehaviour
{
    public CheckHidingTracing checker;

    //�� �̻� ���� ���ϰ� �ϴ� �̺�Ʈ
    public Transform block;

    private void FixedUpdate()
    {
        if(checker.isMobDisappear)
        {
            checker.isMobDisappear = false;
            block.gameObject.SetActive(true);
        }
    }
}
