using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellSlot : MonoBehaviour
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
        ClearSlot();
    }

    public void ClickSlot()
    {
        if (itemSlot != null)
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
                    trade.RemoveOne();
                }
                if (gUI.GetInvMode() == inv_mode.remove)
                {
                    trade.RemoveAll();
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
