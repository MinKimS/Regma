using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    //로딩 된 후 다음에 나올 씬
    public static string nextScene;
    //로딩이 진행된 현황을 볼 수 있는 이미지
    public Image progressImg;
    public Transform progressChrTr;
    public Image blackImg;
    public static bool isLoading = false;

    float leftX, rightX, newX;
    void Start()
    {
        leftX = -643.9296f;
        rightX = 643.9296f;
        if(nextScene == "Ending") { blackImg.enabled = true; }
        else { blackImg.enabled = false; }
        StartCoroutine(LoadScene());
    }
    
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        isLoading = true;
        print(nextScene + "으로 이동");
        AudioManager.instance.StopSFXAll();
        if (nextScene != "Ending")
        {
            AudioManager.instance.SFXPlay("Game Sound_Door open2");
        }
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation asOp = SceneManager.LoadSceneAsync(nextScene);
        //로딩바가 다 채워지고 넘어가기 위해
        asOp.allowSceneActivation = false;
        float timer = 0.0f;

        while(!asOp.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            //로딩이 순간적으로 지나가는 거 방지
            if(asOp.progress < 0.9f)
            {
                progressImg.fillAmount = Mathf.Lerp(progressImg.fillAmount, asOp.progress, timer);
                ChangeImgPosition();
                if (progressImg.fillAmount >= asOp.progress) { timer = 0f; }
            }
            else
            {
                progressImg.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                ChangeImgPosition();
                //다음 씬으로 이동
                if (progressImg.fillAmount == 1.0f)
                {
                    asOp.allowSceneActivation=true;
                    isLoading = false;
                    yield break;
                }
            }
        }
    }

    void ChangeImgPosition()
    {
        newX = Mathf.Lerp(leftX, rightX, progressImg.fillAmount);
        progressChrTr.localPosition = new Vector3(newX, progressChrTr.localPosition.y);
    }
}
