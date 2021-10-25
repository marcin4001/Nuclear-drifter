using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public Slot itemSlot;
    public Barrel[] barrels;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Barrel barrel in barrels)
            barrel.isEmpty = true;

        if(barrels.Length > 0)
        {
            int index = Random.Range(0, barrels.Length);
            barrels[index].isEmpty = false;
        }
    }

    public Slot GetItem()
    {
        return itemSlot;
    }
}
