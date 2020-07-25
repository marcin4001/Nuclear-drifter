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

    public Slot slotItem;
    public bool needItem;

    public int indexDevice;
}
