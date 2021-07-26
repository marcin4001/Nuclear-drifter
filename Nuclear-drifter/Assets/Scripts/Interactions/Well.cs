using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    public int id_mission = 0;
    public Slot water;
    private GUIScript gUI;
    private PlayerClickMove move;
    private Inventory inv;
    private TimeGame time;

    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        move = FindObjectOfType<PlayerClickMove>();
        inv = FindObjectOfType<Inventory>();
        time = FindObjectOfType<TimeGame>();
    }

    public void ShowText()
    {
        gUI.AddText("This is the well");
    }

    public void Use()
    {
        if(!move.ObjIsNear("Well", 1.0f))
        {
            gUI.AddText("The well is too far");
            return;
        }
        MissionObj mission = MissionList.global.GetMission(id_mission);
        if(!mission.complete)
        {
            gUI.AddText("The well is dry");
            return;
        }
        if(time.day == PropertyPlayer.property.waterDay)
        {
            gUI.AddText("End of water for today");
            return;
        }
        Slot temp = inv.FindItem(water.id);
        if(inv.IsFull() && temp == null)
        {
            gUI.AddText("Inventory is full");
            return;
        }

        inv.Add(water);
        PropertyPlayer.property.waterDay = time.day;
        gUI.AddText("You got " + water.amountItem + "x " + water.itemElement.nameItem);
    }
}
