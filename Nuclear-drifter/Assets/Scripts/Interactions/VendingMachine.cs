using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public Slot[] offer;
    public Slot cost;
    private Inventory inv;
    private GUIScript gUI;
    private PlayerClickMove player;
    private Slot money;
    private SoundsTrigger sounds;


    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        gUI = FindObjectOfType<GUIScript>();
        player = FindObjectOfType<PlayerClickMove>();
        sounds = FindObjectOfType<SoundsTrigger>();
    }

    public void ShowText()
    {
        gUI.AddText("This is the vending");
        gUI.AddText("machine");
    }

    // Update is called once per frame
    public void Use()
    {
        money = inv.FindItem(cost.itemElement.idItem);
        if (money != null)
        {
            if (money.amountItem >= cost.amountItem)
            {
                PickItem();
            }
            else
            {
                gUI.AddText("You don't have enough money!");
            }
        }
        else
        {
            gUI.AddText("You don't have enough money!");
        }
    }

    private void PickItem()
    {
        int randomItem = Random.Range(0, offer.Length);
        
        if (!inv.IsFull())
        {
            SellItem(randomItem);
        }
        else
        {
            Slot item = inv.FindItem(offer[randomItem].itemElement.idItem);
            if (item != null)
            {
                SellItem(randomItem);
            }
            else
            {
                gUI.AddText("Inventory is full");
            }
        }
    }

    private void SellItem(int random_item)
    {
        if (player.ObjIsNearPlayer(transform.position, 2.1f))
        {
            inv.RemoveFew(cost);
            inv.Add(offer[random_item]);
            sounds.UseVendingMachine();
        }
        else
        {
            gUI.AddText("Vending Machine is too");
            gUI.AddText("far");
        }
    }
}
