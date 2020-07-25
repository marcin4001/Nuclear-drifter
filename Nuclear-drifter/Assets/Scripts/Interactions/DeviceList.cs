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
}
