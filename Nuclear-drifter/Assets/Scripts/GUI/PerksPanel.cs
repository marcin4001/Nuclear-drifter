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
    private Health playerHP;
    private bool active = false;
    private SoundsTrigger sound;
    private List<string> otherSkills;
    // Start is called before the first frame update
    void Start()
    {
        otherSkills = new List<string>();
        otherSkills.Add("Horn Removal");
        otherSkills.Add("Lockpicking");
        gUI = FindObjectOfType<GUIScript>();
        typeSc = FindObjectOfType<TypeScene>();
        map = FindObjectOfType<MapControl>();
        fade = FindObjectOfType<FadePanel>();
        perksCanvas = GetComponent<Canvas>();
        exp = FindObjectOfType<Experience>();
        playerHP = FindObjectOfType<Health>();
        sound = FindObjectOfType<SoundsTrigger>();
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
        if (!typeSc.combatState)
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
                ShowInfoConsole("Welcome to the perks system!");
            }
            else
            {
                Time.timeScale = 1.0f;
                consoleDesc.text = "";
                currentPerk = null;
            }
        }
        else
        {
            gUI.AddText("Perks unavailable now.");
        }
        sound.PlayClickButton();
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
        sound.PlayClickButton();
    }

    public void AddBtn()
    {
        sound.PlayClickButton();
        if (currentPerk != null)
        {
            Perk perkObj = SkillsAndPerks.playerSkill.GetPerk(currentPerk.indexPerk);
            if (perkObj != null)
            {
                if (perkObj.level >= currentPerk.maxLevelPerk && !currentPerk.isDisposable)
                {
                    ShowInfoConsole("Max level of this perk has been achieved!");
                    currentPerk = null;
                    return;
                }
                if(currentPerk.isDisposable && perkObj.playerHas)
                {
                    ShowInfoConsole("You already own this perk!");
                    currentPerk = null;
                    return;
                }
                if (!exp.RemoveLevelPoints(currentPerk.cost))
                {
                    ShowInfoConsole("You don't have enough LP!");
                    currentPerk = null;
                    return;
                }
                if (!currentPerk.isDisposable)
                {
                    IncCounterPerk(perkObj);
                }
                else
                {
                    perkObj.AddPerk();
                }
                SwichOnLight(currentPerk);
                AddSkill();
                ShowInfoConsole("Perk added!");
            }
        }
        else
        {
            ShowInfoConsole("No perk have been selected!");
        }
        
    }

    public void OtherSkillBtn()
    {
        sound.PlayClickButton();
        consoleDesc.text = "Other skills:\n";
        foreach(string skill in otherSkills)
        {
            consoleDesc.text += skill + ": " + SkillsAndPerks.playerSkill.Skilled(skill) + "\n";
        }
        consoleDesc.text += "Chance of Hitting:" + "\n";
        consoleDesc.text += "- Guns: " + SkillsAndPerks.playerSkill.chanceToShot + "%\n";
        consoleDesc.text += "- Melee Weapons: " + SkillsAndPerks.playerSkill.chanceToHit + "%\n";
    }

    private void IncCounterPerk(Perk perkObj)
    {
        perkObj.AddPerk(true);
        currentPerk.ChangeTextLevel(perkObj.level);
    }

    public void SwichOnLight(PerkElement _perk)
    {
        _perk.lamp.overrideSprite = lampOn;
    }

    private void AddSkill()
    {
        switch (currentPerk.type)
        {
            case TypePerk.hp:
                {
                    playerHP.AddToMaxHealth(currentPerk.value);
                    break;
                }
            case TypePerk.handDmg:
                SkillsAndPerks.playerSkill.handDamage += currentPerk.value;
                break;
            case TypePerk.gunDmg:
                SkillsAndPerks.playerSkill.additionalGunDamage += currentPerk.value;
                break;
            case TypePerk.largeGun:
                SkillsAndPerks.playerSkill.largeGun = true;
                break;
            case TypePerk.radRes:
                SkillsAndPerks.playerSkill.radResistance = true;
                break;
            case TypePerk.poisonRes:
                SkillsAndPerks.playerSkill.poisonResistance = true;
                break;
            case TypePerk.damageRes:
                SkillsAndPerks.playerSkill.damageResistance = currentPerk.value;
                break;
            case TypePerk.repair:
                SkillsAndPerks.playerSkill.repair = true;
                break;
            default:
                break;
        }
    }

    public void ShowInfoConsole(string info)
    {
        consoleDesc.text = info + "\n";
        consoleDesc.text += "Your point: " + exp.lvlPoint + " LP\n";
        consoleDesc.text += exp.GetDescExp() + "\n";
        consoleDesc.text += "Health: " + playerHP.currentHealth + "/" + playerHP.maxHealth + "\n";
        consoleDesc.text += "Hand Damage: " + SkillsAndPerks.playerSkill.handDamage + "\n";
        //consoleDesc.text += "Chance of Hitting:" + "\n";
        //consoleDesc.text += "- Guns: " + SkillsAndPerks.playerSkill.chanceToShot + "%\n";
        //consoleDesc.text += "- Melee Weapons: " + SkillsAndPerks.playerSkill.chanceToHit + "%\n";
    }
}
