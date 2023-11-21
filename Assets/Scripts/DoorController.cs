using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public REFRIG refrig;
    public Dialogue dlg;
    public void ExitDoor(string sceneName)
    {
        if(refrig != null)
        {
            if(refrig.isGetSquid)
            {
                AudioManager.instance.StopSFXAll();
                LoadingManager.LoadScene(sceneName);
            }
            else
            {
                if(dlg!= null)
                {
                    DialogueManager.instance.PlayDlg(dlg);
                }
            }
        }
    }
}
