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
    private Slot meatOrFish;
    private FadePanel fade;
    public Slot slot;
    public Slot slotFish;
    public int id_ach = 2;
    public bool allTimeFire = false;
    private SoundsTrigger sound;
    private Achievement achievement;
    private bool isFish = false;
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
            result &= hour >= 21 || hour <= 5;
            if (!result)
            {
                gUI.AddText("The fireplace can be used");
                gUI.AddText("from 9 PM to 6 AM");
            }
        }

        if (result)
        {
            isFish = false;
            meatOrFish = inv.FindItem(212);
            if (meatOrFish == null)
            {
                meatOrFish = inv.FindItem(206);
                isFish = true;
            }
            if (meatOrFish != null)
            {
                if (!inv.IsFull())
                {
                    SetCook();
                }
                else
                {
                    if (!isFish)
                    {
                        Slot r_meat = inv.FindItem(213);
                        if (r_meat != null || meatOrFish.amountItem == 1)
                        {
                            SetCook();
                        }
                        else
                        {
                            gUI.AddText("Inventory is full");
                        }
                    }
                    else
                    {
                        Slot f_meat = inv.FindItem(222);
                        if (f_meat != null || meatOrFish.amountItem == 1)
                        {
                            SetCook();
                        }
                        else
                        {
                            gUI.AddText("Inventory is full");
                        }
                    }
                }
            }
            else
            {
                gUI.AddText("I don't have raw meat");
                gUI.AddText("or fish");
            }
        }
    }

    private void SetCook()
    {
        if (move.ObjIsNear("FirePlace", 1.0f))
        {
            inv.RemoveOne(meatOrFish);
            fade.EnableImg(true);
            sound.Cook();
            if (!isFish)
                Invoke("CookMeat", 2.3f);
            else
                Invoke("CookFish", 2.3f);
        }
        else
        {
            gUI.AddText("The fireplace is too far");
        }
    }

    private void CookMeat()
    {
        fade.EnableImg(false);
        inv.Add(slot);
        achievement.SetAchievement(id_ach);
    }

    private void CookFish()
    {
        fade.EnableImg(false);
        inv.Add(slotFish);
        achievement.SetAchievement(id_ach);
    }
}
