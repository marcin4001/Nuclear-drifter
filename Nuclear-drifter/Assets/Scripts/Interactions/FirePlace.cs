using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
    private PlayerClickMove move;
    private DayCycle cycle;
    private GUIScript gUI;
    private Inventory inv;
    private Slot meat;
    private FadePanel fade;
    public Slot slot;
    public int id_ach = 2;
    public bool allTimeFire = false;
    private SoundsTrigger sound;
    private Achievement achievement;
    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<PlayerClickMove>();
        cycle = FindObjectOfType<DayCycle>();
        gUI = FindObjectOfType<GUIScript>();
        inv = FindObjectOfType<Inventory>();
        fade = FindObjectOfType<FadePanel>();
        sound = FindObjectOfType<SoundsTrigger>();
        achievement = FindObjectOfType<Achievement>();
    }

    public void Use()
    {
        int hour = cycle.GetHour();
        bool result = inv.FindItemB(54);
        if(!result)
            gUI.AddText("You don't have a pan");
        if (!allTimeFire)
        {
            result &= hour >= 20 || hour <= 3;
            if (!result)
            {
                gUI.AddText("The fireplace can be used");
                gUI.AddText("from 8 PM to 4 AM");
            }
        }

        if (result)
        {
            meat = inv.FindItem(212);
            if(meat != null)
            {
                if(!inv.IsFull())
                {
                    SetCook();
                }
                else
                {
                    Slot r_meat = inv.FindItem(213);
                    if (r_meat != null || meat.amountItem == 1)
                    {
                        SetCook();
                    }
                    else
                    {
                        gUI.AddText("Inventory is full");
                    }
                }
            }
            else
            {
                gUI.AddText("I don't have raw meat");
            }
        }
    }

    private void SetCook()
    {
        if (move.ObjIsNear("FirePlace", 1.0f))
        {
            inv.RemoveOne(meat);
            fade.EnableImg(true);
            sound.Cook();
            Invoke("Cook", 2.3f);
        }
        else
        {
            gUI.AddText("The fireplace is too far");
        }
    }

    private void Cook()
    {
        fade.EnableImg(false);
        inv.Add(slot);
        achievement.SetAchievement(id_ach);
    }
}
