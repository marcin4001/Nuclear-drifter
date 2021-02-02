using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayIcon : MonoBehaviour
{
    public int idMission = 0;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        MissionObj subwayMission = MissionList.global.GetMission(idMission);
        if(subwayMission != null)
        {
            if (!subwayMission.start)
                render.enabled = false;
        }
    }

    public void SetIconActive()
    {
        render.enabled = true;
    }
}
