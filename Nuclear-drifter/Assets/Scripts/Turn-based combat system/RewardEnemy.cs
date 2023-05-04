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
    public int chance = 100;
    public bool giveRandomItem = false;
    private ItemDB itemDB;

    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        gUI = FindObjectOfType<GUIScript>();
        itemDB = FindObjectOfType<ItemDB>();
    }

    public void GiveItem()
    {
        int random = Random.Range(0, 100);
        Debug.Log(random);
        if (random > chance)
            return;
        if(giveRandomItem)
        {
            GiveRandomItem();
            return;
        }
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

    public void GiveRandomItem()
    {
        Slot randomItem = itemDB.GetRandomItem();
        if (inv.IsFull() && !inv.FindItemB(randomItem.itemElement.idItem))
        {
            GameObject obj = Instantiate(itemPref, transform.position, Quaternion.identity);
            ItemElement itemEl = obj.GetComponent<ItemElement>();
            if (itemEl != null) itemEl.item = randomItem;
            SpriteRenderer spriteItem = obj.GetComponent<SpriteRenderer>();
            if (spriteItem != null)
                spriteItem.sprite = randomItem.itemElement.image;
        }
        else
        {
            if (randomItem.itemElement.idItem != moneyId) gUI.AddText("You got " + randomItem.itemElement.nameItem + " x" + randomItem.amountItem);
            else gUI.AddText("You got $" + randomItem.amountItem);
            inv.Add(randomItem);
        }
    }
}
