﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> slots;
    // Start is called before the first frame update
    public SlotElement[] slotsInv;
    private GUIScript gUI;
    public Slot testSlot;
    public GameObject bag;
    private PlayerClickMove move;
    private TypeScene typeSc;
    private void Awake()
    {
        gUI = FindObjectOfType<GUIScript>();
        move = FindObjectOfType<PlayerClickMove>();
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

    private void Start()
    {
        typeSc = FindObjectOfType<TypeScene>();
        if (PropertyPlayer.property.inv != null)
            slots = PropertyPlayer.property.inv;
        else
        {
            PropertyPlayer.property.inv = new List<Slot>();
            slots = PropertyPlayer.property.inv;
        }
        if (slots == null) slots = new List<Slot>();
        else
        {
            if (slots.Count > 0)
            {
                Sort();
                for (int i = 0; i < slotsInv.Length; i++)
                {
                    if (i < slots.Count)
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

    public bool AddOne(Slot _slot)
    {
        bool result = true;
        if (!isEmpty())
        {
            Slot exist = slots.Find(s => s.itemElement == _slot.itemElement);
            if (exist != null)
            {
                if (_slot.itemElement.GetItemType() != ItemType.Weapon)
                {
                    exist.amountItem += 1;
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
                    slots.Add(new Slot(_slot.itemElement, 1, _slot.ammo));
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
            slots.Add(new Slot(_slot.itemElement, 1, _slot.ammo));
            SetItems();
        }
        return result;
    }

    public Slot FindItem(int id)
    {
        return slots.Find(x => x.itemElement.idItem == id);
    }

    public bool FindItemB(int id)
    {
        return slots.Exists(x => x.itemElement.idItem == id);
    }

    public void RemoveOne(Slot _slot)
    {
        if (_slot.itemElement.GetItemType() != ItemType.Weapon || typeSc.inBox) 
        {
            if (_slot.amountItem > 1)
            {
                _slot.amountItem--;
                SetItems();
            }
            else
            {
                slots.Remove(_slot);
                SetItems();
                Sort();
            }
        }
        else
        {
            if (_slot.ammo > 0) _slot.ammo--;
            else gUI.AddText("Ammo is over!");
        }
    }

    public void RemoveAll(Slot _slot)
    {
        if (!typeSc.inBox)
        {
            Slot newslot = new Slot(_slot.itemElement, _slot.amountItem, _slot.ammo);
            slots.Remove(_slot);
            SetItems();
            Sort();
            bool isBag = false;
            Collider2D[] col = Physics2D.OverlapCircleAll((Vector2)move.GetPosPlayer(), 0.5f);
            foreach(Collider2D c in col)
            {
                isBag = c.gameObject.name.StartsWith("bag") || isBag;
            }
            
            GameObject obj = Instantiate(bag, move.GetPosPlayer(), Quaternion.identity);
            ItemElement itemElement = obj.GetComponent<ItemElement>();
            if (itemElement != null) itemElement.item = newslot;
            SpriteRenderer r = obj.GetComponent<SpriteRenderer>();
            if (r != null) r.enabled = !isBag;
        }
        else
        {
            slots.Remove(_slot);
            SetItems();
            Sort();
        }
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
                else
                {
                    slotsInv[i].ClearSlot();
                }
            }
        }
        else
        {
            for (int i = 0; i < slotsInv.Length; i++) slotsInv[i].ClearSlot();
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