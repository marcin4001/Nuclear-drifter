﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public string nameBomb;
    public Item tool;
    public bool stateInit = true;
    public int indexDevice = -1;
    public bool isDismantled;
    private Inventory inv;
    private PlayerClickMove player;
    private GUIScript gUI;
    private FadePanel fade;
    private SoundsTrigger sound;

    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        if (indexDevice < 0) isDismantled = stateInit;
        else isDismantled = DeviceList.global.devices[indexDevice].repair;
        fade = FindObjectOfType<FadePanel>();
        sound = FindObjectOfType<SoundsTrigger>();
    }

    public void Use()
    {
        if (isDismantled)
        {
            gUI.AddText(nameBomb + " is inactive");
        }
        else
        {
            if (!inv.FindItemB(tool.idItem))
            {
                gUI.ClearText();
                gUI.AddText(nameBomb + " is active!");
                gUI.AddText("Items needed to ");
                gUI.AddText("dismantle: " + tool.nameItem);
                return;
            }
            if (player.ObjIsNearPlayer(transform.position, 1.1f))
            {
                sound.UseTool();
                fade.EnableImg(true);
                Invoke("Disarming", 1.0f);
            }
            else
            {
                gUI.AddText(nameBomb + " is too far");
            }
        }
    }

    private void Disarming()
    {
        fade.EnableImg(false);
        isDismantled = true;
        DeviceList.global.devices[indexDevice].repair = isDismantled;
        gUI.AddText("Bomb has been disarmed!");
    }
}