using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBoatTrigger : MonoBehaviour
{
    public int idMission = 34;
    public string[] texts;
    public MissionObj mission;
    private GUIScript gUI;
    private MissionTextGUI missionText;

    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        missionText = FindObjectOfType<MissionTextGUI>();
        mission = MissionList.global.GetMission(idMission);
        if (mission == null)
            Destroy(gameObject);
        if(mission.start)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            mission.start = true;
            if (missionText != null)
                missionText.Show();
            gUI.ClearText();
            foreach (string text in texts)
                gUI.AddText(text);
            gUI.ShowWarning();
            Destroy(gameObject);
        }
    }
}
