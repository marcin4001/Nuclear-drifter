using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSystem : MonoBehaviour
{
    public List<Slot> slots;
    public TradeSlot[] tradeSlots;
    private GUIScript gUI;
    private PlayerClickMove move;
    private TypeScene typeSc;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        move = FindObjectOfType<PlayerClickMove>();
        typeSc = FindObjectOfType<TypeScene>();
        if (slots == null) slots = new List<Slot>();
        else
        {
            if (slots.Count > 0)
            {
                for (int i = 0; i < tradeSlots.Length; i++)
                {
                    if (i < slots.Count)
                    {
                        tradeSlots[i].SetSlotStart(slots[i]);
                    }
                    else
                    {
                        tradeSlots[i].ClearSlot();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
