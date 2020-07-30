using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRelocate : MonoBehaviour
{
    public GameObject npc;
    public bool visible = true;
    public int idMission = 0;
    private GridNode nodes;
    // Start is called before the first frame update
    void Start()
    {
        if (npc == null) Destroy(gameObject);
        nodes = FindObjectOfType<GridNode>();
        MissionObj mission = MissionList.global.GetMission(idMission);
        if(mission != null)
        {
            if(mission.complete && visible || !mission.complete && !visible)
            {
                nodes.SetWalkable(npc.transform.position, true);
                npc.SetActive(false);
            }
        }
    }

}
