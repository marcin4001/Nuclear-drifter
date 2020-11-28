using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    public SpriteRenderer holeTrapdoor;
    public SpriteRenderer upTrapdoor;
    public SpriteRenderer downTrapdoor;

    public Item key;
    public Item rope;
    public Item gasMask;

    public List<string> result;

    private GUIScript gUI;
    private Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        holeTrapdoor = GetComponent<SpriteRenderer>();
        gUI = FindObjectOfType<GUIScript>();
        inv = FindObjectOfType<Inventory>();
        holeTrapdoor.enabled = false;
        upTrapdoor.enabled = false;
    }

    public void Use()
    {
        bool open = true;
        result = new List<string>();
        result.Add("Items needed to open:");
        if (!inv.FindItemB(key.idItem))
        {
            result.Add("- " + key.nameItem);
            open = false;
        }
        if (!inv.FindItemB(rope.idItem))
        {
            result.Add("- " + rope.nameItem);
            open = false;
        }

        if (open)
        {
            holeTrapdoor.enabled = true;
            upTrapdoor.enabled = true;
            downTrapdoor.enabled = false;
        }
        else
        {
            foreach (string res in result)
            {
                gUI.AddText(res);
            }
        }
    }

    public void ShowText()
    {
        if (gUI == null) gUI = FindObjectOfType<GUIScript>();
        if (gUI != null)
        {
            gUI.AddText("This is the subway");
            gUI.AddText("entrance");
        }
    }
}
