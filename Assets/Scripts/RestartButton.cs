using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Animator[] buttonAnimators;
    private Button[] buttons; // ��ư �迭 �߰�
    private int selectedButtonIndex = 0;

    void Start()
    {
        // ĵ���� �ȿ� �ִ� ��� ��ư�� Animator �� Button ������Ʈ ��������
        buttonAnimators = GetComponentsInChildren<Animator>();
        buttons = GetComponentsInChildren<Button>();
        SelectButton(selectedButtonIndex);
    }

    void Update()
    {
        // ���� ȭ��ǥ Ű�� ������ ���� ��ư�� ����
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SelectButton(selectedButtonIndex - 1);
        }

        // ������ ȭ��ǥ Ű�� ������ ���� ��ư�� ����
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            SelectButton(selectedButtonIndex + 1);
        }

        // ���� Ű�� ������ ���� ���õ� ��ư Ŭ��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ClickSelectedButton();
        }
    }

    void SelectButton(int index)
    {
        // �ε����� ��ȿ�� ������ ����
        selectedButtonIndex = Mathf.Clamp(index, 0, buttonAnimators.Length - 1);

        // ��� ��ư�� �ִϸ��̼��� ����
        foreach (Animator animator in buttonAnimators)
        {
            animator.SetTrigger("Normal");
        }

        // ���õ� ��ư�� �ִϸ��̼� Ʈ����
        buttonAnimators[selectedButtonIndex].SetTrigger("Highlighted");
    }

    void ClickSelectedButton()
    {
        // ���� ���õ� ��ư�� �ְ� �ִϸ��̼��� Ʈ���ŵ� ��� Ŭ�� �̺�Ʈ �߻�
        if (selectedButtonIndex >= 0 && selectedButtonIndex < buttons.Length)
        {
            buttonAnimators[selectedButtonIndex].SetTrigger("Pressed");
            // ��ư�� Pressed ������ �� �ʿ��� �۾��� ����

            // ���� ���õ� ��ư Ŭ�� �̺�Ʈ �߻�
            buttons[selectedButtonIndex].onClick.Invoke();
        }
    }
}
