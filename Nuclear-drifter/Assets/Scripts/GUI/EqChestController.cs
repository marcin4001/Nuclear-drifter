using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqChestController : MonoBehaviour
{
    private GUIScript gUI;
    public GameObject goEq;
    public bool active = false;
    public int currentIndex = 0;
    public SlotElement[] slotsInv;
    //public string nameInvScene;
    public InventoryBox inventoryBox;
    public List<Slot> slots;
    public Sprite[] backgrounds;
    public Image backImg;
    private Inventory inv;
    private TypeScene typeSc;
    //public int testIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        goEq.SetActive(active);
        inventoryBox = FindObjectOfType<InventoryBox>();
        typeSc = FindObjectOfType<TypeScene>();
        inv = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (active) Close();
            //else Open(1, testIndex); //test
        }
    }

    public void Close()
    {
        active = false;
        goEq.SetActive(active);
        gUI.move.active = !active;
        gUI.blockGUI = active;
        typeSc.inBox = active;
    }

    public void Open(int id, int indexBack)
    {
        currentIndex = id;
        backImg.overrideSprite = backgrounds[indexBack];
        active = true;
        goEq.SetActive(active);
        gUI.move.active = !active;
        gUI.blockGUI = active;
        typeSc.inBox = active;
        SetItems();
    }

    private void SetItems()
    {
        slots = inventoryBox.boxes[currentIndex].eqSlots;
        if (slots != null)
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
        else
        {
            inventoryBox.boxes[currentIndex].eqSlots = new List<Slot>();
            for (int i = 0; i < slotsInv.Length; i++) slotsInv[i].ClearSlot();
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
        if (!isEmpty())
        {
            Slot exist = slots.Find(s => s.itemElement == _slot.itemElement);
            if (exist != null)
            {
                if (_slot.itemElement.GetItemType() != ItemType.Weapon)
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
                }
                else
                {
                    gUI.AddText("Container is Full");
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
                }
                else
                {
                    gUI.AddText("Container is Full");
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

    public void RemoveOne(Slot _slot)
    {
        if (_slot.amountItem > 1)
        {
             _slot.amountItem--;
        }
        else
        {
            slots.Remove(_slot);
        }
        SetItems();
    }

    public void RemoveAll(Slot _slot)
    {
        slots.Remove(_slot);
        SetItems();
    }

    public void TakeOutOne(Slot _slot)
    {
        bool added = inv.AddOne(_slot);
        if(added)
        {
            RemoveOne(_slot);
        }
    }

    public void TakeOutAll(Slot _slot)
    {
        bool added = inv.Add(_slot);
        if (added)
        {
            RemoveAll(_slot);
        }
    }

    public void PutOnOne(Slot _slot)
    {
        bool added = AddOne(_slot);
        if (added)
        {
            inv.RemoveOne(_slot);
        }
    }

    public void PutOnAll(Slot _slot)
    {
        bool added = Add(_slot);
        if (added)
        {
            inv.RemoveAll(_slot);
        }
    }
}
