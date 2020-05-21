using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Food,
    Document,
    Misc
}

public abstract class Item : ScriptableObject
{
    public string nameItem;
    public Sprite image;
    public string description;
    public int value;
    protected ItemType type;
    protected GUIScript gUI;

    public void Look()
    {
        if(gUI == null)gUI = FindObjectOfType<GUIScript>();
        if (gUI != null) gUI.AddText("Item: " + nameItem);
    }

    public abstract void Use();
}
