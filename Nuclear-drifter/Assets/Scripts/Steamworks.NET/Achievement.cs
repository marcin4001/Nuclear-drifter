using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class Achievement : MonoBehaviour
{
    public string[] names;
    // Start is called before the first frame update
    void Start()
    {
        if(!SteamManager.Initialized)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SteamUserStats.ResetAllStats(true);
            SteamUserStats.StoreStats();
        }
    }

    public void SetAchievement(int id)
    {
        if(names.Length != 0 && id < names.Length)
        {
            bool complete = false;
            SteamUserStats.GetAchievement(names[id], out complete);

            if (!complete)
            {
                SteamUserStats.SetAchievement(names[id]);
                SteamUserStats.StoreStats();
            }
        }
    }
}
