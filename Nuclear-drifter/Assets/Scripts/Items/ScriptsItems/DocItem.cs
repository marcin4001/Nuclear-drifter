using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDocument", menuName = "Item/DocItem")]
public class DocItem : Item
{
    public string idDoc = "";

    private void Awake()
    {
        type = ItemType.Document;
    }

    public override void Use()
    {
        
    }
}
