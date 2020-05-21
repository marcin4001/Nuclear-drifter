using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMisc", menuName = "Item/MiscItem")]
public class MiscItem : Item
{
    public override void Use()
    {
        if (gUI == null) gUI = FindObjectOfType<GUIScript>();
        if (gUI != null) gUI.AddText("This cannot be used");
    }
}
