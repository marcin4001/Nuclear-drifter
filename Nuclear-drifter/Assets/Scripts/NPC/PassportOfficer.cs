using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassportOfficer : Job
{
    public int cost = 500;
    public int idMission = 0;
    private Inventory inv;
    private MissionNPC mission;
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        mission = GetComponent<MissionNPC>();
        if (mission == null) Destroy(this);
    }

    public override string Work(int opt)
    {
        int currentCost = 0;
        Slot money = inv.FindItem(300);
        if(money != null)
        {
            currentCost = money.amountItem;
        }

        if(currentCost >= cost)
        {
            if(cost > 0)
            {
                Slot tempMoney = new Slot(money.itemElement, cost, 0);
                inv.RemoveFew(tempMoney);
            }
            mission.CompleteMissionNoCond(idMission);
            return "Okay. Your passport is in my wardrobe. Thank you for using our services. We recommend ourselves for the future.";
        }
        else
        {
            return "I'm sorry. You don't have enough dollars to buy a passport.";
        }
        
    }
}
