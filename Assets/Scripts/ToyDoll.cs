using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyDoll : MonoBehaviour
{
    public Dialogue dlg;

    public void FirstMeet()
    {
        DialogueManager.instance.PlayDlg(dlg);
    }
}
