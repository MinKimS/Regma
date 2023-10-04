using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public Dialogue dlg;

    bool isSee;
    Vector3 origin;

    public void FirstSee()
    {
        CameraController cam = Camera.main.GetComponent<CameraController>();
        cam.target = transform;
        origin = cam.fixedPosition;
        cam.fixedPosition = Vector3.zero + (Vector3.back*10);
        Camera.main.orthographicSize -= 1f;

        DialogueManager.instance.PlayDlg(dlg);
        isSee = true;
    }

    private void Update()
    {
        if(isSee && DialogueManager.instance._dlgState == DialogueManager.DlgState.End)
        {
            CameraController cam = Camera.main.GetComponent<CameraController>();
            cam.target = PlayerInfoData.instance.playerTr;
            cam.fixedPosition = origin;
            Camera.main.orthographicSize += 1f;
            isSee = false;
        }
    }
}
