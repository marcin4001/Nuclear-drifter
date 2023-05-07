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
    private BakeContextMenu contextMenu;
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
        contextMenu = FindObjectOfType<BakeContextMenu>();
        achievement = FindObjectOfType<Achievement>();
    }

    public void Use()
    {
        if (contextMenu.active)
        {
            return;
        }
        if (PropertyPlayer.property.isDehydrated)
        {
            gUI.AddText("You are dehydrated!");
            gUI.AddText("You can't use it now!");
            return;
        }
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
            if (!inv.FindItemB(212) && !inv.FindItemB(206))
            {
                gUI.AddText("I don't have raw meat");
                gUI.AddText("or fish");
                return;
            }
            if (!move.ObjIsNear("FirePlace", 1.0f))
            {
                gUI.AddText("Fireplace is too far");
                return;
            }
            if (inv.FindItemB(212) && inv.FindItemB(206))
            {
                contextMenu.ShowMenu(gameObject);
                return;
            }
            if (inv.FindItemB(212))
                BakeRawMeat();
            if (inv.FindItemB(206))
                BakeFish();
            //
            //isFish = false;
            //meatOrFish = inv.FindItem(212);
            //if (meatOrFish == null)
            //{
            //    meatOrFish = inv.FindItem(206);
            //    isFish = true;
            //}
            //if (meatOrFish != null)
            //{
            //    if (!inv.IsFull())
            //    {
            //        SetCook();
            //    }
            //    else
            //    {
            //        if (!isFish)
            //        {
            //            Slot r_meat = inv.FindItem(213);
            //            if (r_meat != null || meatOrFish.amountItem == 1)
            //            {
            //                SetCook();
            //            }
            //            else
            //            {
            //                gUI.AddText("Inventory is full");
            //            }
            //        }
            //        else
            //        {
            //            Slot f_meat = inv.FindItem(222);
            //            if (f_meat != null || meatOrFish.amountItem == 1)
            //            {
            //                SetCook();
            //            }
            //            else
            //            {
            //                gUI.AddText("Inventory is full");
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    gUI.AddText("I don't have raw meat");
            //    gUI.AddText("or fish");
            //}
        }
    }

    public void BakeRawMeat()
    {
        meatOrFish = inv.FindItem(212);
        isFish = false;
        if (!inv.IsFull())
        {
            SetCook();
        }
        else
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
    }

    public void BakeFish()
    {
        meatOrFish = inv.FindItem(206);
        isFish = true;
        if (!inv.IsFull())
        {
            SetCook();
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

    private void SetCook()
    {
        inv.RemoveOne(meatOrFish);
        fade.EnableImg(true);
        sound.Cook();
        if (!isFish)
            Invoke("CookMeat", 2.3f);
        else
            Invoke("CookFish", 2.3f);
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
