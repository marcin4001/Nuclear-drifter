using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEnemy : MonoBehaviour
{
    public Slot item;
    public GameObject itemPref;
    private int moneyId = 300;
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
        if(inv.IsFull() && !inv.FindItemB(item.itemElement.idItem))
        {
            GameObject obj = Instantiate(itemPref, transform.position, Quaternion.identity);
            ItemElement itemEl = obj.GetComponent<ItemElement>();
            if (itemEl != null) itemEl.item = item;
        }
        else
        {
            if (item.itemElement.idItem != moneyId) gUI.AddText("You got " + item.itemElement.nameItem + " x" + item.amountItem);
            else gUI.AddText("You got $" + item.amountItem);
            inv.Add(item);
        }
        Hunt hunt = GetComponent<Hunt>();
        if (hunt != null)
            hunt.GiveTrophy();
    }
}
