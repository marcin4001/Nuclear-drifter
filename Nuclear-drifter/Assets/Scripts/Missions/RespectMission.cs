using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespectMission : MonoBehaviour
{
    public int missionBefore = -1;
    public int respect = 75;
    public int dialogIndex = -1;
    public bool respectUSA = false;
    private NPCBasic nPC;
    // Start is called before the first frame update
    void Start()
    {
        nPC = GetComponent<NPCBasic>();
    }

    public void CheckRespect()
    {
        int currentRespect = MissionList.global.PercentRespect();
        if (respectUSA)
            currentRespect = MissionList.global.PercentRespectUSA();
        if (!nPC.GetHaveRespect())
        {
            MissionObj before = MissionList.global.GetMission(missionBefore);
            if (before == null && respect <= currentRespect)
            {
                if (dialogIndex > 0) nPC.SetStartIndex(dialogIndex);
                nPC.SetHaveRespect();
                return;
            }
            if (before != null)
            {
                if (before.complete && respect <= currentRespect)
                {
                    if (dialogIndex > 0) nPC.SetStartIndex(dialogIndex);
                    nPC.SetHaveRespect();
                    return;
                }
            }
        }
        
    }
}
