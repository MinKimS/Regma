using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjData : MonoBehaviour
{
    public GameEvent[] gmEvent;
    public GameEvent[] cancelEvent;
    int gmEventIdx = 0;
    int cancelEventIdx = 0;
    //test SerializeField
    [SerializeField]
    bool isOkInteracting = false;
    [SerializeField]
    bool isRunInteraction = false;

    public bool IsOkInteracting
    { 
        get { return isOkInteracting; }
        set { isOkInteracting = value; }
    }
    public bool IsRunInteraction
    { 
        get { return isRunInteraction; }
        set { isRunInteraction = value; }
    }

    public int GmEventIdx
    {
        get { return gmEventIdx; }
        set
        {
            if(gmEventIdx < gmEvent.Length-1) { gmEventIdx = value; }
            else { gmEventIdx = 0; }
        }
    }
    public int CancelEventIdx
    {

        get { return cancelEventIdx; }
        set
        {
            if (cancelEventIdx != cancelEvent.Length - 1) { cancelEventIdx++; }
            else { cancelEventIdx = 0; }
        }
    }

    public Dialogue[] objDlg;

    public void PlayObjDlg(int num)
    {
        DialogueManager.instance.PlayDlg(objDlg[num]);
    }
}
