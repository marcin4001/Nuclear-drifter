using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListMissionTest : MonoBehaviour
{
    public List<MissionObj> missions;
    // Start is called before the first frame update
    void Start()
    {
        missions = MissionList.global.GetListMission();
    }

    
}
