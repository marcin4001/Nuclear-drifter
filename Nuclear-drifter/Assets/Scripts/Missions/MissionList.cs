using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionList : MonoBehaviour
{
    public static MissionList global;
    public MissionObj[] missions;
    // Start is called before the first frame update
    private void Awake()
    {
        if(!global)
        {
            DontDestroyOnLoad(this.gameObject);
            global = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public MissionObj GetMission(int id)
    {
        if (missions.Length == 0) return null;
        if(id >= 0 && id < missions.Length)
        {
            return missions[id];
        }
        else
        {
            return null;
        }
    }

    public void StartMission(int id)
    {
        if (missions.Length == 0) return;
        if (id >= 0 && id < missions.Length)
        {
            missions[id].start = true;
        }
    }

    public void CompleteMission(int id)
    {
        if (missions.Length == 0) return;
        if (id >= 0 && id < missions.Length)
        {
            missions[id].complete = true;
        }
    }

    public int GetExp(int id)
    {
        if (missions.Length == 0) return 0;
        if (id >= 0 && id < missions.Length)
        {
            return missions[id].exp;
        }
        else
        {
            return 0;
        }
    }

    public List<MissionObj> GetListMission()
    {
        List<MissionObj> list = missions.ToList();
        list.Sort(delegate (MissionObj m1, MissionObj m2)
        {
            return m2.idSort.CompareTo(m1.idSort);
        });

        return list;
    }
}
