using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Area
{
    public TextMeshPro text;
    public GameObject col;

    public void ActiveText()
    {
        text.enabled = true;
        GameObject.Destroy(col);
    }

    public void DeactiveText()
    {
        text.enabled = false;
    }
}


public class SignController : MonoBehaviour
{
    public Area[] areas;
    private GUIScript gUI;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        if(areas.Length > 0)
        {
            for(int i = 0; i < areas.Length; i++)
            {
                if (PropertyPlayer.property.foundArea[i])
                    areas[i].ActiveText();
                else
                    areas[i].DeactiveText();
            }
        }
    }

    public void FoundArea(int index)
    {
        if (index >= 0 && index < areas.Length)
        {
            areas[index].ActiveText();
            gUI.AddText("You found " + areas[index].text.text);
            PropertyPlayer.property.foundArea[index] = true;
        }
    }
}
