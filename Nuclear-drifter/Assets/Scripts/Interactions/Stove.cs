using System.Collections;
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
        achievement = FindObjectOfType<Achievement>();
    }

    // Update is called once per frame
    public void Use()
    {
        if(PropertyPlayer.property.isDehydrated)
        {
            gUI.AddText("You are dehydrated!");
            gUI.AddText("You can't use it now!");
            return;
        }
        isFish = false;
        meatOrFish = inv.FindItem(212);
        if(meatOrFish == null)
        {
            meatOrFish = inv.FindItem(206);
            isFish = true;
        }
        if(meatOrFish != null)
        {
            if(!inv.IsFull())
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

    private void SetCook()
    {
        
        if (player.ObjIsNearPlayer(transform.position, 1.1f))
        {
            render.sprite = stoveOn;
            inv.RemoveOne(meatOrFish);
            fade.EnableImg(true);
            sound.Cook();
            if(!isFish)
                Invoke("CookMeat", 2.3f);
            else
                Invoke("CookFish", 2.3f);
        }
        else gUI.AddText("Stove is too far");
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
