using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipButton : MonoBehaviour
{
    public string[] tips;
    private GUIScript gUI;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
    }

    public void ClickTip()
    {
        if(tips.Length > 0)
        {
            gUI.ClearText();
            foreach (string tip in tips)
                gUI.AddText(tip);
        }
    }
}
