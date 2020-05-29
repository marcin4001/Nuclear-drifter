using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> slots;
    // Start is called before the first frame update
    public SlotElement[] slotsInv;
    private GUIScript gUI;
    public Slot testSlot;
    private void Awake()
    {
        gUI = FindObjectOfType<GUIScript>();
        if (slots == null) slots = new List<Slot>();
        else
        {
            if(slots.Count > 0)
            {
                Sort();
                for (int i = 0; i < slotsInv.Length; i++)
                {
                    if(i < slots.Count)
                    {
                        slotsInv[i].SetSlotStart(slots[i]);
                    }
                }
            }
        }
        
    }

    public bool IsFull()
    {
        return slots.Count >= slotsInv.Length;
    }

    public bool isEmpty()
    {
        return slots.Count <= 0;
    }

    public bool Add(Slot _slot)
    {
        bool result = true;
        if(!isEmpty())
        {
            Slot exist = slots.Find(s => s.itemElement == _slot.itemElement);
            if (exist != null)
            {
                if(_slot.itemElement.GetItemType() != ItemType.Weapon)
                {
                    exist.amountItem += _slot.amountItem;
                }
                else
                {
                    exist.ammo += _slot.ammo;
                }
            }
            else
            {
                if (!IsFull())
                {
                    slots.Add(new Slot(_slot.itemElement, _slot.amountItem, _slot.ammo));
                    Sort();
                }
                else
                {
                    gUI.AddText("Inventory is Full");
                    result = false;
                }
            }
            
            SetItems();
        }
        else
        {
            slots.Add(new Slot(_slot.itemElement, _slot.amountItem, _slot.ammo));
            SetItems();
        }
        return result;
    }
    private void Sort()
    {
        slots.Sort(delegate (Slot s1, Slot s2)
        {
            return s1.itemElement.idItem.CompareTo(s2.itemElement.idItem);
        });
    }

    private void SetItems()
    {
        if (slots.Count > 0)
        {
            for (int i = 0; i < slotsInv.Length; i++)
            {
                if (i < slots.Count)
                {
                    slotsInv[i].SetSlot(slots[i]);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Add(testSlot);
            Debug.Log("IsFull: " + IsFull());
            Debug.Log("IsEmpty: " + isEmpty());
            
        }
    }
}
