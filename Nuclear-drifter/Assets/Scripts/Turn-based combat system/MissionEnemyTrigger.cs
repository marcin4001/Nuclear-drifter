﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEnemyTrigger : MonoBehaviour
{
    public int idEnemy = 0;
    public EnemyMission enemyMission;
    // Start is called before the first frame update
    void Start()
    {
        enemyMission = EnemyMissionList.global.GetEnemy(idEnemy);
        if(enemyMission != null)
        {
            if (enemyMission.GetIsKilled())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetKill()
    {
       if(enemyMission != null) enemyMission.Kill();
    }
}