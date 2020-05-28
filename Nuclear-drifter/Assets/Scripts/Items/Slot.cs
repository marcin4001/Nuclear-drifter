using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    public Item itemElement;
    public int amountItem = 0;
    public int ammo = 0;

    public Slot()
    {
        itemElement = null;
        amountItem = 0;
        ammo = 0;
    }

    public Slot(Item item, int amount, int _ammo)
    {
        itemElement = item;
        amountItem = amount;
        ammo = _ammo;
    }

    public string GetAmount()
    {
        if(amountItem >= 1000)
        {
            int amount = amountItem / 1000;
            return amount + "K";
        }
        if (amountItem == 1) return "";
        return amountItem + "";
    }

    public string GetAmmo()
    {
        if (ammo >= 1000)
        {
            int amount = ammo / 1000;
            return amount + "k";
        }
        return ammo + "";
    }
}
