using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchController : MonoBehaviour
{
    public Transform glitchPanel;

    private void Start()
    {
        SetGlitchActive(false);
    }

    void SetGlitchActive(bool value)
    {
        glitchPanel.gameObject.SetActive(value);
    }

    public void SetGlitchActiveTime(float time)
    {
        StartCoroutine(IESsetGlitchActiveTime(time));
    }

    IEnumerator IESsetGlitchActiveTime(float time)
    {
        AudioManager.instance.SFXPlay("TV noise");
        SetGlitchActive(true);
        yield return new WaitForSeconds(time);
        SetGlitchActive(false);
        AudioManager.instance.StopSFX("TV noise");
    }
}
