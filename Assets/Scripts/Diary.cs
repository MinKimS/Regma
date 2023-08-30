using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    //넘겨질 페이지
    public RectTransform[] flipPages;
    //실제 페이지
    public GameObject[] pages;
    int flipPageIdx = 0;
    bool isFliping = false;
    public GameObject itemDiary;

    private void FlipPage(bool isLeft)
    {
        StartCoroutine(FlipPaper(isLeft));
    }

    IEnumerator FlipPaper(bool isLeft)
    {
        if(isLeft)
        {
            isFliping = true;
            while(flipPages[flipPageIdx].rotation.y < 1f)
            {
                flipPages[flipPageIdx].Rotate(Vector2.up*10);
                if(flipPages[flipPageIdx].rotation.y > 0.7f)
                {
                    pages[flipPageIdx].transform.GetChild(0).gameObject.SetActive(false);
                    pages[flipPageIdx].transform.GetChild(1).gameObject.SetActive(false);

                    if(flipPageIdx==1)
                    {
                        flipPages[flipPageIdx].transform.SetAsLastSibling();
                    }
                }
                yield return new WaitForSeconds(0.05f);
            }
            flipPageIdx++;
            
            isFliping = false;
        }
        else
        {
            isFliping = true;
            flipPageIdx--;
            while(flipPages[flipPageIdx].rotation.y > 0f)
            {
                flipPages[flipPageIdx].Rotate(Vector2.down*10);
                if(flipPages[flipPageIdx].rotation.y < 0.7f)
                {
                    pages[flipPageIdx].transform.GetChild(0).gameObject.SetActive(true);
                    pages[flipPageIdx].transform.GetChild(1).gameObject.SetActive(true);

                    if(flipPageIdx==0)
                    {
                        flipPages[flipPageIdx].transform.SetAsLastSibling();
                    }
                }
                yield return new WaitForSeconds(0.05f);
            }
            isFliping = false;
        }
    }

    private void Update() {
        if(SmartphoneManager.instance.inven.filesInven.IsInvenItemActive&&!isFliping)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow)&&flipPageIdx<2&&flipPages[flipPageIdx].rotation.y<=0)
            {
                FlipPage(true);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)&&flipPageIdx>0&&flipPages[flipPageIdx-1].rotation.y>=1)
            {
                FlipPage(false);
            }
        }
    }

    public void FallDiary()
    {
        itemDiary.SetActive(true);
    }
}
