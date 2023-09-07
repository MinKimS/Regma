using UnityEngine;

public class AnimationStateChecker : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 현재 애니메이션 상태 확인
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 현재 애니메이션 상태의 이름 확인
        string currentStateName = stateInfo.IsName("Base Layer.jump") ? "jump" : "NotYourAnimationName";

        // 현재 애니메이션 상태의 정규화된 시간(0~1) 확인
        //float normalizedTime = stateInfo.normalizedTime;

        // 현재 애니메이션 상태 출력
        Debug.Log("Current Animation State: " + currentStateName);
        //Debug.Log("Normalized Time: " + normalizedTime);
    }
}
