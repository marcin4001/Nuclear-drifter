using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStart : MonoBehaviour
{
    public List<MissionInit> inits;
    public List<MissionItem> missionItems;
    // Start is called before the first frame update
    void Start()
    {
        if (inits == null) inits = new List<MissionInit>();
        if (missionItems == null) missionItems = new List<MissionItem>();
        else
        {
            Invoke("DeactivationItem", 0.1f);
        }
    }

    private void DeactivationItem()
    {
        foreach (MissionItem item in missionItems)
        {
            MissionObj mission = MissionList.global.GetMission(item.missionid);
            if (mission != null)
            {
                if (!mission.start && item.item != null)
                    item.item.SetActive(false);
            }
        }
    }

    public void Init(int id)
    {
        MissionInit init = inits.Find(x => x.id == id);
        if(init != null)
        {
            init.box.LockOpen();
        }
        MissionItem item = missionItems.Find(x => x.missionid == id);
        if(item != null)
        {
            if(item.item != null)item.item.SetActive(true);
        }
    }
}
