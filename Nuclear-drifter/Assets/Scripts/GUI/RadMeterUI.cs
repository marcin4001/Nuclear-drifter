using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadMeterUI : MonoBehaviour
{
    public float posYShort = -60f;
    public float posYMedium = -28f;
    public float posYHigh = 3f;
    public int test = 0;

    public RectTransform pointerPos;
    public Image backgroundImg;
    public Image pointerImg;
    // Start is called before the first frame update
    void Start()
    {
        SetRadLevel(test);
    }

    public void SetRadLevel(int level)
    {
        if(level <= 0)
        {
            backgroundImg.enabled = false;
            pointerImg.enabled = false;
        }
        else if (level == 1)
        {
            pointerPos.anchoredPosition = new Vector2(pointerPos.anchoredPosition.x, posYShort);
        }
        else if(level == 2)
        {
            pointerPos.anchoredPosition = new Vector2(pointerPos.anchoredPosition.x, posYMedium);
        }
        else
        {
            pointerPos.anchoredPosition = new Vector2(pointerPos.anchoredPosition.x, posYHigh);
        }

        if (level > 0)
        {
            backgroundImg.enabled = true;
            pointerImg.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (test >= 3)
                test = 0;
            else
                test++;

            SetRadLevel(test);
        }
    }
}
