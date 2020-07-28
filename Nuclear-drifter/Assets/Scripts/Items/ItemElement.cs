using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemElement : MonoBehaviour
{
    public bool saveItem = false;
    public int indexItem = 0;
    public Slot item;
    public float distance = 1.1f;
    private GUIScript gUI;
    private Inventory inv;
    private PlayerClickMove player;
    private InventoryBox invBox;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        inv = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerClickMove>();
        invBox = FindObjectOfType<InventoryBox>();
        if(saveItem)
        {
            item = invBox.GetFreeItem(indexItem);
            if(item != null)
            {
                if (item.amountItem <= 0) Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void ShowText()
    {
        if (item != null)
        {
            if(item.itemElement != null && item.amountItem > 0)gUI.AddText("Item: " + item.itemElement.nameItem);
        }
    }

    public void Use()
    {
        if (item != null)
        {
            if (item.itemElement != null && item.amountItem > 0) {

                if (player.ObjIsNearPlayer(transform.position, distance))
                {
                    bool result = inv.Add(item);
                    if (result)
                    {
                        item.amountItem = 0;
                        Destroy(gameObject);
                    }
                }
                else gUI.AddText("This is too far");
            }
        }
    }

}
