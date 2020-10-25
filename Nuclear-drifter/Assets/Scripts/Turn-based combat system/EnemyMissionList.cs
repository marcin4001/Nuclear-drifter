using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissionList : MonoBehaviour
{
    public static EnemyMissionList global;
    public EnemyMission[] enemies;
    
    private void Awake()
    {
        if (!global)
        {
            DontDestroyOnLoad(this.gameObject);
            global = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public EnemyMission GetEnemy(int id)
    {
        if (enemies.Length <= 0) return null;
        if (id >= 0 && id < enemies.Length)
        {
            return enemies[id];
        }
        else
        {
            return null;
        } 
    }
}
