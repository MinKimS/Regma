using UnityEngine;

public class AnimationStateChecker : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // ���� �ִϸ��̼� ���� Ȯ��
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // ���� �ִϸ��̼� ������ �̸� Ȯ��
        string currentStateName = stateInfo.IsName("Base Layer.jump") ? "jump" : "NotYourAnimationName";

        // ���� �ִϸ��̼� ������ ����ȭ�� �ð�(0~1) Ȯ��
        //float normalizedTime = stateInfo.normalizedTime;

        // ���� �ִϸ��̼� ���� ���
        Debug.Log("Current Animation State: " + currentStateName);
        //Debug.Log("Normalized Time: " + normalizedTime);
    }
}
