using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EqBox 
{
    public List<Slot> eqSlots;
    public bool useKey = false;

    public void SetIDs()
    {
        foreach(Slot slot in eqSlots)
        {
            slot.SetId();
        }
    }

    public void SetItemElement(ItemDB dB)
    {
        foreach (Slot slot in eqSlots)
        {
            slot.SetItemElement(dB);
        }
    }
}
