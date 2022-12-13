using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLastMission : MonoBehaviour
{
    public int preLastMissionId = 31;
    public int lastMissionId = 32;
    private MissionObj preLastMission;
    private MissionObj lastMission;
    private MissionTextGUI textGUI;
    void Start()
    {
        preLastMission = MissionList.global.GetMission(preLastMissionId);
        lastMission = MissionList.global.GetMission(lastMissionId);
        if(lastMission != null && preLastMission != null)
        {
            if (lastMission.start)
                Destroy(gameObject);
            else
                textGUI = FindObjectOfType<MissionTextGUI>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero" && preLastMission.complete)
        {
            lastMission.start = true;
            textGUI.Show("New Mission: " + lastMission.task);
            Destroy(gameObject);
        }
    }
}
