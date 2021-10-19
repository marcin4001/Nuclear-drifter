using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    item,
    device,
    kill
}

[System.Serializable]
public class MissionDetails
{
    public int id;
    public MissionType type;
    public int dialogSuccess;
    public int dialogNormal;
    public int dialogComplete;
    public int dialogAlt;

    public Slot slotItem;
    public bool needItem;
    public bool removeAll;
    public int indexDevice;
    public bool noRemove;
    public bool killAlt;
    public int id_ach = -1;
}
