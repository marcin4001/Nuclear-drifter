using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayIcon : MonoBehaviour
{
    public int idMission = 0;
    public int idMissionOther = 0;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        MissionObj subwayMission = MissionList.global.GetMission(idMission);
        if(subwayMission != null)
        {
            if (!subwayMission.start)
            {
                render.enabled = false;
            }
        }
        MissionObj subwayMissionOther = MissionList.global.GetMission(idMissionOther);
        if(subwayMissionOther != null)
        {
            if(subwayMissionOther.complete)
                render.enabled = true;
        }
    }

    public void SetIconActive()
    {
        render = GetComponent<SpriteRenderer>();
        render.enabled = true;
    }
}
