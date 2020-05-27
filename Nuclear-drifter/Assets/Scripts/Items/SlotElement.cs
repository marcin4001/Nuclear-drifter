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
    }

    public void ClickSlot()
    {
        if (itemSlot != null)
        {
            if (itemSlot.itemElement != null && itemSlot.amountItem > 0)
            {
                itemSlot.itemElement.Look();
                if (itemSlot.itemElement.GetItemType() == ItemType.Weapon)
                {
                    WeaponItem weapon = (WeaponItem)itemSlot.itemElement;
                    if (!weapon.isMeleeWeapon) gUI.AddText("Ammo amount: " + itemSlot.ammo);
                }
                gUI.AddText("Amount: " + itemSlot.amountItem);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && itemSlot != null)
        {
            if (itemSlot.itemElement != null && itemSlot.amountItem > 0)
            {

                imgSlot.enabled = true;
                imgSlot.overrideSprite = itemSlot.itemElement.image;
                labelSlot.text = itemSlot.GetAmount();
            }
        }
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
