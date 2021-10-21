using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPoison : MonoBehaviour
{
    public string nameEnemy;
    public int ach_id = 8;
    private Achievement achievement;
    // Start is called before the first frame update
    void Start()
    {
        achievement = FindObjectOfType<Achievement>();
    }

    public void Check(string _name)
    {
        if (nameEnemy == _name)
            achievement.SetAchievement(ach_id);
    }
}
