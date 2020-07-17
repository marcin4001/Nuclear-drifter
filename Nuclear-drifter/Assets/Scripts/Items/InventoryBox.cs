using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBox : MonoBehaviour
{
    public EqBox[] boxes;
    public Slot[] freeItems;

    void Awake()
    {
        SaveAndLoad.LoadTemp(this);
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
