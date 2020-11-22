using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public List<Item> items;
    
    public Item GetItem(int id)
    {
        return items.Find(x => x.idItem == id);
    }
}
