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

    private void Awake()
    {
        type = ItemType.Food;
    }

    public override void Use()
    {
        Debug.Log("Use: " + name);
    }
}
