using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(2,4)]
    public string choice = "";
    [TextArea(2,4)]
    public string reply = "";
    public int nextModule = 0;
    public bool missionStart = false;
    public int startIdMission = 0;
    public bool missionEnd = false;
    public int endIdMission = 0;
    public bool changeStartIndex;
    public bool isWorker = false;
    public int workOpt = 0;
}
