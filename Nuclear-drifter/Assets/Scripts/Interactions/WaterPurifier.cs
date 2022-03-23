using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPurifier : JobDevice
{
    public Slot clearWater;
    private Slot dirtyWater;
    
    public override void Work()
    {
        dirtyWater = inv.FindItem(205);
        if(dirtyWater != null)
        {
            if(!inv.IsFull())
            {
                SetClean();
            }
            else
            {
                Slot water = inv.FindItem(204);
                if(water != null || dirtyWater.amountItem == 1)
                {
                    SetClean();
                }
                else
                {
                    gUI.AddText("Inventory is full");
                }
            }
        }
        else
        {
            gUI.AddText("I don't have dirty water");
        }
    }

    private void SetClean()
    {
        if (player.ObjIsNearPlayer(transform.position, 1.1f))
        {
            inv.RemoveOne(dirtyWater);
            fade.EnableImg(true);
            sound.PlayPurifier();
            Invoke("Clean", 2.3f);
        }
        else gUI.AddText("Purifier is too far");
    }

    private void Clean()
    {
        fade.EnableImg(false);
        inv.Add(clearWater);
    }
}
