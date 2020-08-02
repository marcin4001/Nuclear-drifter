using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : MonoBehaviour
{
    public Slot item;
    private Inventory inv;
    private PlayerClickMove move;
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        move = FindObjectOfType<PlayerClickMove>();
    }

    // Update is called once per frame
    public void Use()
    {
        if(item != null)
        {
            if (item.amountItem > 0 && move.ObjIsNearPlayer(transform.parent.position, 1.5f)) 
            {
                inv.AddOne(item);
                item.amountItem -= 1;
            }
        }
    }
}
