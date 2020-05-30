﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/WeaponItem")]
public class WeaponItem : Item
{
    public int ammoAmount = 0;
    public int damage = 0;
    public bool isMeleeWeapon = false;

    private void Awake()
    {
        type = ItemType.Weapon;
    }

    public override void Use()
    {
        Debug.Log("Use: " + name);
    }
}
