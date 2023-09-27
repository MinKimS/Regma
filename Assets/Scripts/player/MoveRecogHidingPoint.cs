using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRecogHidingPoint : MonoBehaviour
{
    bool isCrouch = false;
    private void Update()
    {
        //웅크릴때 숨음거 인식하는 지점 이동
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(!isCrouch)
            {
                isCrouch = true;
                transform.position += Vector3.down * 0.4f;
            }
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if(isCrouch)
            {
                transform.position += Vector3.up * 0.4f;
                isCrouch = false;
            }
        }
    }
}
