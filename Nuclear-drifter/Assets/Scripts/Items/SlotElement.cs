using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotElement : MonoBehaviour
{
    public Slot itemSlot;
    public Text labelSlot;
    public Image imgSlot;
    private GUIScript gUI;
    private Inventory inv;

    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        if(itemSlot.itemElement == null || itemSlot.amountItem <= 0)
        {
            ClearSlot();
        }
        else
        {
            imgSlot.enabled = true;
            imgSlot.sprite = itemSlot.itemElement.image;
            labelSlot.text = itemSlot.GetAmount();
        }
        inv = GetComponentInParent<Inventory>();
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
                if(gUI.GetInvMode() == inv_mode.use)
                {

                    if (itemSlot.itemElement.GetItemType() == ItemType.Food)
                    {
                        FoodItem food = (FoodItem)itemSlot.itemElement;
                        food.SetHP(gUI.playerHealth);
                        itemSlot.itemElement.Use();
                        inv.RemoveOne(itemSlot);
                    }
                    else if (itemSlot.itemElement.GetItemType() == ItemType.Weapon)
                    {
                        if (!gUI.GetCombatState()) gUI.AddText("I'm not fighting anyone");
                    }
                }
                if (gUI.GetInvMode() == inv_mode.remove)
                {
                    inv.RemoveAll(itemSlot);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && itemSlot != null)
        {
            if (itemSlot.itemElement != null && itemSlot.amountItem > 0)
            {

                imgSlot.enabled = true;
                imgSlot.overrideSprite = itemSlot.itemElement.image;
                labelSlot.text = itemSlot.GetAmount();
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
