using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public int idMission = 0;
    public GameObject gateOpen;
    public GameObject gateClose;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj mission = MissionList.global.GetMission(idMission);
        if (mission == null) Destroy(this);
        if (mission.complete)
        {
            Destroy(gameObject);
            Destroy(gateClose);
        }
        else
        {
            Destroy(gateOpen);
        }
    }
}
