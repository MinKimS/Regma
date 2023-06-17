using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SendTalk", menuName = "Smartphone/SendTalk", order = 0)]
public class SendTalk : ScriptableObject {
    [TextArea]
    public string talkText;
    public SendTalk nextSendTalk;
}