using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator playAnim;
    bool state;
    public void SetAnim(Animator anim)
    {
        playAnim = anim;
    }

    public void SetAnimBool(string animState)
    {
        playAnim.SetBool(animState, state);
    }

    public void SetAnimState(bool value)
    {
        state = value;
    }
}
