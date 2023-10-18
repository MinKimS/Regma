using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryItemUsage : MonoBehaviour
{
    [SerializeField]
    PlayerFallingController pfc;
    Diary diary;
    public GameEvent glassEvent;
    public GameEvent squidEvent;

    private void Awake()
    {
        diary = GetComponentInChildren<Diary>();
    }
    private void Start()
    {
        pfc = PlayerInfoData.instance.playerTr.GetComponent<PlayerFallingController>();
        if(diary != null)
        {
            diary.gameObject.SetActive(false);
        }

        SceneManager.sceneLoaded += LoadSceneEvent;
    }
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoadingScene")
        {
            SetInitialization();
        }
    }

    void SetInitialization()
    {
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
        if(squidEvent.GetListeners())
        {
            squidEvent.Raise();
        }
    }

    void BrokenPieceOfGlass()
    {
        print("BrokenPieceOfGalss");
        if(glassEvent.GetListeners())
        {
            glassEvent.Raise();
        }

    }

    void Blanket()
    {
        print("플레이어 점프 낙하 데미지 없음 모드로 변경");
        pfc.isNoFallingDamage = true;
    }
}
