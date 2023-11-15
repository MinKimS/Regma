using UnityEngine;

public class TvController : MonoBehaviour
{
    public Animator animator;
    //public GameObject canvas;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //canvas.SetActive(false); // 시작 시 캔버스를 비활성화합니다.
    }

    public void SetTVOn()
    {
        // TV를 켜는 애니메이션을 재생합니다.
        animator.SetBool("IsTVOn", true);
        //canvas.SetActive(true); // 애니메이션이 재생되면 캔버스를 활성화합니다.
    }

    public void SetTVOff()
    {
        // TV를 끄는 애니메이션을 재생합니다.
        animator.SetBool("IsTVOn", false);
        //canvas.SetActive(false); // 애니메이션이 종료되면 캔버스를 비활성화합니다.
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
