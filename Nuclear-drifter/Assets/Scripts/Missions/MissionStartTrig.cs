using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStartTrig : MonoBehaviour
{
    public int idMission;
    private MissionTextGUI missionText;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj mission = MissionList.global.GetMission(idMission);
        if (mission != null)
        {
            if (mission.start) Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        missionText = FindObjectOfType<MissionTextGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            MissionObj mission = MissionList.global.GetMission(idMission);
            if (mission != null)
            {
                mission.start = true;
                if (missionText != null)
                    missionText.Show();
            }
        }
        Destroy(gameObject);
    }
}
