using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/WeaponItem")]
public class WeaponItem : Item
{
    public int ammoAmount = 0;
    public int damage = 0;
    public bool isMeleeWeapon = false;

    public override void Use()
    {
        
    }
}
