using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float minDistance = 1.5f;
    private TerminalController terminal;
    private PlayerClickMove player;
    private GUIScript gUI;

    private void Start()
    {
        terminal = FindObjectOfType<TerminalController>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
    }

    public void Use()
    {
        if (PropertyPlayer.property.isDehydrated)
        {
            gUI.AddText("You are dehydrated!");
            gUI.AddText("You can't use it now!");
            return;
        }
        if (player.ObjIsNearPlayer(transform.position, minDistance))
        {
            terminal.StartTerminal();
        }
        else
        {
            gUI.AddText("Computer is too far");
        }
    }
}
