using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartPos : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SetPlayerPosStartPos());
    }

    IEnumerator SetPlayerPosStartPos()
    {
        yield return new WaitUntil(() => PlayerInfoData.instance != null);
        PlayerInfoData.instance.playerTr.position = transform.position;
    }
}
