using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public List<Item> items;
    public List<Item> randomItem;
    
    public Item GetItem(int id)
    {
        return items.Find(x => x.idItem == id);
    }

    public Slot GetRandomItem()
    {
        int randomItemIndex = Random.Range(0, randomItem.Count);
        Item newItem = randomItem[randomItemIndex];
        int randomAmount = Random.Range(1, 3);
        if(newItem.idItem == 300)
            randomAmount = Random.Range(1, 6);
        Slot newSlot = new Slot(newItem, randomAmount, 0);
        return newSlot;
    }
}
