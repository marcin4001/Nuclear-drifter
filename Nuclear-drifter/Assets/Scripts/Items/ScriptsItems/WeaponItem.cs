﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/WeaponItem")]
public class WeaponItem : Item
{
    public int ammoAmount = 0;
    public int damage = 0;
    public bool isMeleeWeapon = false;
    public bool isBomb = false;
    public int soundId = 0;
    public float criticChance;

    private void Awake()
    {
        type = ItemType.Weapon;
    }

    public override void Use()
    {
        CombatSystem combat = FindObjectOfType<CombatSystem>();
        if (combat != null) combat.SelectWeapon(this);
        Debug.Log("Use: " + name);
    }
}
