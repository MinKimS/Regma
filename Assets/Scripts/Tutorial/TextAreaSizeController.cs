using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAreaSizeController : MonoBehaviour
{
    RectTransform rectTr;
    Vector2 minimumSize = new Vector2(1725.175f, 597.8059f);

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
    }

    private void Update()
    {
        float width = 0f;
        float height = 0f;

        foreach(Transform tr in rectTr)
        {
            RectTransform childRcTr = tr.GetComponent<RectTransform>();
            if(childRcTr != null )
            {
                width = Mathf.Max(width, childRcTr.rect.width);
                height = Mathf.Max(height, childRcTr.rect.height);
            }
        }

        width = Mathf.Max(width, minimumSize.x);
        height = Mathf.Max(height, minimumSize.y);

        rectTr.sizeDelta = new Vector2(width, height);
    }
}
