using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barman : Job
{
    public Slot[] items;
    public Slot currency;
    private Inventory inv;
    private void Start()
    {
        inv = FindObjectOfType<Inventory>();
        if (items == null) Destroy(this);
        if (items.Length == 0) Destroy(this);
    }

    public override string Work(int opt)
    {
        if (opt >= items.Length || opt < 0) return "I have no such item. I'm sorry.";
        Slot _item = items[opt];
        if(_item == null) return "I have no such item. I'm sorry.";
        if (_item.itemElement == null) return "I have no such item. I'm sorry.";
        if(inv.CanBuy(_item))
        {
            if(inv.Add(_item))
            {
                currency.amountItem = _item.amountItem * _item.itemElement.value;
                inv.RemoveFew(currency);
                return "Please, your " + _item.itemElement.nameItem + ".";
            }
            else
            {
                return "I can't sell you " + _item.itemElement.nameItem + ".\n(Inventory is full)";
            }
        }
        else
        {
            return "You don't have enough money for a " + _item.itemElement.nameItem + ".";
        }
        //return "";
    }

}
