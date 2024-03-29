using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroController : MonoBehaviour
{
    public Image introImg;
    public Sprite[] introImgs;
    public GameObject[] dlgImg;
    int introImgidx = 0;
    int introDlgIdx = 0;

    public TextMeshProUGUI dlgText;
    public float typingSpeed = 0.5f;
    bool isTyping = false;

    public Transform dowon;

    //인트로에 나오는 대사
    List<string> dlgIntro = new List<string>();

    private void Start() {
        introImg.sprite = introImgs[introImgidx++];
        SetIntroDlg();
    }
    void Update()
    {
        if(isTyping == false && Input.GetKeyDown(KeyCode.Return))
        {
            dlgText.text = "";
            if(introImgidx < 27)
            {
                if(introImgidx != 24)
                {
                    ChgImg();
                    if (introImgidx > 4 && introImgidx != 11 && introImgidx != 16 && introImgidx != 20 && introImgidx != 23 && introImgidx < 24)
                    {
                        StartCoroutine(TypingDlg(dlgIntro[introDlgIdx++]));
                    }
                    introImgidx++;
                    if (introImgidx == 24)
                    {
                        Invoke("ChgImg", 1f);
                    }
                }
            }
            else
            {
                LoadingManager.LoadScene("SampleScene");
            }
        }
    }

    //시간 관계상 이렇게 구현
    void ChgImg()
    {
        if (introImgidx < 5)
        {
            introImg.sprite = introImgs[introImgidx];
        }
        else if (introImgidx == 5)
        {
            introImg.sprite = introImgs[5];
        }
        else if (introImgidx == 11)
        {
            introImg.sprite = introImgs[9];
        }
        else if (introImgidx == 12)
        {
            introImg.sprite = introImgs[6];
        }
        else if (introImgidx == 16)
        {
            introImg.sprite = introImgs[10];
        }
        else if (introImgidx == 17)
        {
            introImg.sprite = introImgs[7];
        }
        else if (introImgidx == 20)
        {
            introImg.sprite = introImgs[11];
        }
        else if (introImgidx == 21)
        {
            introImg.sprite = introImgs[8];
        }
        else if (introImgidx == 23)
        {
            introImg.sprite = introImgs[8];
        }
        else if (introImgidx == 24)
        {
            introImg.sprite = introImgs[12];
            introImgidx++;
        }
        else if (introImgidx == 25)
        {
            introImg.sprite = introImgs[13];
        }
        else if (introImgidx == 26)
        {
            introImg.sprite = introImgs[14];
        }


        if (introImgidx == 3)
        {
            dowon.gameObject.SetActive(true);
        }
        else
        {
            dowon.gameObject.SetActive(false);
        }

        //dowon
        if (introImgidx == 8 || introImgidx == 15 || introImgidx == 19)
        {
            dlgImg[0].SetActive(true);
        }
        else
        {
            dlgImg[0].SetActive(false);
        }
        //jiae
        if (introImgidx == 5 || introImgidx == 10 || introImgidx == 17 || introImgidx == 21)
        {
            dlgImg[1].SetActive(true);
        }
        else
        {
            dlgImg[1].SetActive(false);
        }
        //aho
        if (introImgidx == 6 || introImgidx == 9 || introImgidx == 13 || introImgidx == 14 || introImgidx == 18 || introImgidx == 22)
        {
            dlgImg[2].SetActive(true);
        }
        else
        {
            dlgImg[2].SetActive(false);
        }
        //imjung
        if (introImgidx == 7 || introImgidx == 12)
        {
            dlgImg[3].SetActive(true);
        }
        else
        {
            dlgImg[3].SetActive(false);
        }
    }

    private IEnumerator TypingDlg(string text)
    {        
        isTyping = true;
        dlgText.text = "";
        int dlgWordIdx = 0;

        //대화 출력하는 부분
        while(dlgWordIdx != text.Length)
        {
            dlgText.text += text[dlgWordIdx++];
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void SetIntroDlg()
    {
        dlgIntro.Add("실제로 다 만나니까 너무 반갑다아-!");
        dlgIntro.Add("형님 맞죠?\n영화에 공짜 엑스트라 시키려고 우리 끌고 온 거죠?");
        dlgIntro.Add("난 아니에요... 촬영할 카메라도 없구.");
        dlgIntro.Add("잉? 아호 님이나 지애 님 두 분 중에 아니셨어요?");
        dlgIntro.Add("에? 뭐야, 왜 다들 아니라 그러는거야~");
        dlgIntro.Add("그냥 빨리 들어가보자!\n너무 깜깜해지면 더 무섭단 말이야...");
        dlgIntro.Add("제가 먼저 들어갈게요.\n다들 헤드 랜턴 키시고 차례로 들어오세요.");
        dlgIntro.Add("자자, 나는 쫄보라서 뒤에 서는게 아니고\n지켜주려고 그러는 거야, 알았지?");
        dlgIntro.Add("그러니까 제가 맨 뒤에 설게요!");
        dlgIntro.Add("근데 랜턴 불이 약한 건가?\n안이 안보이지 않아요?");
        dlgIntro.Add("...아호야, 다시 문 열어봐봐.");
        dlgIntro.Add("어..어? 문이 안 열려.");
        dlgIntro.Add("같이 할게요.");
        dlgIntro.Add("구라 치치 말고 빨리 열라니까?!");
        dlgIntro.Add("아 진짜 안 열린다고!");
    }
}
