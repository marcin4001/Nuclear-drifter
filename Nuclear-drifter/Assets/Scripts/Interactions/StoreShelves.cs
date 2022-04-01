using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreShelves : MonoBehaviour
{
    public int indexEq;
    public List<Slot> slots;
    public string nameObj;
    public float minDistance;
    public Transform center;
    public bool are = true;
    private PlayerClickMove player;
    private GUIScript gUI;
    private Inventory inv;
    private InventoryBox inventoryBox;

    void Start()
    {
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        inventoryBox = FindObjectOfType<InventoryBox>();
        inv = FindObjectOfType<Inventory>();
    }

    public void Use()
    {
        if(indexEq < 0)
        {
            if(are)
                gUI.AddText("The " + nameObj + " are empty");
            else
                gUI.AddText("The " + nameObj + " is empty");
            return;
        }
        slots = inventoryBox.boxes[indexEq].eqSlots;
        if(slots.Count == 0)
        {
            if (are)
                gUI.AddText("The " + nameObj + " are empty");
            else
                gUI.AddText("The " + nameObj + " is empty");
            return;
        }
        Slot slot = slots[0];
        if (player.ObjIsNearPlayer(center.position, minDistance))
        {
            bool added = inv.AddOne(slot);
            if (added)
            {
                if(slot.itemElement.idItem != 300)
                    gUI.AddText("You found " + slot.itemElement.nameItem);
                else
                    gUI.AddText("You found $1");
                if (slot.amountItem > 1)
                {
                    slot.amountItem--;
                }
                else
                {
                    slots.Remove(slot);
                }
            }
        }
        else
        {
            if(are)
                gUI.AddText("The " + nameObj + " are too far");
            else
                gUI.AddText("The " + nameObj + " is too far");
        }
    }
}
