using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionItemTrig : MonoBehaviour
{
    public int idMission;
    public Item item;
    private Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj mission = MissionList.global.GetMission(idMission);
        if(mission != null)
        {
            if (mission.start) Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (item == null) Destroy(gameObject);
        inv = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(inv.FindItemB(item.idItem) && collision.tag == "Hero")
        {
            MissionObj mission = MissionList.global.GetMission(idMission);
            if (mission != null)
            {
                mission.start = true;
            }
        }
        Destroy(gameObject);
    }

}
