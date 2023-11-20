using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryItemUsage : MonoBehaviour
{
    [SerializeField]
    PlayerFallingController pfc;
    [SerializeField]
    Diary diary;
    public GameEvent glassEvent;
    public GameEvent squidEvent;
    public Dialogue cantUseDlg;

    [HideInInspector] public bool isUsingItem;
    private void Awake()
    {
        diary = GetComponentInChildren<Diary>();
    }
    private void Start()
    {
        diary.gameObject.SetActive(false);
        if ((LoadingManager.nextScene != "Ending" || SceneManager.GetActiveScene().name != "Ending") && PlayerInfoData.instance != null)
        {
            pfc = PlayerInfoData.instance.playerTr.GetComponent<PlayerFallingController>();
        }
        if(diary != null)
        {
            diary.gameObject.SetActive(false);
        }

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadSceneEvent;
    }
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoadingScene")
        {
            StartCoroutine(SetInitialization());
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadSceneEvent;
    }

    IEnumerator SetInitialization()
    {
        Scene sc = SceneManager.GetActiveScene();
        yield return new WaitWhile(() => sc.name == "LoadingScene");
        if(PlayerInfoData.instance != null)
            pfc = PlayerInfoData.instance.playerTr.GetComponent<PlayerFallingController>();
    }

    public void UseItem(ItemData item)
    {
        switch(item.itemID)
        {
            case 0 : Diary(); break;
            case 1 : Blanket(); break;
            case 2 : Squid(); break;
            case 3 : BrokenPieceOfGlass(); break;
        }
    }

    void Diary()
    {
        print("Diary");
        if(diary != null)
        {
            diary.ShowDiary();
        }
    }

    void Squid()
    {
        print("Squid");
        if(squidEvent != null && squidEvent.GetListeners())
        {
            squidEvent.Raise();
        }
        else
        {
            DialogueManager.instance.PlayDlg(cantUseDlg);
        }
    }

    void BrokenPieceOfGlass()
    {
        print("BrokenPieceOfGalss");
        if(glassEvent != null && glassEvent.GetListeners())
        {
            glassEvent.Raise();
        }
        else
        {
            DialogueManager.instance.PlayDlg(cantUseDlg);
        }

    }

    void Blanket()
    {
        print("플레이어 점프 낙하 데미지 없음 모드로 변경");
        pfc.isNoFallingDamage = true;
    }
}
