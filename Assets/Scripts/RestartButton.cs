using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Animator[] buttonAnimators;
    private Button[] buttons; // 버튼 배열 추가
    private int selectedButtonIndex = 0;

    void Start()
    {
        // 캔버스 안에 있는 모든 버튼의 Animator 및 Button 컴포넌트 가져오기
        buttonAnimators = GetComponentsInChildren<Animator>();
        buttons = GetComponentsInChildren<Button>();
        SelectButton(selectedButtonIndex);
    }

    void Update()
    {
        // 왼쪽 화살표 키를 누르면 이전 버튼을 선택
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SelectButton(selectedButtonIndex - 1);
        }

        // 오른쪽 화살표 키를 누르면 다음 버튼을 선택
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            SelectButton(selectedButtonIndex + 1);
        }

        // 엔터 키를 누르면 현재 선택된 버튼 클릭
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ClickSelectedButton();
        }
    }

    void SelectButton(int index)
    {
        // 인덱스를 유효한 범위로 조정
        selectedButtonIndex = Mathf.Clamp(index, 0, buttonAnimators.Length - 1);

        // 모든 버튼의 애니메이션을 리셋
        foreach (Animator animator in buttonAnimators)
        {
            animator.SetTrigger("Normal");
        }

        // 선택된 버튼만 애니메이션 트리거
        buttonAnimators[selectedButtonIndex].SetTrigger("Highlighted");
    }

    void ClickSelectedButton()
    {
        // 현재 선택된 버튼이 있고 애니메이션이 트리거된 경우 클릭 이벤트 발생
        if (selectedButtonIndex >= 0 && selectedButtonIndex < buttons.Length)
        {
            buttonAnimators[selectedButtonIndex].SetTrigger("Pressed");
            // 버튼이 Pressed 상태일 때 필요한 작업을 수행

            // 현재 선택된 버튼 클릭 이벤트 발생
            buttons[selectedButtonIndex].onClick.Invoke();
        }
    }
}
