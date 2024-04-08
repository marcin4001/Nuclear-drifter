using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public bool isEmpty = true;
    private Inventory inv;
    private GUIScript gUI;
    private BarrelController controller;
    private PlayerClickMove move;
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        gUI = FindObjectOfType<GUIScript>();
        controller = FindObjectOfType<BarrelController>();
        move = FindObjectOfType<PlayerClickMove>();
    }

    public void Use()
    {
        if(!inv.IsFull())
        {
            if (move.ObjIsNearPlayer(transform.position, 1.1f))
            {
                GiveItem();
            }
            else
            {
                gUI.AddText("Barrel is too far");
            }
        }
        else
        {
            gUI.AddText("Inventory is full");
        }
    }

    private void GiveItem()
    {
        if(!isEmpty && !PropertyPlayer.property.gotMachete)
        {
            Slot item = controller.GetItem();
            inv.Add(item);
            PropertyPlayer.property.SetGotMachete();
            gUI.AddText("Found: " + item.itemElement.name + " x" + item.amountItem);
        }
        else
        {
            gUI.AddText("Barrel is empty");
        }
    }
}
