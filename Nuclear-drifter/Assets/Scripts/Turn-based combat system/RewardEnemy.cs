using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEnemy : MonoBehaviour
{
    public Slot item;
    public GameObject itemPref;
    private Inventory inv;
    private GUIScript gUI;
    
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        gUI = FindObjectOfType<GUIScript>();
    }

    public void GiveItem()
    {
        if(!inv.IsFull())
        {
            gUI.AddText("You got " + item.itemElement.name + " x" + item.amountItem);
            inv.Add(item);
        }
        else
        {
            GameObject obj = Instantiate(itemPref, transform.position, Quaternion.identity);
            ItemElement itemEl = obj.GetComponent<ItemElement>();
            if (itemEl != null) itemEl.item = item;
        }
    }
}
