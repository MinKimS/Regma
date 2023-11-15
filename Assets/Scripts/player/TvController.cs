using UnityEngine;

public class TvController : MonoBehaviour
{
    public Animator animator;
    //public GameObject canvas;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //canvas.SetActive(false); // ���� �� ĵ������ ��Ȱ��ȭ�մϴ�.
    }

    public void SetTVOn()
    {
        // TV�� �Ѵ� �ִϸ��̼��� ����մϴ�.
        animator.SetBool("IsTVOn", true);
        //canvas.SetActive(true); // �ִϸ��̼��� ����Ǹ� ĵ������ Ȱ��ȭ�մϴ�.
    }

    public void SetTVOff()
    {
        // TV�� ���� �ִϸ��̼��� ����մϴ�.
        animator.SetBool("IsTVOn", false);
        //canvas.SetActive(false); // �ִϸ��̼��� ����Ǹ� ĵ������ ��Ȱ��ȭ�մϴ�.
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tv") && Input.GetKey(KeyCode.E))
        {
            
            SetTVOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tv"))
        {
            
            SetTVOff();
        }
    }
}
