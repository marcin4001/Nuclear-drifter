using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementStart : MonoBehaviour
{
    public int id_ach;
    private Achievement achievement;
    // Start is called before the first frame update
    void Start()
    {
        achievement = FindObjectOfType<Achievement>();
        if (achievement != null)
            achievement.SetAchievement(id_ach);
    }

}
