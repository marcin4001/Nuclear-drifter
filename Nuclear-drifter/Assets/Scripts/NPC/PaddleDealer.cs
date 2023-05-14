using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleDealer : Job
{
    public Slot paddle;
    public Slot currency;
    private Inventory inv;

    private void Start()
    {
        inv = FindObjectOfType<Inventory>();
        if (paddle == null) Destroy(this);
    }

    public override string Work(int opt)
    {
        if (paddle == null) return "I have no such item. I'm sorry.";
        if (paddle.itemElement == null) return "I have no such item. I'm sorry.";

        if (inv.CanBuy(paddle))
        {
            if (inv.Add(paddle))
            {
                currency.amountItem = paddle.amountItem * paddle.itemElement.value;
                inv.RemoveFew(currency);
                return "Great, here's your paddle. Have a good time boating!";
            }
            else
            {
                return "I can't sell you " + paddle.itemElement.nameItem + ".\n(Inventory is full)";
            }
        }
        else
        {
            return "You don't have enough money for a " + paddle.itemElement.nameItem + ".";
        }
        //return "";
    }
}
