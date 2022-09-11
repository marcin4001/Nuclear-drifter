﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFood", menuName = "Item/FoodItem")]
public class FoodItem : Item
{
    public int addHP = 0;
    public bool healsPoison = false;
    public bool healsRad = false;
    public bool isPoison = false;
    public bool addRad = false;
    public bool isDrink = false;
    private Health hp;
    private Irrigation irrigation;

    private void Awake()
    {
        type = ItemType.Food;
    }

    public void SetHP(Health _hp)
    {
        hp = _hp;
        irrigation = _hp.GetComponent<Irrigation>();
    }

    public override void Use()
    {
        //Debug.Log("Use: " + name);
        if(hp != null)
        {
            hp.AddHealth(addHP);
            if (healsPoison) hp.SetPoison(false);
            if (healsRad) hp.SetRad(false);
            if (isPoison) hp.SetPoison(true);
            if (addRad && !SkillsAndPerks.playerSkill.radResistance) hp.SetRad(true);
            if (isDrink) irrigation.Drink();
        }
    }
}
