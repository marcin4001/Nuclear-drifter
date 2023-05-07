﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    private SpriteRenderer render;
    public Sprite stoveOff;
    public Sprite stoveOn;
    public Slot slot;
    public Slot slotFish;
    public int id_ach = 1;
    private Inventory inv;
    private GUIScript gUI;
    private PlayerClickMove player;
    private Slot meatOrFish;
    private FadePanel fade;
    private SoundsTrigger sound;
    private BakeContextMenu contextMenu;
    private Achievement achievement;
    private bool isFish = false;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.sprite = stoveOff;
        inv = FindObjectOfType<Inventory>();
        gUI = FindObjectOfType<GUIScript>();
        player = FindObjectOfType<PlayerClickMove>();
        fade = FindObjectOfType<FadePanel>();
        sound = FindObjectOfType<SoundsTrigger>();
        contextMenu = FindObjectOfType<BakeContextMenu>();
        achievement = FindObjectOfType<Achievement>();
    }

    // Update is called once per frame
    public void Use()
    {
        if(contextMenu.active)
        {
            return;
        }
        if(PropertyPlayer.property.isDehydrated)
        {
            gUI.AddText("You are dehydrated!");
            gUI.AddText("You can't use it now!");
            return;
        }
        if(!inv.FindItemB(212) && !inv.FindItemB(206))
        {
            gUI.AddText("I don't have raw meat");
            gUI.AddText("or fish");
            return;
        }
        if (!player.ObjIsNearPlayer(transform.position, 1.1f))
        {
            gUI.AddText("Stove is too far");
            return;
        }
        if(inv.FindItemB(212) && inv.FindItemB(206))
        {
            contextMenu.ShowMenu(gameObject);
            return;
        }

        if (inv.FindItemB(212))
            BakeRawMeat();
        if (inv.FindItemB(206))
            BakeFish();
        //isFish = false;
        //meatOrFish = inv.FindItem(212);
        //if(meatOrFish == null)
        //{
        //    meatOrFish = inv.FindItem(206);
        //    isFish = true;
        //}
        //if(meatOrFish != null)
        //{
        //    if(!inv.IsFull())
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
        render.sprite = stoveOn;
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
        render.sprite = stoveOff;
        inv.Add(slot);
        achievement.SetAchievement(id_ach);
    }

    private void CookFish()
    {
        fade.EnableImg(false);
        render.sprite = stoveOff;
        inv.Add(slotFish);
        achievement.SetAchievement(id_ach);
    }
}
