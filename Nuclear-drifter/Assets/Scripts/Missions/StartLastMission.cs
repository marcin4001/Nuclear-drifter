using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLastMission : MonoBehaviour
{
    public int preLastMissionId = 31;
    public int lastMissionId = 32;
    public int secoundMission = 25;
    public string[] textSecoundMissionStart;
    public int respect = 75;
    public bool withoutInfo = false;
    public NPCBasic npcToSetRespect;
    private MissionObj preLastMission;
    private MissionObj lastMission;
    private MissionTextGUI textGUI;
    private GUIScript gUI;
    void Start()
    {
        preLastMission = MissionList.global.GetMission(preLastMissionId);
        lastMission = MissionList.global.GetMission(lastMissionId);
        if(lastMission != null && preLastMission != null)
        {
            if (lastMission.start)
            {
                if (npcToSetRespect != null)
                    npcToSetRespect.SetHaveRespect();
                Destroy(gameObject);
                return;
            }
            else
            {
                textGUI = FindObjectOfType<MissionTextGUI>();
                gUI = FindObjectOfType<GUIScript>();
            }
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
            int currentRespect = MissionList.global.PercentRespect();
            if(currentRespect < respect)
            {
                Destroy(gameObject);
                return;
            }
            lastMission.start = true;
            if (npcToSetRespect != null)
                npcToSetRespect.SetHaveRespect();
            if (withoutInfo)
            {
                Destroy(gameObject);
                return;
            }
            textGUI.Show("New Mission: " + lastMission.task);

            MissionObj secound_mission = MissionList.global.GetMission(secoundMission);
            if(secound_mission != null)
            {
                if(secound_mission.start)
                {
                    gUI.ShowWarning();
                    gUI.ClearText();
                    foreach(string text in textSecoundMissionStart)
                    {
                        gUI.AddText(text);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
