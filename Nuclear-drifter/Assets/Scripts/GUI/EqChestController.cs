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
    public string nameInvScene;
    public InventoryBox inventoryBox;
    private TypeScene typeSc;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        goEq.SetActive(active);
        inventoryBox = GameObject.Find(nameInvScene).GetComponent<InventoryBox>();
        typeSc = FindObjectOfType<TypeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (active) Close();
            else Open(); //test
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

    public void Open()
    {
        active = true;
        goEq.SetActive(active);
        gUI.move.active = !active;
        gUI.blockGUI = active;
        typeSc.inBox = active;
        SetItems();
    }

    private void SetItems()
    {
        List<Slot> slots = inventoryBox.boxes[currentIndex].eqSlots;
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
}
