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
                        if (!weapon.isMeleeWeapon) gUI.AddText("Ammo amount: " + itemSlot.ammo);
                    }
                    gUI.AddText("Amount: " + itemSlot.amountItem);
                }
                if (gUI.GetInvMode() == inv_mode.use)
                {
                    if (trade.SellSlotIsEmpty())
                    {
                        Slot temp = new Slot(itemSlot.itemElement, 1, itemSlot.ammo);
                        if (inv.CanBuy(temp))
                        {
                            if (inv.Add(temp))
                            {
                                trade.money.amountItem = temp.itemElement.value;
                                inv.RemoveFew(trade.money);
                            }
                            else
                            {
                                gUI.AddText("Inventory is full");
                            }
                        }
                        else
                        {
                            gUI.AddText("You don't have");
                            gUI.AddText("enough money");
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
                        if (inv.CanBuy(itemSlot))
                        {
                            if (inv.Add(itemSlot))
                            {
                                trade.money.amountItem = itemSlot.itemElement.value * itemSlot.amountItem;
                                inv.RemoveFew(trade.money);
                            }
                            else
                            {
                                gUI.AddText("Inventory is full");
                            }
                        }
                        else
                        {
                            gUI.AddText("You don't have");
                            gUI.AddText("enough money");
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
