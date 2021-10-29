using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlockNPCs : MonoBehaviour
{
    public Carpet door;
    public int[] indexGlobalNPCs;
    // Start is called before the first frame update
    void Start()
    {
        if (door == null || indexGlobalNPCs.Length != 0)
            Destroy(gameObject);

        bool result = true;
        foreach(int i in indexGlobalNPCs)
        {
            NPCElement nPC = MissionList.global.GetNPC(i);
            if(nPC != null)
            {
                result &= (nPC.startIndex > 0);
                Debug.Log(nPC.npcName);
            }
            else
            {
                result &= true;
                Debug.Log("NPC no found");
            }
        }

        if(result)
        {
            door.isLock = false;
            door.closeInNight = true;
        }
        else
        {
            door.isLock = true;
            door.closeInNight = false;
        }

        Debug.Log("Result: " + result);
    }

}
