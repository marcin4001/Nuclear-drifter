using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReplace : MonoBehaviour
{
    public Item itemReplace;
    public Slot currentItem;
    public bool isReplace = false;
    public int indexItem;
    private GUIScript gUI;
    private SpriteRenderer render;
    private Inventory inv;
    private PlayerClickMove player;
    private InventoryBox box;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        render = GetComponent<SpriteRenderer>();
        inv = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerClickMove>();
        box = FindObjectOfType<InventoryBox>();
        currentItem = box.GetFreeItem(indexItem);
        render.sprite = currentItem.itemElement.image;
        if (itemReplace == currentItem.itemElement)
        {
            isReplace = true;
        }
    }

    public void ShowText()
    {
        if (currentItem != null)
        {
            if (currentItem.itemElement != null && currentItem.amountItem > 0) gUI.AddText("Item: " + currentItem.itemElement.nameItem);
        }
    }

    public void Use()
    {
        if (isReplace)
        {
            gUI.AddText("The item has been");
            gUI.AddText("changed!");
        }
        else
        {
            Slot _item = inv.FindItem(itemReplace.idItem);
            if(_item == null)
            {
                gUI.AddText("You do not have:");
                gUI.AddText(itemReplace.nameItem);
            }
            else
            {
                if (player.ObjIsNearPlayer(transform.position, 1.1f))
                {
                    inv.RemoveAllUni(_item);
                    inv.Add(currentItem);
                    currentItem.itemElement = itemReplace;
                    currentItem.id = itemReplace.idItem;
                    isReplace = true;
                    render.sprite = itemReplace.image;
                }
                else
                {
                    gUI.AddText(currentItem.itemElement.nameItem + "is too far!");
                }
            }
        }

    }
}
