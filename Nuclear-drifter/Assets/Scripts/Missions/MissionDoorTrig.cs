using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDoorTrig : MonoBehaviour
{
    public Carpet door;
    public int idMission = 0;
    public bool closeInNight = true;
    // Start is called before the first frame update
    void Start()
    {
        if (door == null) Destroy(gameObject);
        MissionObj mission = MissionList.global.GetMission(idMission);
        if (mission != null)
        {
            if (!mission.complete)
            {
                door.isLock = true;
                door.closeInNight = false;
            }
            else
            {
                door.isLock = false;
                door.closeInNight = closeInNight;
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            MissionObj mission = MissionList.global.GetMission(idMission);
            if (mission != null)
            {
                if (!mission.complete)
                {
                    door.isLock = true;
                    door.closeInNight = false;
                }
                else
                {
                    door.isLock = false;
                    door.closeInNight = closeInNight;
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
