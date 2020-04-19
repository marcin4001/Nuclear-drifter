using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleItem : MonoBehaviour
{
    public string textItem;
    private GUIScript gUI;
    public void ShowText()
    {
        if (gUI == null) gUI = FindObjectOfType<GUIScript>();
        if(gUI != null)gUI.AddText(textItem);
        //Debug.Log(textItem);
    }
}
