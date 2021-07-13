using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionObj
{
    public string task;
    public string alt;
    public string ownerName;
    public string location;
    public bool start;
    public bool complete;
    public int idSort;
    public int exp = 300;
    public int respect = 0;
    public int respectUSA = 0;
    public int idHelp = 0;

    public void ResetObj()
    {
        start = false;
        complete = false;
    }
}
