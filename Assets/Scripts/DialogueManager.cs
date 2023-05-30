using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    //모든 씬에 나올 첫번째 대화들
    public List<Dialogue> dialogueList;
    //현재 출력될 대화
    public Dialogue curDlg;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }

    private void Start() {
        SceneManager.sceneLoaded += SceneChangeEvent;
        curDlg = dialogueList[0];
    }
    //씬 변경 시 수행될 이벤트들
    void SceneChangeEvent(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "LoadingScene")
        {
            curDlg = dialogueList[1];
        }
    }
}
