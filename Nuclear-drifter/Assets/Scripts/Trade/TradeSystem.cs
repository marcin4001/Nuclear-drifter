using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TradeSystem : MonoBehaviour
{
    public List<Slot> slots;
    public TradeSlot[] tradeSlots;
    public Slot money;

    public SellSlot sellSlot;

    private GUIScript gUI;
    private PlayerClickMove move;
    private TypeScene typeSc;
    private Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        move = FindObjectOfType<PlayerClickMove>();
        typeSc = FindObjectOfType<TypeScene>();
        inv = FindObjectOfType<Inventory>();
        if (slots == null) slots = new List<Slot>();
        else
        {
            if (slots.Count > 0)
            {
                for (int i = 0; i < tradeSlots.Length; i++)
                {
                    if (i < slots.Count)
                    {
                        tradeSlots[i].SetSlotStart(slots[i]);
                    }
                    else
                    {
                        tradeSlots[i].ClearSlot();
                    }
                }
            }
        }
    }

    public bool SellSlotIsEmpty()
    {
        if(sellSlot.itemSlot != null)
        {
            if(sellSlot.itemSlot.itemElement == null || sellSlot.itemSlot.amountItem <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public void AddOne(Slot slot)
    {
        if(SellSlotIsEmpty())
        {
            Slot temp = new Slot(slot.itemElement, 1, slot.ammo);
            sellSlot.SetSlot(temp);
            inv.RemoveOne(slot);
        }
        else if(slot.itemElement == sellSlot.itemSlot.itemElement)
        {
            int amount = sellSlot.itemSlot.amountItem + 1;
            Slot temp = new Slot(slot.itemElement, amount, slot.ammo);
            sellSlot.SetSlot(temp);
            inv.RemoveOne(slot);
        }
        else
        {
            gUI.AddText("Sell slot is not empty");
        }
    }

    public void AddAll(Slot slot)
    {
        if (SellSlotIsEmpty())
        {
            Slot temp = new Slot(slot.itemElement, slot.amountItem, slot.ammo);
            sellSlot.SetSlot(temp);
            inv.RemoveAllUni(slot);
        }
        else if (slot.itemElement == sellSlot.itemSlot.itemElement)
        {
            int amount = sellSlot.itemSlot.amountItem + slot.amountItem;
            Slot temp = new Slot(slot.itemElement, amount, slot.ammo);
            sellSlot.SetSlot(temp);
            inv.RemoveAllUni(slot);
        }
        else
        {
            gUI.AddText("Sell slot is not empty");
        }
    }

    public void RemoveOne()
    {
        if(!SellSlotIsEmpty())
        {
            if(sellSlot.itemSlot.amountItem > 1)
            {
                int amount = sellSlot.itemSlot.amountItem - 1;
                Slot temp = new Slot(sellSlot.itemSlot.itemElement, amount, sellSlot.itemSlot.ammo);
                sellSlot.SetSlot(temp);
                inv.Add(new Slot(sellSlot.itemSlot.itemElement, 1, sellSlot.itemSlot.ammo));
            }
            else
            {
                inv.Add(new Slot(sellSlot.itemSlot.itemElement, 1, sellSlot.itemSlot.ammo));
                sellSlot.ClearSlot();
            }
        }
    }

    public void RemoveAll()
    {
        if (!SellSlotIsEmpty())
        {
            inv.Add(sellSlot.itemSlot);
            sellSlot.ClearSlot();
        }
    }
    
    public void Sell()
    {
        if (!SellSlotIsEmpty())
        {
            money.amountItem = sellSlot.itemSlot.itemElement.value * sellSlot.itemSlot.amountItem;
            inv.Add(money);
            sellSlot.ClearSlot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
