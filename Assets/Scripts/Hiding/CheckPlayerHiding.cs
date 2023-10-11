using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerHiding : MonoBehaviour
{
    public CheckHidingTracing checker;

    private void FixedUpdate()
    {
        if(checker.isMobDisappear)
        {
            checker.isMobDisappear = false;
        }
    }
}
