﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionNPC : MonoBehaviour
{
    public MissionDetails[] mission;
    private Inventory inv;
    private NPCBasic npc;
    private MissionPrize prize;
    private MissionStart startM;
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        npc = GetComponent<NPCBasic>();
        prize = GetComponent<MissionPrize>();
        startM = GetComponent<MissionStart>();
    }

    public MissionDetails GetMission(int id)
    {
        foreach(MissionDetails m in mission)
        {
            if (m.id == id) return m;
        }
        return null;
    }

    public void StartMission(int id)
    {
        MissionList.global.missions[id].start = true;
        CheckMission();
        if(startM != null)
        {
            startM.Init(id);
        }
    }

    public void CompleteMission(int id)
    {
        MissionList.global.missions[id].complete = true;
        MissionDetails m = GetMission(id);
        if(m != null)
        {
            if(m.needItem)
            {
                inv.RemoveFew(m.slotItem);
                npc.SetStartIndex(m.dialogComplete);
            }
        }
        if(prize != null)
        {
            prize.GivePrize(id);
        }
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