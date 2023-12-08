using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    //넘겨질 페이지
    public RectTransform flipPage;
    public GameObject itemDiary;

    public Sprite[] book;
    public Image img;

    public Inventory inven;

    public float flipSpeed = 0.5f;

    public Dialogue dlg;
    [HideInInspector] public bool isAfterDlg = false;

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.E)|| Input.GetKeyDown(KeyCode.Escape)) && DialogueManager.instance._dlgState == DialogueManager.DlgState.End)
        {
            inven.HideDiary();
            print("Diary");
        }
    }

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
    }

    public void FallDiary()
    {
        itemDiary.SetActive(true);
    }

    public void ShowDiary()
    {
        SmartphoneManager.instance.itemUsage.isUsingItem = true;
        flipPage.gameObject.SetActive(false);
        flipPage.rotation = Quaternion.identity;
        img.sprite = book[0];
        gameObject.SetActive(true);
        GameManager.instance._canOpenMenu = false;
    }

    public void HideDiary()
    {
        gameObject.SetActive(false);

        if(!isAfterDlg)
        {
            DialogueManager.instance.PlayDlg(dlg);
            isAfterDlg = true;
        }
        SmartphoneManager.instance.itemUsage.isUsingItem = false;
    }
}
