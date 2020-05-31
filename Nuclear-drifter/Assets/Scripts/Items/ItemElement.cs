using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemElement : MonoBehaviour
{
    public Slot item;
    private GUIScript gUI;
    private Inventory inv;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        inv = FindObjectOfType<Inventory>();
        player = GameObject.FindWithTag("Player").transform;
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
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance <= 1.0f)
                {
                    bool result = inv.Add(item);
                    if (result)
                    {
                        Destroy(gameObject);
                    }
                }
                else gUI.AddText("This is too far");
            }
        }
    }

}
