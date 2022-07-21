using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOpenDoorMission : MonoBehaviour
{
    public int idMission;
    public Carpet door;
    private GUIScript gUI;

    // Start is called before the first frame update
    void Start()
    {
        int aliveEnemy = EnemyMissionList.global.HowMunyAlive(idMission);
        if (aliveEnemy <= 0)
        {
            door.isLock = false;
            Destroy(gameObject);
        }
        else
        {
            door.isLock = true;
        }
        gUI = FindObjectOfType<GUIScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hero")
        {
            int aliveEnemy = EnemyMissionList.global.HowMunyAlive(idMission);
            if(aliveEnemy <= 0)
            {
                door.isLock = false;
                gUI.AddText("You hear the door lock");
                gUI.AddText("open...");
                Destroy(gameObject);
            }
        }
    }
}
