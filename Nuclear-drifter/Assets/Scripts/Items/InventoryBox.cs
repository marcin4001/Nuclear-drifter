using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBox : MonoBehaviour
{
    public EqBox[] boxes;
    public Slot[] freeItems;

    void Awake()
    {
        foreach (EqBox box in boxes)
        {
            box.SetIDs();
        }
        foreach (Slot slot in freeItems)
        {
            slot.SetId();
        }
        bool isLoad = SaveAndLoad.LoadTemp(this);
        if (isLoad)
        {
            ItemDB dB = FindObjectOfType<ItemDB>();
            foreach (EqBox box in boxes)
            {
                box.SetItemElement(dB);
            }
            foreach (Slot slot in freeItems)
            {
                slot.SetItemElement(dB);
            }
        }
    }

    public Slot GetFreeItem(int index)
    {
        if(freeItems.Length > 0)
        {
            if (index >= 0 && index < freeItems.Length)
            {
                return freeItems[index];
            }
            else return null;
        }
        else return null;
    }
}
