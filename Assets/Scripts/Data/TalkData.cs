using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TalkData : MonoBehaviour
{
    public int talkId;
    public TextMeshProUGUI readNumText, talkText, nameText;
    public RectTransform textRT, talkRT;
    public GameObject tail;
    public Image profile;
    public Image profileImage;
    public string userName;
    public int readNum;
}
