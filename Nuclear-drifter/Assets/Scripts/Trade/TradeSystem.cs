using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TradeSystem : MonoBehaviour
{
    public List<Slot> slots;
    public TradeSlot[] tradeSlots;
    public Slot money;

    public SellSlot sellSlot;
    public bool active = false;
    public GameObject tradeGO;
    private float sellPercent = 0.75f;
    private GUIScript gUI;
    private PlayerClickMove move;
    private TypeScene typeSc;
    private Inventory inv;
    private Offer offer;
    private SoundsTrigger sound;
    private SoundUse soundUse;

    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        move = FindObjectOfType<PlayerClickMove>();
        typeSc = FindObjectOfType<TypeScene>();
        inv = FindObjectOfType<Inventory>();
        offer = FindObjectOfType<Offer>();
        soundUse = FindObjectOfType<SoundUse>();
        if (offer != null) slots = offer.slots;
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
        tradeGO.SetActive(active);
        sound = FindObjectOfType<SoundsTrigger>();
    }

    public void Close()
    {
        active = false;
        tradeGO.SetActive(active);
        gUI.move.active = !active;
        gUI.blockGUI = active;
        gUI.DeactiveBtn(!active);
        RemoveAll();
        typeSc.inBox = 0;
        sound.PlayClickButton();
    }

    public void CloseCanvas()
    {
        active = false;
        tradeGO.SetActive(active);
        RemoveAll();
        typeSc.inBox = 0;
    }

    public void Open(bool useAlternative = false)
    {
        if(offer != null)
        {
            if(offer.isAlternative)
            {
                if (useAlternative)
                {
                    slots = offer.alternativeSlot;
                }
                else
                {
                    slots = offer.slots;
                }
                SetItems();
            }
        }
        active = true;
        tradeGO.SetActive(active);
        gUI.move.active = !active;
        gUI.blockGUI = active;
        gUI.DeactiveBtn(!active);
        typeSc.inBox = 2;
    }

    public void SetItems()
    {
        if(slots == null)
        {
            foreach(TradeSlot slot in tradeSlots)
                slot.ClearSlot();
            return;
        }
        if (slots.Count > 0)
        {
            for (int i = 0; i < tradeSlots.Length; i++)
            {
                if (i < slots.Count)
                {
                    tradeSlots[i].SetSlot(slots[i]);
                }
                else
                {
                    tradeSlots[i].ClearSlot();
                }
            }
        }
        else
        {
            foreach (TradeSlot slot in tradeSlots)
                slot.ClearSlot();
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
            soundUse.PlayPickUp();
        }
        else if(slot.itemElement == sellSlot.itemSlot.itemElement)
        {
            int amount = sellSlot.itemSlot.amountItem + 1;
            Slot temp = new Slot(slot.itemElement, amount, slot.ammo);
            sellSlot.SetSlot(temp);
            inv.RemoveOne(slot);
            soundUse.PlayPickUp();
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
            soundUse.PlayPickUp();
        }
        else if (slot.itemElement == sellSlot.itemSlot.itemElement)
        {
            int amount = sellSlot.itemSlot.amountItem + slot.amountItem;
            Slot temp = new Slot(slot.itemElement, amount, slot.ammo);
            sellSlot.SetSlot(temp);
            inv.RemoveAllUni(slot);
            soundUse.PlayPickUp();
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
            soundUse.PlayPickUp();
        }
    }

    public void RemoveAll()
    {
        if (!SellSlotIsEmpty())
        {
            inv.Add(sellSlot.itemSlot);
            sellSlot.ClearSlot();
            soundUse.PlayPickUp();
        }
    }
    
    public void Sell()
    {
        if (!SellSlotIsEmpty())
        {
            if (!sellSlot.itemSlot.itemElement.noSell)
            {
                if (sellSlot.itemSlot.itemElement.idItem != 300)
                {
                    money.amountItem = Mathf.RoundToInt((sellSlot.itemSlot.itemElement.value * sellSlot.itemSlot.amountItem) * sellPercent);
                    int percentText = (int)(sellPercent * 100f);
                    gUI.AddText("You got $" + money.amountItem + " (" + percentText + "% value)");
                    if (money.amountItem > 0) inv.Add(money);
                    sellSlot.ClearSlot();
                }
                else
                {
                    money.amountItem = sellSlot.itemSlot.itemElement.value * sellSlot.itemSlot.amountItem;
                    gUI.AddText("You got $" + money.amountItem);
                    if (money.amountItem > 0) inv.Add(money);
                    sellSlot.ClearSlot();
                }
                soundUse.PlayCash();
            }
            else
            {
                gUI.AddText("It cannot be sold!");
                sound.PlayClickButton();
            } 
        }
        else
        {
            sound.PlayClickButton();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.T))
        //{
        //    if (active) Close();
        //    else Open();
        //}
    }
}
