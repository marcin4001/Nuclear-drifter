using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDealer : Job
{
    public Slot mask;
    public Slot currency;
    public int moduleStart;
    private Inventory inv;
    private NPCBasic nPC;

    private void Start()
    {
        inv = FindObjectOfType<Inventory>();
        nPC = GetComponent<NPCBasic>();
    }

    public override string Work(int opt)
    {
        if (opt < 0) return "I have no such item. I'm sorry.";
        
        if (mask == null) return "I have no such item. I'm sorry.";
        if (mask.itemElement == null) return "I have no such item. I'm sorry.";
        if (inv.CanBuy(mask))
        {
            if (inv.Add(mask))
            {
                currency.amountItem = mask.amountItem * mask.itemElement.value;
                inv.RemoveFew(currency);
                nPC.SetStartIndex(moduleStart);
                return "Please, your " + mask.itemElement.nameItem + ".";
            }
            else
            {
                return "I can't sell you " + mask.itemElement.nameItem + ".\n(Inventory is full)";
            }
        }
        else
        {
            return "You don't have enough money for a " + mask.itemElement.nameItem + ".";
        }
        //return "";
    }
}
