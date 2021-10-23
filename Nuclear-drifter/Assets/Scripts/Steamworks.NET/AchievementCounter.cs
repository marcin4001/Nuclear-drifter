using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCounter : MonoBehaviour
{
    public static AchievementCounter global;
    public List<string> areas;
    public int maxAreaCounter = 10;
    public int ach_id_area = 9;
    public List<EnemyCounter> counters;
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

    public void AddArea(string area)
    {
        if (areas == null)
            areas = new List<string>();
        if (!areas.Contains(area))
            areas.Add(area);
        if(areas.Count >= maxAreaCounter)
        {
            Achievement achievement = FindObjectOfType<Achievement>();
            achievement.SetAchievement(ach_id_area);
        }
    }

    public void AddEnemies(string nameEnemy)
    {
        EnemyCounter counter = counters.Find(e => e.name == nameEnemy);
        if(counter != null)
        {
            Achievement achievement = FindObjectOfType<Achievement>();
            counter.AddPoint(achievement);
        }
    }

    public void Clear()
    {
        if (areas != null)
            areas.Clear();
        foreach(EnemyCounter counter in counters)
        {
            counter.Clear();
        }
    }

    public static string GetJson()
    {
        if (global.areas == null)
            global.areas = new List<string>();
        string json = JsonUtility.ToJson(global);
        return json;
    }

    public static void JsonToObj(string json)
    {
        JsonUtility.FromJsonOverwrite(json, global);
    }
}
