using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceList : MonoBehaviour
{
    public DeviceElement[] devices;
    public static DeviceList global;

    private void Awake()
    {
        if (!global)
        {
            DontDestroyOnLoad(this.gameObject);
            global = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public DeviceElement GetDevice(int index)
    {
        if (devices.Length == 0) return null;
        if (index >= 0 && index < devices.Length)
        {
            return devices[index];
        }
        else return null;
    }
}
