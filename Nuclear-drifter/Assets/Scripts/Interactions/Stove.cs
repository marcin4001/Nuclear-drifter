using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    private SpriteRenderer render;
    public Sprite stoveOff;
    public Sprite stoveOn;
    public Slot slot;
    private Inventory inv;
    private GUIScript gUI;
    private PlayerClickMove player;
    private Slot meat;
    private FadePanel fade;
    private SoundsTrigger sound;
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
    }

    // Update is called once per frame
    public void Use()
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
                if(r_meat != null || meat.amountItem == 1)
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

    private void SetCook()
    {
        
        if (player.ObjIsNearPlayer(transform.position, 1.1f))
        {
            render.sprite = stoveOn;
            inv.RemoveOne(meat);
            fade.EnableImg(true);
            sound.Cook();
            Invoke("Cook", 2.3f);
        }
        else gUI.AddText("Stove is too far");
    }

    private void Cook()
    {
        fade.EnableImg(false);
        render.sprite = stoveOff;
        inv.Add(slot);
    }
}
