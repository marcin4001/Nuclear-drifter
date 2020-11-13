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

    public int GetFullRespect()
    {
        if (missions.Length == 0) return 0;
        int fullRespect = 0;
        foreach(MissionObj m in missions)
        {
            fullRespect += m.respect;
        }
        return fullRespect;
    }

    public int GetCurrentRespect()
    {
        if (missions.Length == 0) return 0;
        int respect = 0;
        foreach (MissionObj m in missions)
        {
           if(m.complete) respect += m.respect;
        }
        return respect;
    }

    public int PercentRespect()
    {
        int percent = 0;
        float respectPercent = (float)GetCurrentRespect() / (float)GetFullRespect();
        percent = (int)(respectPercent * 100);
        return percent;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        Debug.Log(GetCurrentRespect());
    //        Debug.Log(PercentRespect() + "%");
    //    }
    //}
}
