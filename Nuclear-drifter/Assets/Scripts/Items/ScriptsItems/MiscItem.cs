using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMisc", menuName = "Item/MiscItem")]
public class MiscItem : Item
{
    private void Awake()
    {
        type = ItemType.Misc;
    }

    public override void Use()
    {
        if (gUI == null) gUI = FindObjectOfType<GUIScript>();
        if (gUI != null) gUI.AddText("This cannot be used");
    }
}
