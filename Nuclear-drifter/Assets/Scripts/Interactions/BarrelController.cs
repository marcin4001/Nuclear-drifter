using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public int idMission = 13;
    public Slot itemSlot;
    public Barrel[] barrels;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj mission = MissionList.global.GetMission(idMission);
        if(mission != null)
        {
            if(!mission.start && !mission.complete)
            {
                foreach (Barrel barrel in barrels)
                    barrel.gameObject.SetActive(false);
                return;
            }
        }
        else
        {
            foreach (Barrel barrel in barrels)
                barrel.gameObject.SetActive(false);
            return;
        }

        foreach (Barrel barrel in barrels)
            barrel.isEmpty = true;

        if(barrels.Length > 0)
        {
            int index = Random.Range(0, barrels.Length);
            barrels[index].isEmpty = false;
        }
    }

    public Slot GetItem()
    {
        return itemSlot;
    }
}
