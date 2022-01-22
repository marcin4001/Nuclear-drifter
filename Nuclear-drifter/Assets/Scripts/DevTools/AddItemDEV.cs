using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemDEV : MonoBehaviour
{
    public Slot item;
    public KeyCode keyCode = KeyCode.F2;
    private Inventory inv;

    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            inv.Add(item);
        }
    }
}
