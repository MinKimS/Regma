using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public Image introImg;
    public Sprite[] introImgs;
    int IntroImgidx = 0;
    private void Start() {
        introImg.sprite = introImgs[IntroImgidx++];
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(IntroImgidx < introImgs.Length)
            {
                introImg.sprite =  introImgs[IntroImgidx++];
            }
            else
            {
                LoadingManager.LoadScene("SampleScene");
            }
        }
    }
}
