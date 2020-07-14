using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionList : MonoBehaviour
{
    public static MissionList global;
    public MissionObj[] missions;
    // Start is called before the first frame update
    private void Awake()
    {
        if(!global)
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
