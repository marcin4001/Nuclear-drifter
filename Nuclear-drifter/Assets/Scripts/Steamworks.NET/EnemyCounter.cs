using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyCounter
{
    public string name;
    public int counter = 0;
    public int counterMax = 0;
    public int ach_id;

    public void AddPoint(Achievement achievement)
    {
        counter += 1;
        if (counter >= counterMax)
            achievement.SetAchievement(ach_id);
    }

    public void Clear()
    {
        counter = 0;
    }
}
