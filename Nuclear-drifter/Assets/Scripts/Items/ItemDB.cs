using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public List<Item> items;
    public List<Item> randomZombieItem;
    public List<Item> randomCyclopItem;

    public Item GetItem(int id)
    {
        return items.Find(x => x.idItem == id);
    }

    public Slot GetRandomZombieItem()
    {
        int randomItemIndex = Random.Range(0, randomZombieItem.Count);
        Item newItem = randomZombieItem[randomItemIndex];
        int randomAmount = Random.Range(1, 3);
        if(newItem.idItem == 300)
            randomAmount = Random.Range(1, 6);
        Slot newSlot = new Slot(newItem, randomAmount, 0);
        return newSlot;
    }

    public Slot GetRandomCyclopItem()
    {
        int randomItemIndex = Random.Range(0, randomCyclopItem.Count);
        Item newItem = randomCyclopItem[randomItemIndex];
        int randomAmount = 1;
        int ammo = 0;
        if (newItem.idItem == 300)
            randomAmount = Random.Range(1, 6);
        if (newItem is WeaponItem)
        {
            WeaponItem weaponItem = (WeaponItem)newItem;
            if(!weaponItem.isMeleeWeapon && !weaponItem.isBomb)
                ammo = Random.Range(1, 6);
        }
        Slot newSlot = new Slot(newItem, randomAmount, ammo);
        return newSlot;
    }
}
