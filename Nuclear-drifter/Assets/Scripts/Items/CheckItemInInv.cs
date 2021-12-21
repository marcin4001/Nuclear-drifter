using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItemInInv : MonoBehaviour
{
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        Inventory inv = FindObjectOfType<Inventory>();
        if (inv == null || item == null)
            Destroy(gameObject);
        if(inv.FindItemB(item.idItem))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

}
