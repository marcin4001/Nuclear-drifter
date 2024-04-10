using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public bool isEmpty = false;
    private Inventory inv;
    private GUIScript gUI;
    private BagController bagController;
    private PlayerClickMove move;
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        gUI = FindObjectOfType<GUIScript>();
        bagController = FindObjectOfType<BagController>();
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
                gUI.AddText("Bag is too far");
            }
        }
        else
        {
            gUI.AddText("Inventory is full");
        }
    }

    private void GiveItem()
    {
        if(!isEmpty && !PropertyPlayer.property.gotPicture)
        {
            Slot item = bagController.GetItemSlot();
            inv.Add(item);
            gUI.AddText("Found: " + item.itemElement.nameItem + " x" + item.amountItem);
            PropertyPlayer.property.SetGotPicture();
        }
        else
        {
            gUI.AddText("Bag is empty");
        }
    }
}
