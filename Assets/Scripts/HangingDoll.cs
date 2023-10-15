using System.Collections;
using UnityEngine;

public class HangingDoll : MonoBehaviour
{
    public Transform fallPos;
    public float fallingSpeed = 3f;
    public Sprite dollSprite;
    SpriteRenderer sp;
    public Dialogue dlg;
    bool isOkUseSquid = false;
    InteractionObjData interactionObjData;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        interactionObjData = GetComponent<InteractionObjData>();
    }

    public void SaveDoll()
    {
        if (CheckDistanceToPlayer())
        {
            SmartphoneManager.instance.DeleteSelectItem();
            interactionObjData.IsOkInteracting = true;
            StartCoroutine(FallDoll());
        }
        else
        {
            DialogueManager.instance.PlayDlg(dlg);
        }
    }

    public void FeedDoll()
    {
        if (isOkUseSquid)
        {
            if (CheckDistanceToPlayer())
            {
                interactionObjData.IsOkInteracting = true;
                SmartphoneManager.instance.DeleteSelectItem();
                DialogueManager.instance.PlayDlg();
                interactionObjData.GmEventIdx++;
            }
            else
            {
                DialogueManager.instance.PlayDlg(dlg);
            }
        }
    }

    //�������� ������ ����ؼ� �� ��� ������ ��
    IEnumerator FallDoll()
    {
        while (transform.position.y > fallPos.position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, fallPos.position, fallingSpeed);
            yield return null;
        }

        sp.sprite = dollSprite;
        transform.position = fallPos.position;
        transform.localScale = Vector2.one * 1.5f;
    }

    //���� ��ó���� ������ ����ϴ��� Ȯ��
    public bool CheckDistanceToPlayer()
    {
        float dir = Vector2.Distance(PlayerInfoData.instance.playerTr.position, transform.position);
        return dir < 4f ? true : false;
    }

    public void SetIsOkUseSquid()
    {
        interactionObjData.IsOkInteracting = false;
        isOkUseSquid = true;
    }

    public void CarryDoll()
    {
        print("carrydoll");
        gameObject.SetActive(false);
    }
}
