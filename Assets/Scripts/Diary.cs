using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    //넘겨질 페이지
    public RectTransform flipPage;
    //int flipPageIdx = 0;
    bool isFliping = false;
    public GameObject itemDiary;

    public Sprite[] book;
    public Image img;

    public Inventory inven;

    public float flipSpeed = 0.5f;

    public Dialogue dlg;
    bool isAfterDlg = false;

    private void FlipPage()
    {
        StartCoroutine(FlipPaper());
    }

    IEnumerator FlipPaper()
    {
        img.sprite = book[1];
        
        flipPage.gameObject.SetActive(true);
        while(flipPage.rotation.y < 150f)
        {
            flipPage.Rotate(Vector2.up * flipSpeed);
            if(flipPage.rotation.y > 0.9f)
            {
                break;
            }
            yield return null;
        }

        inven.HideDiary();

        //if(isLeft)
        //{
        //    isFliping = true;
        //    while(flipPages.rotation.y < 1f)
        //    {
        //        flipPages.Rotate(Vector2.up * 10);
        //        if(flipPages.rotation.y > 0.7f)
        //        {
        //            flipPage.gameObjects.transform.GetChild(0).gameObject.SetActive(false);
        //        }
        //        yield return new WaitForSeconds(0.05f);
        //    }
        //    //while(flipPages[flipPageIdx].rotation.y < 1f)
        //    //{
        //    //    flipPages[flipPageIdx].Rotate(Vector2.up*10);
        //    //    if(flipPages[flipPageIdx].rotation.y > 0.7f)
        //    //    {
        //    //        flipPage.gameObjects[flipPageIdx].transform.GetChild(0).gameObject.SetActive(false);
        //    //        flipPage.gameObjects[flipPageIdx].transform.GetChild(1).gameObject.SetActive(false);

        //    //        if(flipPageIdx==1)
        //    //        {
        //    //            flipPages[flipPageIdx].transform.SetAsLastSibling();
        //    //        }
        //    //    }
        //    //    yield return new WaitForSeconds(0.05f);
        //    //}
        //    //flipPageIdx++;
            
        //    isFliping = false;
        //}
        //else
        //{
        //    isFliping = true;
        //    flipPageIdx--;
        //    while(flipPages[flipPageIdx].rotation.y > 0f)
        //    {
        //        flipPages[flipPageIdx].Rotate(Vector2.down*10);
        //        if(flipPages[flipPageIdx].rotation.y < 0.7f)
        //        {
        //            flipPage.gameObjects[flipPageIdx].transform.GetChild(0).gameObject.SetActive(true);
        //            flipPage.gameObjects[flipPageIdx].transform.GetChild(1).gameObject.SetActive(true);

        //            if(flipPageIdx==0)
        //            {
        //                flipPages[flipPageIdx].transform.SetAsLastSibling();
        //            }
        //        }
        //        yield return new WaitForSeconds(0.05f);
        //    }
        //    isFliping = false;
        //}
    }

    //private void Update() {
    //    if(SmartphoneManager.instance.inven.filesInven.IsInvenItemActive&&!isFliping)
    //    {
    //        //if(Input.GetKeyDown(KeyCode.LeftArrow)&&flipPageIdx<2&&flipPages[flipPageIdx].rotation.y<=0)
    //        //{
    //        //    FlipPage(true);
    //        //}
    //        //if(Input.GetKeyDown(KeyCode.RightArrow)&&flipPageIdx>0&&flipPages[flipPageIdx-1].rotation.y>=1)
    //        //{
    //        //    FlipPage(false);
    //        //}

    //        if(Input.GetKeyDown(KeyCode.LeftArrow))
    //        {
    //            FlipPage();
    //        }
    //    }
    //}

    public void FallDiary()
    {
        itemDiary.SetActive(true);
    }

    public void ShowDiary()
    {
        flipPage.gameObject.SetActive(false);
        flipPage.rotation = Quaternion.identity;
        img.sprite = book[0];
        gameObject.SetActive(true);
    }

    public void HideDiary()
    {
        gameObject.SetActive(false);

        if(!isAfterDlg)
        {
            DialogueManager.instance.PlayDlg(dlg);
            isAfterDlg = true;
        }
    }
}
