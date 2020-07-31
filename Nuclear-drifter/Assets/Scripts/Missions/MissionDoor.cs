using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDoor : MonoBehaviour
{
    public Carpet door;
    public int idMission = 0;
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
            }
            else
            {
                door.isLock = false;
            }
        }
        Destroy(gameObject);

    }

}
