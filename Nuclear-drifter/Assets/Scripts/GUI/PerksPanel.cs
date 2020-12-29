﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerksPanel : MonoBehaviour
{
    public Text consoleDesc;
    public PerkElement currentPerk;

    private Canvas perksCanvas;
    private GUIScript gUI;
    private TypeScene typeSc;
    private MapControl map;
    private FadePanel fade;
    private Experience exp;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        typeSc = FindObjectOfType<TypeScene>();
        map = FindObjectOfType<MapControl>();
        fade = FindObjectOfType<FadePanel>();
        perksCanvas = GetComponent<Canvas>();
        exp = FindObjectOfType<Experience>();
        perksCanvas.enabled = false;
    }

    public void Open()
    {
        active = !active;
        perksCanvas.enabled = active;
        gUI.blockGUI = active;
        map.keyActive = !active;
        gUI.DeactiveBtn(!active);
        fade.EnableImg(active);
        typeSc.inMenu = active;
        if (active)
        {
            Time.timeScale = 0.0f;
            consoleDesc.text = "Welcome to the perks system!\n";
            consoleDesc.text += "Your point: " + exp.lvlPoint + " LP\n";
        }
        else
        {
            Time.timeScale = 1.0f;
            consoleDesc.text = "";
        }
    }

    public void ShowDesc(PerkElement perk)
    {
        consoleDesc.text = "";
        string descText = "";
        descText += perk.namePerk + "\n";
        descText += perk.description + "\n";
        descText += "Cost: " + perk.cost + " LP\n";
        descText += "Your point: " + exp.lvlPoint + " LP\n";
        consoleDesc.text = descText;
        currentPerk = perk;
    }
}
