using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionList : MonoBehaviour
{
    public static MissionList global;
    public MissionObj[] missions;
    public NPCElement[] globalNPCs;
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

    public int GetFullRespectUSA()
    {
        if (missions.Length == 0) return 0;
        int fullRespect = 0;
        foreach (MissionObj m in missions)
        {
            fullRespect += m.respectUSA;
        }
        return fullRespect;
    }

    public int GetCurrentRespectUSA()
    {
        if (missions.Length == 0) return 0;
        int respect = 0;
        foreach (MissionObj m in missions)
        {
            if (m.complete) respect += m.respectUSA;
        }
        return respect;
    }

    public int PercentRespectUSA()
    {
        int percent = 0;
        float respectPercent = (float)GetCurrentRespectUSA() / (float)GetFullRespectUSA();
        percent = (int)(respectPercent * 100);
        return percent;
    }

    public static string GetJson()
    {
        string json = JsonUtility.ToJson(global);
        return json;
    }

    public static void JsonToObj(string json)
    {
        JsonUtility.FromJsonOverwrite(json, global);
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        Debug.Log(GetCurrentRespectUSA());
    //        Debug.Log(PercentRespectUSA() + "%");
    //    }
    //}

    public void SetNPC(int index, bool _init, int _startIndex)
    {
        globalNPCs[index].init = _init;
        globalNPCs[index].startIndex = _startIndex;
    }

    public void SetHaveRespect(int index)
    {
        globalNPCs[index].haveRespect = true;
    }

    public void ResetGlobalNPCs()
    {
        foreach (NPCElement element in globalNPCs)
            element.Reset();
    }
}
