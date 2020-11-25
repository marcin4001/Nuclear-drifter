using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespectMission : MonoBehaviour
{
    public int missionBefore = -1;
    public int respect = 75;
    public int dialogIndex = -1;
    private NPCBasic nPC;
    // Start is called before the first frame update
    void Start()
    {
        nPC = GetComponent<NPCBasic>();
    }

    public void CheckRespect()
    {
        if (!nPC.GetHaveRespect())
        {
            MissionObj before = MissionList.global.GetMission(missionBefore);
            if (before == null && respect <= MissionList.global.PercentRespect())
            {
                if (dialogIndex > 0) nPC.SetStartIndex(dialogIndex);
                nPC.SetHaveRespect();
                return;
            }
            if (before != null)
            {
                if (before.complete && respect <= MissionList.global.PercentRespect())
                {
                    if (dialogIndex > 0) nPC.SetStartIndex(dialogIndex);
                    nPC.SetHaveRespect();
                    return;
                }
            }
        }
        
    }
}
