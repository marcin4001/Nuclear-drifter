using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public Slot[] offer;
    public Slot cost;
    public int idDevice;
    public int maxUses;
    public DeviceElement device;
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
        device = DeviceList.global.GetDevice(idDevice);
    }

    public void ShowText()
    {
        gUI.AddText("This is the vending");
        gUI.AddText("machine");
    }

    // Update is called once per frame
    public void Use()
    {
        if (maxUses <= device.uses)
        {
            gUI.AddText("The vending machine is");
            gUI.AddText("empty");
            return;
        }

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
            gUI.AddText("You got " + offer[random_item].amountItem + "x " + offer[random_item].itemElement.nameItem);
            sounds.UseVendingMachine();
            if (device != null)
                device.uses += 1;
        }
        else
        {
            gUI.AddText("Vending Machine is too");
            gUI.AddText("far");
        }
    }
}
