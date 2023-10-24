using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorialScreen : MonoBehaviour
{
    [TextArea]
    public string[] explain;

    public void ShowTutorial(int explainIdx)
    {
        TutorialController.instance.OpenTutorialScreen(explain[explainIdx]);
    }
}
