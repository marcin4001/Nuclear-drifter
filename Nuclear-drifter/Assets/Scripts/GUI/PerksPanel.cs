using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerksPanel : MonoBehaviour
{
    public Text consoleDesc;
    public PerkElement currentPerk;
    public Sprite lampOn;

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

        PerkElement[] perks = FindObjectsOfType<PerkElement>();
        if(perks.Length > 0)
        {
            foreach(PerkElement perk in perks)
            {
                Perk tempPerk = SkillsAndPerks.playerSkill.GetPerk(perk.indexPerk);
                if(tempPerk != null)
                {
                    if (tempPerk.playerHas)
                    {
                        SwichOnLight(perk);
                        perk.ChangeTextLevel(tempPerk.level);
                    }
                }
            }
        }
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
            currentPerk = null;
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

    public void AddBtn()
    {
        if(currentPerk != null)
        {
            Perk perkObj = SkillsAndPerks.playerSkill.GetPerk(currentPerk.indexPerk);
            if (perkObj != null)
            {
                if (!currentPerk.isDisposable)
                {
                    IncCounterPerk(perkObj);
                }
                else
                {
                    perkObj.AddPerk();
                }
                SwichOnLight(currentPerk);
            }
        }
        else
        {
            consoleDesc.text = "No perk have been selected!\n";
            consoleDesc.text += "Your point: " + exp.lvlPoint + " LP\n";
        }
    }

    private void IncCounterPerk(Perk perkObj)
    {
        if(perkObj.level < currentPerk. maxLevelPerk)
        {
            perkObj.AddPerk(true);
            currentPerk.ChangeTextLevel(perkObj.level);
        }
        else
        {
            consoleDesc.text = "Max level of this perk has been achieved!\n";
            consoleDesc.text += "Your point: " + exp.lvlPoint + " LP\n";
        }
    }

    public void SwichOnLight(PerkElement _perk)
    {
        _perk.lamp.overrideSprite = lampOn;
    }
}
