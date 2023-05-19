using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornRemovalPlugin : MonoBehaviour
{
    private int idMission = 28;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj mission = MissionList.global.GetMission(idMission);
        if (mission == null)
            Destroy(this);
        if(mission.start)
            SkillsAndPerks.playerSkill.AddOtherSkill("Horn Removal");
        Destroy(this);
    }

}
