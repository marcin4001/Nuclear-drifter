using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    public static DevTools dev;
    public bool activeFPSCounter = false;

    void Awake()
    {
        if (!dev)
        {
            DontDestroyOnLoad(this.gameObject);
            dev = this;
        }
        else
            Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            activeFPSCounter = !activeFPSCounter;
        }
    }
}
