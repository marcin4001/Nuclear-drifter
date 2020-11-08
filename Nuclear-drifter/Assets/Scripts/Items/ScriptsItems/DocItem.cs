using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDocument", menuName = "Item/DocItem")]
public class DocItem : Item
{
    public int idDoc;

    private void Awake()
    {
        type = ItemType.Document;
    }

    public override void Use()
    {
        Debug.Log("Use: " + name);
        DocumentController doc = FindObjectOfType<DocumentController>();
        if (doc != null) doc.OpenDoc(idDoc);
    }
}
