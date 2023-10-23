using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    public Slot itemSlot;
    public Bag[] bags;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Bag bag in bags)
            bag.isEmpty = true;

        if(bags.Length > 0)
        {
            int index = Random.Range(0, bags.Length);
            bags[index].isEmpty = false;
        }
    }

    public Slot GetItemSlot()
    {
        return itemSlot;
    }
}
