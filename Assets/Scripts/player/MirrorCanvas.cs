using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCanvas : MonoBehaviour
{
    public Animator MirrorAnimator; // 캔버스의 Animator 컴포넌트

    private void OnEnable()
    {
        // 캔버스가 활성화되면 애니메이션을 재생합니다.
        PlayCanvasAnimation();
    }

    // private void OnDisable()
    // {
    //     // 캔버스가 비활성화되면 애니메이션을 정지합니다.
    //     StopCanvasAnimation();
    // }

    // 캔버스 애니메이션을 재생하는 메서드
    private void PlayCanvasAnimation()
    {
        if (MirrorAnimator != null)
        {
           MirrorAnimator.SetBool("Broken", true);
        }
    }

    // // 캔버스 애니메이션을 정지하는 메서드
    // private void StopCanvasAnimation()
    // {
    //     if (canvasAnimator != null)
    //     {
    //         canvasAnimator.Rebind(); // 애니메이션을 초기 상태로 되돌립니다.
    //     }
    // }
}
