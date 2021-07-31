using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeviceElement
{
    public string nameDevice;
    public bool repair;
    public bool switchOn;
    public int uses;

    public void Reset()
    {
        repair = false;
        switchOn = false;
    }
}
