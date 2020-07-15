using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionObj
{
    public string task;
    public string ownerName;
    public string location;
    public bool start;
    public bool complete;

    public void ResetObj()
    {
        start = false;
        complete = false;
    }
}
