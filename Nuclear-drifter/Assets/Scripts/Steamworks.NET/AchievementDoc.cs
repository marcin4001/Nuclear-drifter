using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDoc : MonoBehaviour
{
    public int id_ach = 7;
    public int idItem = 100;
    public int secoundIdItem = 103;
    private Achievement achievement;
    // Start is called before the first frame update
    void Start()
    {
        achievement = FindObjectOfType<Achievement>();
    }

    public void Check(int _idItem)
    {
        if (idItem == _idItem || secoundIdItem == _idItem)
            achievement.SetAchievement(id_ach);
    }
}
