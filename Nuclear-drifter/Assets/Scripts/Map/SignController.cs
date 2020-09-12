using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Area
{
    public TextMeshPro text;
    public SpriteRenderer cross;
    public GameObject col;
    

    public void ActiveText()
    {
        text.enabled = true;
        cross.enabled = false;
        GameObject.Destroy(col);
    }

    public void DeactiveText()
    {
        text.enabled = false;
        cross.enabled = false;
    }

    public void ActiveCross()
    {
        text.enabled = false;
        cross.enabled = true;
    }

    public void ColliderOff()
    {
        col.GetComponent<BoxCollider2D>().enabled = false;
    }
}


public class SignController : MonoBehaviour
{
    public Area[] areas;
    private GUIScript gUI;
    private TypeScene scene;
    private Experience exp;
    // Start is called before the first frame update
    void Start()
    {
        scene = FindObjectOfType<TypeScene>();
        gUI = FindObjectOfType<GUIScript>();
        exp = FindObjectOfType<Experience>();
        if(areas.Length > 0)
        {
            if(scene.isInterior)
            {
                foreach(Area a in areas)
                {
                    a.ColliderOff();
                }
            }
            for(int i = 0; i < areas.Length; i++)
            {
                if (PropertyPlayer.property.foundArea[i] == 2)
                    areas[i].ActiveText();
                else if (PropertyPlayer.property.foundArea[i] == 1)
                    areas[i].ActiveCross();
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
            exp.AddExp(100);
            PropertyPlayer.property.foundArea[index] = 2;
        }
    }

    public void FindArea(int index)
    {
        if (index >= 0 && index < areas.Length)
        {
            if(PropertyPlayer.property.foundArea[index] != 2)
            {
                areas[index].ActiveCross();
                PropertyPlayer.property.foundArea[index] = 1;
            }
        }
    }
}
