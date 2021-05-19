using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    public int id_mission = 0;
    private GUIScript gUI;
    private PlayerClickMove move;
    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        move = FindObjectOfType<PlayerClickMove>();
    }

    public void ShowText()
    {
        gUI.AddText("This is well");
    }

    public void Use()
    {
        MissionObj mission = MissionList.global.GetMission(id_mission);
        if(mission.complete)
        {
            if(move.ObjIsNear("Well", 1.0f))
            {
                gUI.AddText("You got 1x Clean water");
            }
            else
            {
                gUI.AddText("The well is too far");
            }
        }
        else
        {
            gUI.AddText("The well is dry");
        }
    }
}
