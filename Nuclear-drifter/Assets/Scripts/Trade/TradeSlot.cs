using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour
{
    public Slot itemSlot;
    public Text labelSlot;
    public Image imgSlot;
    private GUIScript gUI;
    private Inventory inv;
    private TradeSystem trade;
    private TypeScene typeSc;

    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        inv = FindObjectOfType<Inventory>();
        trade = FindObjectOfType<TradeSystem>();
        typeSc = FindObjectOfType<TypeScene>();
    }

    public void ClickSlot()
    {
       if(itemSlot != null)
        {
            if (itemSlot.itemElement != null && itemSlot.amountItem > 0)
            {
                if (gUI.GetInvMode() == inv_mode.look)
                {
                    itemSlot.itemElement.Look();
                    if (itemSlot.itemElement.GetItemType() == ItemType.Weapon)
                    {
                        WeaponItem weapon = (WeaponItem)itemSlot.itemElement;
                        if (!weapon.isMeleeWeapon)
                        {
                            int cost = Mathf.CeilToInt(itemSlot.itemElement.value * 0.01f) - 1;
                            if (cost <= 0) cost = 1;
                            cost *= itemSlot.ammo;
                            gUI.AddText("Ammo value(10): " + cost + "$");
                            gUI.AddText("Ammo amount: " + itemSlot.ammo);
                        }
                    }
                    gUI.AddText("Amount: " + itemSlot.amountItem);
                }
                if (gUI.GetInvMode() == inv_mode.use)
                {
                    if (trade.SellSlotIsEmpty())
                    {
                        if (itemSlot.isGun() && inv.FindItemB(itemSlot.itemElement.idItem))
                        {
                            BuyAmmo();
                        }
                        else
                        {
                            BuyOne();
                        }
                    }
                    else
                    {
                        gUI.AddText("Sell ​​Slot is not empty");
                    }
                }
                
                if(gUI.GetInvMode() == inv_mode.remove)
                {
                    if (trade.SellSlotIsEmpty())
                    {
                        if (itemSlot.isGun() && inv.FindItemB(itemSlot.itemElement.idItem))
                        {
                            BuyAmmo();
                        }
                        else
                        {
                            BuyAll();
                        }
                    }
                    else
                    {
                        gUI.AddText("Sell ​​Slot is not empty");
                    }
                }
            }
        }
    }

    private void BuyOne()
    {
        Slot temp = new Slot(itemSlot.itemElement, 1, itemSlot.ammo);
        if (inv.CanBuy(temp))
        {
            if (inv.Add(temp))
            {
                trade.money.amountItem = temp.itemElement.value;
                gUI.AddText("You spent $" + trade.money.amountItem);
                inv.RemoveFew(trade.money);
            }
            else
            {
                gUI.AddText("Inventory is full");
            }
        }
        else
        {
            gUI.AddText("You don't have enough");
            gUI.AddText("money");
        }
    }

    private void BuyAll()
    {
        if (inv.CanBuy(itemSlot))
        {
            if (inv.Add(itemSlot))
            {
                trade.money.amountItem = itemSlot.itemElement.value * itemSlot.amountItem;
                gUI.AddText("You spent $" + trade.money.amountItem);
                inv.RemoveFew(trade.money);
            }
            else
            {
                gUI.AddText("Inventory is full");
            }
        }
        else
        {
            gUI.AddText("You don't have enough");
            gUI.AddText("money");
        }
    }

    private void BuyAmmo()
    {
        int cost = Mathf.CeilToInt(itemSlot.itemElement.value * 0.01f) - 1;
        if (cost <= 0) cost = 1;
        //Debug.Log(cost);
        cost *= itemSlot.ammo;
        //Debug.Log(cost);
        if (inv.HaveMoney(cost))
        {
            if (inv.Add(itemSlot))
            {
                trade.money.amountItem = cost;
                gUI.AddText("You spent $" + trade.money.amountItem);
                inv.RemoveFew(trade.money);
            }
            else
            {
                gUI.AddText("Inventory is full");
            }
        }
        else
        {
            gUI.AddText("You don't have enough");
            gUI.AddText("money");
        }
    }

    public void SetSlotStart(Slot newSlot)
    {
        itemSlot = newSlot;
        imgSlot.enabled = true;
        imgSlot.sprite = itemSlot.itemElement.image;
        labelSlot.text = itemSlot.GetAmount();
    }


    public void SetSlot(Slot newSlot)
    {
        itemSlot = newSlot;
        imgSlot.enabled = true;
        imgSlot.overrideSprite = itemSlot.itemElement.image;
        labelSlot.text = itemSlot.GetAmount();
    }

    public void ClearSlot()
    {
        itemSlot = null;
        imgSlot.enabled = false;
        labelSlot.text = "";
    }
}
