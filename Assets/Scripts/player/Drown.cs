using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{
    bool drown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -8)
        {
            // �÷��̾ -8 ���Ϸ� �������� �� drown ������ true�� ����
            // �� ��, �ʿ信 ���� �ٸ� �۾��� ������ �� �ֽ��ϴ�.
            Debug.Log("Player has drowned!");
            drown = true;
            RespawnManager.Instance.OnGameOver.Invoke();
        }
    }
}
