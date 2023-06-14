using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvController : MonoBehaviour
{
    public Animator animator;
    public GameObject canvas;

    private void Start()
    {
        animator = GetComponent<Animator>();
        canvas.SetActive(false); // ���� �� ĵ������ ��Ȱ��ȭ�մϴ�.
    }

    public void SetTVOn()
    {
        // TV�� �Ѵ� �ִϸ��̼��� ����մϴ�.
        animator.SetBool("IsTVOn", true);
        canvas.SetActive(true); // �ִϸ��̼��� ����Ǹ� ĵ������ Ȱ��ȭ�մϴ�.
    }

    public void SetTVOff()
    {
        // TV�� ���� �ִϸ��̼��� ����մϴ�.
        animator.SetBool("IsTVOn", false);
        canvas.SetActive(false); // �ִϸ��̼��� ����Ǹ� ĵ������ ��Ȱ��ȭ�մϴ�.
    }
}
