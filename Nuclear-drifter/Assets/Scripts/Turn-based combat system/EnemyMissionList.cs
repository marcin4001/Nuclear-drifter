using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public EnemyMission GetEnemy(int id, MissionEnemyTrigger trigger)
    {
        if (enemies.Length <= 0) return null;
        if (id < 0) return null;
        EnemyMission enemy = null;
        if (id < enemies.Length)
        {
            enemy = enemies[id];
            Debug.Log(1);
        }
        else
        {
            List<EnemyMission> enemyList = enemies.ToList();
            while (id >= enemyList.Count)
            {
                enemyList.Add(new EnemyMission());
            }
            enemies = enemyList.ToArray();
            enemy = enemies[id];
            Debug.Log(-1);
        }
        if (enemy.nameEnemy == "" || enemy.nameEnemy == null)
        {
            Debug.Log(trigger.nameEnemy);
            enemy.nameEnemy = trigger.nameEnemy;
            enemy.idMission = trigger.idMission;
        }
        return enemy;
        //if (id >= 0 && id < enemies.Length)
        //{
        //    return enemies[id];
        //}
        //else
        //{
        //    return null;
        //} 
    }

    public int HowMunyAlive(int id_mission)
    {
        if (enemies.Length <= 0) return 0;
        int count = 0;
        foreach(EnemyMission enemy in enemies)
        {
            if (enemy.idMission == id_mission && !enemy.isKilled)
                count += 1;
        }
        return count;
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //        Debug.Log(HowMunyAlive(23));
    //}
    public static string GetJson()
    {
        string json = JsonUtility.ToJson(global);
        return json;
    }

    public static void JsonToObj(string json)
    {
        JsonUtility.FromJsonOverwrite(json, global);
    }
}
