using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodMartSign : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject trigger;
    private GUIScript gUI;
    private Experience exp;

    // Start is called before the first frame update
    void Start()
    {
        if(!PropertyPlayer.property.foodMartFound)
        {
            text.enabled = false;
        }
        else
        {
            if(trigger != null)
                Destroy(trigger);
            Destroy(this);
        }
        gUI = FindObjectOfType<GUIScript>();
        exp = FindObjectOfType<Experience>();
    }

    public void ActiveText()
    {
        PropertyPlayer.property.foodMartFound = true;
        text.enabled = true;
        gUI.AddText("You found " + text.text);
        exp.AddExp(100);
        if (trigger != null)
            Destroy(trigger);
    }

}
