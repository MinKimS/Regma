using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingroomSelection : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite changeSp;
    public Transform dlgSelection;

    public void ChangeFrame()
    {
        DialogueManager.instance.PlayDlg();
        //sr.sprite = changeSp;
    }

    public void NoiseActivation()
    {
        print("nooooooise");
    }

    public void StartErase()
    {
        StartCoroutine(IEStartErase());
    }

    IEnumerator IEStartErase()
    {
        DialogueManager.instance.PlayDlg();
        yield return new WaitWhile(() => DialogueManager.instance._dlgState != DialogueManager.DlgState.End);
        SmartphoneManager.instance.phone.HidePhone();
        DialogueManager.instance._dlgState = DialogueManager.DlgState.Start;
        dlgSelection.gameObject.SetActive(true);
    }
}
