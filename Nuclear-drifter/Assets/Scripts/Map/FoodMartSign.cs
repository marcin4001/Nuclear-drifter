using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodMartSign : MonoBehaviour
{
    public TextMeshPro text;
    public TextMeshPro textBigSandy;
    public GameObject trigger;
    public GameObject triggerBigSandy;
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
        }
        if (!PropertyPlayer.property.bigSandyFound)
        {
            textBigSandy.enabled = false;
        }
        else
        {
            if (triggerBigSandy != null)
                Destroy(triggerBigSandy);
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

    public void ActiveTextBigSandy()
    {
        PropertyPlayer.property.bigSandyFound = true;
        textBigSandy.enabled = true;
        gUI.AddText("You found Big Sandy");
        exp.AddExp(100);
        if (triggerBigSandy != null)
            Destroy(triggerBigSandy);
    }
}
