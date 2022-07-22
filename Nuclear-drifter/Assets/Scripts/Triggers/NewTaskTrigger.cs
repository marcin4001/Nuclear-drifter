using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTaskTrigger : MonoBehaviour
{
    public int idMission;
    private MissionTextGUI textGUI;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj mission = MissionList.global.GetMission(idMission);
        if (mission == null)
            Destroy(gameObject);
        if (mission.complete)
            Destroy(gameObject);
        textGUI = FindObjectOfType<MissionTextGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hero")
        {
            textGUI.Show();
            Destroy(gameObject);
        }
    }
}
