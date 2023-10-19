using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkController : MonoBehaviour
{

    //톡 시작
    public void StartTalk()
    {
        SmartphoneManager.instance.phone.StartTalk();
    }

}
