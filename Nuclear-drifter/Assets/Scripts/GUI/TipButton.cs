using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipButton : MonoBehaviour
{
    public string[] tips;
    private GUIScript gUI;
    private SoundsTrigger sound;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        sound = FindObjectOfType<SoundsTrigger>();
    }

    public void ClickTip()
    {
        if(tips.Length > 0)
        {
            gUI.ClearText();
            foreach (string tip in tips)
                gUI.AddText(tip);
        }
        sound.PlayClickButton();
    }
}
