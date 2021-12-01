using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunt : MonoBehaviour
{
    public Slot item;
    public Vector3 offset = new Vector3(1.0f, 0.0f, 0.0f);
    public GameObject itemPref;
    public int idMission;
    public int idTool = 52;

    private Inventory inv;
    private GUIScript gUI;

    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        gUI = FindObjectOfType<GUIScript>();
    }

    public void GiveTrophy()
    {
        MissionObj mission = MissionList.global.GetMission(idMission);
        if (mission == null)
            return;
        if (!mission.start)
            return;

        if(!inv.FindItemB(idTool))
        {
            gUI.AddText("You need a knife");
            gUI.AddText("to take a trophy.");
            return;
        }

        if (inv.IsFull() && !inv.FindItemB(item.itemElement.idItem))
        {
            GameObject obj = Instantiate(itemPref, transform.position + offset, Quaternion.identity);
            ItemElement itemEl = obj.GetComponent<ItemElement>();
            if (itemEl != null) itemEl.item = item;
        }
        else
        {
            gUI.AddText("You got " + item.itemElement.nameItem + " x" + item.amountItem);
            inv.Add(item);
        }
    }
}
