using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionNPC : MonoBehaviour
{
    public MissionDetails[] mission;
    private Inventory inv;
    private NPCBasic npc;
    private MissionPrize prize;
    private MissionStart startM;
    private Experience exp;
    private RespectMission respectM;
    // Start is called before the first frame update
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        npc = GetComponent<NPCBasic>();
        prize = GetComponent<MissionPrize>();
        startM = GetComponent<MissionStart>();
        exp = FindObjectOfType<Experience>();
        respectM = GetComponent<RespectMission>();
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
        MissionList.global.StartMission(id);
        CheckMission();
        if(startM != null)
        {
            startM.Init(id);
        }
    }

    public void CompleteMission(int id)
    {
        MissionList.global.CompleteMission(id);
        int amountExp = MissionList.global.GetExp(id);
        exp.AddExp(amountExp);
        MissionDetails m = GetMission(id);
        if(m != null)
        {
            npc.SetStartIndex(m.dialogComplete);
            if (m.needItem && !m.noRemove)
            {
                if(!m.removeAll)inv.RemoveFew(m.slotItem);
                else inv.RemoveAllId(m.slotItem.itemElement.idItem);
            }
        }
        if(prize != null)
        {
            prize.GivePrize(id);
        }
        if (respectM != null) respectM.CheckRespect();
    }

    public void CompleteMissionNoCond(int id)
    {
        MissionList.global.CompleteMission(id);
        int amountExp = MissionList.global.GetExp(id);
        exp.AddExp(amountExp);
        MissionDetails m = GetMission(id);
        if (m != null)
        {
            npc.SetStartIndex(m.dialogComplete);
        }
        if (prize != null)
        {
            prize.GivePrize(id);
        }
        if (respectM != null) respectM.CheckRespect();
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
                    switch(m.type){
                        case MissionType.item:
                            {
                                ItemMission(m);
                                break;
                            }
                        case MissionType.device:
                            {
                                DeviceMission(m);
                                break;
                            }
                        case MissionType.kill:
                            {
                                KillMission(m);
                                break;
                            }
                        default:
                            {
                                npc.SetStartIndex(m.dialogSuccess);
                                break;
                            }
                    }
                }
            }
        }
        if (respectM != null) respectM.CheckRespect();
    }

    private void ItemMission(MissionDetails m)
    {
        if (m.needItem)
        {
            if (inv.CheckAccessItem(m.slotItem))
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

    private void DeviceMission(MissionDetails m)
    {
        DeviceElement device = DeviceList.global.GetDevice(m.indexDevice);
        if(device != null)
        {
            if (device.repair)
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
            npc.SetStartIndex(m.dialogNormal);
        }
    }

    private void KillMission(MissionDetails m)
    {
        int enemiesAlive = EnemyMissionList.global.HowMunyAlive(m.id);
        if(enemiesAlive <= 0)
        {
            npc.SetStartIndex(m.dialogSuccess);
        }
        else
        {
            npc.SetStartIndex(m.dialogNormal);
        }
    }
}
