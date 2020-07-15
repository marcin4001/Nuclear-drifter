using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionNPC : MonoBehaviour
{
    public MissionDetails[] mission;
    private Inventory inv;
    private NPCBasic npc;
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        npc = GetComponent<NPCBasic>();
    }

    public void StartMission(int id)
    {
        MissionList.global.missions[id].start = true;
    }

    public void CheckMission()
    {
        if(mission.Length > 0)
        {
            foreach(MissionDetails m in mission)
            {
                MissionObj m_obj = MissionList.global.missions[m.id];
                if(m_obj.start && !m_obj.complete)
                {
                    if(m.needItem)
                    {
                        if(inv.CheckAccessItem(m.slotItem))
                        {
                            npc.SetStartIndex(m.dialogSuccess);
                        }
                        else
                        {
                            npc.SetStartIndex(m.dialogNormal);
                        }
                    }
                    else
                    {
                        npc.SetStartIndex(m.dialogSuccess);
                    }
                }
            }
        }
    }

}
