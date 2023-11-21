using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAudioWithDlg : MonoBehaviour
{
    public Dialogue dlg;
    public void PlayAudio(string audio)
    {
        if(!RespawnManager.isGameOver)
        {
            TimelineManager.instance.tlstate = TimelineManager.TlState.Play;
            TimelineManager.instance.timelineController.cutSceneAppearence.SetBool("isRunCutScene", true);

            AudioManager.instance.SFXPlay(audio);
            StartCoroutine(IEPlayAudio(audio));
        }
    }

    IEnumerator IEPlayAudio(string audio)
    {
        yield return new WaitForSeconds(14.625f);
        AudioManager.instance.StopSFX(audio);
        DialogueManager.instance.PlayDlg(dlg);

        yield return new WaitUntil(() => DialogueManager.instance._dlgState == DialogueManager.DlgState.End);

        TimelineManager.instance.timelineController.cutSceneAppearence.SetBool("isRunCutScene", false);
        TimelineManager.instance.tlstate = TimelineManager.TlState.End;
    }

    public void StopAudio(string audio)
    {
        AudioManager.instance.StopSFX(audio);
    }
}
