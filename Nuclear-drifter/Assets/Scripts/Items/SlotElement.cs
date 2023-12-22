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
    public bool isBoxSlot = false;
    private TypeScene typeSc;
    private EqChestController eqChest;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        if (!isBoxSlot)
        {
            if (itemSlot.itemElement == null || itemSlot.amountItem <= 0)
            {
                ClearSlot();
            }
            else
            {
                imgSlot.enabled = true;
                imgSlot.sprite = itemSlot.itemElement.image;
                labelSlot.text = itemSlot.GetAmount();
            }
        }
        inv = GetComponentInParent<Inventory>();
        typeSc = FindObjectOfType<TypeScene>();
        eqChest = FindObjectOfType<EqChestController>();
    }

    public void ClickSlot()
    {
        if(typeSc == null) typeSc = FindObjectOfType<TypeScene>();
        if (itemSlot != null && !typeSc.inMenu)
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
                    if (typeSc.inBox == 0)
                    {
                        if (itemSlot.itemElement.GetItemType() == ItemType.Food)
                        {
                            if(typeSc.radZone && itemSlot.itemElement.idItem == 211)
                            {
                                gUI.AddText("It won't help me now");
                                return;
                            }
                            FoodItem food = (FoodItem)itemSlot.itemElement;
                            if(!gUI.GetCombatState() && food.isDrag)
                            {
                                gUI.AddText("I'm not fighting anyone");
                                return;
                            }
                            food.SetHP(gUI.playerHealth);
                            itemSlot.itemElement.Use();
                            inv.RemoveOne(itemSlot);
                            if (food.isDrag) UseDrag();
                            if (typeSc.combatState) EatCombatAction();

                        }
                        else if (itemSlot.itemElement.GetItemType() == ItemType.Weapon)
                        {
                            if (!gUI.GetCombatState()) gUI.AddText("I'm not fighting anyone");
                            else itemSlot.itemElement.Use();
                        }
                        else if(itemSlot.itemElement.GetItemType() == ItemType.Document)
                        {
                            if (!gUI.GetCombatState()) itemSlot.itemElement.Use();
                            else gUI.AddText("You can't read now");
                        }
                        else if (itemSlot.itemElement.GetItemType() == ItemType.Misc)
                        {
                            MiscItem misc = (MiscItem)itemSlot.itemElement;
                            if (gUI.GetCombatState() && misc.isBackpack)
                            {
                                gUI.AddText("You can't use it now");
                            }
                            else
                            {
                                misc.SetHP(gUI.playerHealth);
                                itemSlot.itemElement.Use();
                                if(misc.isCigarette)
                                    inv.RemoveOne(itemSlot);
                            }
                        }
                    }
                    else if (typeSc.inBox == 1)
                    {
                        if(isBoxSlot)
                        {
                            eqChest.TakeOutOne(itemSlot);
                        }
                        else
                        {
                            eqChest.PutOnOne(itemSlot);
                        }
                    }
                    else
                    {
                        TradeSystem trade = FindObjectOfType<TradeSystem>();
                        trade.AddOne(itemSlot);
                    }
                }
                if (gUI.GetInvMode() == inv_mode.remove)
                {
                    if (typeSc.inBox == 0)
                    {
                        if (!typeSc.combatState) inv.RemoveAll(itemSlot);
                        else gUI.AddText("You can't do it now!");
                    }
                    else if (typeSc.inBox == 1)
                    {
                        if (isBoxSlot)
                        {
                            eqChest.TakeOutAll(itemSlot);
                        }
                        else
                        {
                            eqChest.PutOnAll(itemSlot);
                        }
                    }
                    else
                    {
                        TradeSystem trade = FindObjectOfType<TradeSystem>();
                        trade.AddAll(itemSlot);
                    }
                }
            }
        }
    }

    public void EatCombatAction()
    {
        CombatSystem combat = FindObjectOfType<CombatSystem>();
        if(combat != null)
        {
            combat.UseAP();
        }
    }

    public void UseDrag()
    {
        CombatSystem combat = FindObjectOfType<CombatSystem>();
        if (combat != null)
        {
            combat.UseDrag();
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
