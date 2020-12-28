using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Perk
{
    public string name;
    public bool playerHas = false;
    public int level = 0;

    public void AddPerk(bool inc = false)
    {
        playerHas = true;
        if (inc)
            level = level + 1;
    }
}
