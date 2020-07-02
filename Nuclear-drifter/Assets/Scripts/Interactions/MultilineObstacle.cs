using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultilineObstacle : MonoBehaviour
{
    public string[] texts;
    // Start is called before the first frame update
    private GUIScript gUI;
    public void ShowText()
    {
        if (gUI == null) gUI = FindObjectOfType<GUIScript>();
        
        if (gUI != null) {
            if (texts.Length >= 2) gUI.ClearText();
            foreach (string t in texts) gUI.AddText(t);
        }
        //Debug.Log(textItem);
    }
}
