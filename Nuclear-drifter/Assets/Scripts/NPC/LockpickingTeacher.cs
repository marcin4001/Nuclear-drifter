using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockpickingTeacher : Job
{
    public Slot lockpickItem;
    public Slot currency;
    public int costLearning = 100;
    private Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
    }

    public override string Work(int opt)
    {
        if(opt == 0)
        {
            Slot moneyPlayer = inv.FindItem(currency.itemElement.idItem);
            if (moneyPlayer == null)
                return "I cannot teach you lockpicking. You don't have $100.";
            if(moneyPlayer.amountItem >= costLearning)
            {
                currency.amountItem = costLearning;
                inv.RemoveFew(currency);
                return "";
            }
            else
                return "I cannot teach you lockpicking. You don't have $100.";
        }
        if(opt == 1)
        {
            if (lockpickItem == null) return "I have no such item. I'm sorry.";
            if (lockpickItem.itemElement == null) return "I have no such item. I'm sorry.";
            if (inv.CanBuy(lockpickItem))
            {
                if (inv.Add(lockpickItem))
                {
                    currency.amountItem = lockpickItem.amountItem * lockpickItem.itemElement.value;
                    inv.RemoveFew(currency);
                    return "Please, your " + lockpickItem.itemElement.nameItem + ".";
                }
                else
                {
                    return "I can't sell you " + lockpickItem.itemElement.nameItem + ".\n(Inventory is full)";
                }
            }
            else
            {
                return "You don't have enough money for a " + lockpickItem.itemElement.nameItem + ".";
            }
        }
        return "";
    }
}
