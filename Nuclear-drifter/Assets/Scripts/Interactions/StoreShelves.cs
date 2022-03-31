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
            gUI.AddText("The " + nameObj + " are empty");
            return;
        }
        slots = inventoryBox.boxes[indexEq].eqSlots;
        if(slots.Count == 0)
        {
            gUI.AddText("The " + nameObj + " are empty");
            return;
        }
        Slot slot = slots[0];
        if (player.ObjIsNearPlayer(center.position, minDistance))
        {
            bool added = inv.AddOne(slot);
            if (added)
            {
                gUI.AddText("You found " + slot.itemElement.nameItem);
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
        else gUI.AddText("The " + nameObj + " is too far");
    }
}
