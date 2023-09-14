using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathRoomController : MonoBehaviour
{
    //욕조로 이동
    public void LoadBathScene()
    {
        LoadingManager.LoadScene("Bath");
    }
}
