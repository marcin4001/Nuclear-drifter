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
        //if(!SteamManager.Initialized)
        //{
        //    Destroy(gameObject);
        //}
    }
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Weszło");
            SteamUserStats.ResetAllStats(true);
            SteamUserStats.StoreStats();
        }
    }
#endif
    public void SetAchievement(int id)
    {
        if(names.Length != 0 && id < names.Length && SteamManager.Initialized)
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
