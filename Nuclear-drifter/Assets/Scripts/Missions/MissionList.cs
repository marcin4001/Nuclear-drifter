using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
