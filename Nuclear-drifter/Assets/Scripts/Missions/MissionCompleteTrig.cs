using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCompleteTrig : MonoBehaviour
{
    public int idMission;
    private Experience exp;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj mission = MissionList.global.GetMission(idMission);
        if (mission != null)
        {
            if (mission.complete) Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        exp = FindObjectOfType<Experience>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            MissionObj mission = MissionList.global.GetMission(idMission);
            if (mission != null)
            {
                mission.start = true;
                mission.complete = true;
                if (exp != null)
                    exp.AddExp(mission.exp);
            }
        }
        Destroy(gameObject);
    }
}
