using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestItem : MonoBehaviour
{
    public Item item;
    public Image img;
    public Text textInfo;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        if(item != null)
        {
            img.overrideSprite = item.image;
            string txt = item.nameItem + "\n";
            txt += item.description + "\n";
            txt += item.value + "\n";
            txt += "Type: " + item.GetItemType();
            textInfo.text = txt;
        }
    }

    // Update is called once per frame
    public void Change()
    {
        if (item != null)
        {
            img.overrideSprite = item.image;
            
            string txt = item.nameItem + "\n";
            txt += item.description + "\n";
            txt += item.value + "\n";
            txt += "Type: " + item.GetItemType();
            textInfo.text = txt;
        }
    }
}
