using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : Job
{
    private Health hp;
    private Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        hp = FindObjectOfType<Health>();
        inv = FindObjectOfType<Inventory>();
    }

    public override string Work(int opt)
    {
        Debug.Log("Doctor");
        if (!hp.isFullHealth())
        {
            int cost = 0;
            Slot money = inv.FindItem(300);
            if (money != null)
            {
                cost = Mathf.RoundToInt(money.amountItem * 0.1f);
                if (cost > 0)
                {
                    Slot tempMoney = new Slot(money.itemElement, cost, 0);
                    inv.RemoveFew(tempMoney);
                }
            }
            hp.SetFullHP();
            return "Please.You are now fully healthy. (Cost: $" + cost + ").";
        }
        else
        {
            return "You don't overdo it. It's just a scratch. Nothing bad will happen to you.";
        }
    }
}
