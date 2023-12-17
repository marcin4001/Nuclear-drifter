using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRelocator : MonoBehaviour
{
    public int idMission;
    public Transform npc;
    public Vector3 newPos;
    public bool changleCbReply;
    [TextArea(2, 3)]
    public string NewCbReply;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj mission = MissionList.global.GetMission(idMission);
        if(mission != null )
        {
            if(mission.complete)
            {
                npc.position = newPos;
                if(changleCbReply)
                {
                    NPCBasic nPCBasic = npc.GetComponentInChildren<NPCBasic>();
                    if (nPCBasic != null) nPCBasic.cbReply = NewCbReply;
                }
            }
        }
    }
}
