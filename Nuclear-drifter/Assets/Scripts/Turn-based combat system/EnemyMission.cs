using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyMission 
{
    public string nameEnemy;
    public int idMission;
    public bool isKilled = false;
    
    public void Kill()
    {
        isKilled = true;
    }

    public void Reset()
    {
        isKilled = false;
    }

    public bool GetIsKilled()
    {
        return isKilled;
    }
}
