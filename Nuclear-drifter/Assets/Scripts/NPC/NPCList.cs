using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCList : MonoBehaviour
{
    public NPCElement[] nPCs;
    private void Awake()
    {
        SaveAndLoad.LoadTemp(this);
    }

    public NPCElement GetNPC(int index)
    {
        if(index >= nPCs.Length)
        {
            List<NPCElement> list = nPCs.ToList();
            for (int i = list.Count; i <= index; i++)
            {
                list.Add(new NPCElement());
            }
            nPCs = list.ToArray();
        }
        return nPCs[index];
    }

    public void SetNPC(int index, bool _init, int _startIndex)
    {
        if (index >= nPCs.Length)
        {
            List<NPCElement> list = nPCs.ToList();
            for (int i = list.Count; i <= index; i++)
            {
                list.Add(new NPCElement());
            }
            nPCs = list.ToArray();
        }
        nPCs[index].init = _init;
        nPCs[index].startIndex = _startIndex;
        if (nPCs[index].indexGlobal >= 0)
            MissionList.global.SetNPC(nPCs[index].indexGlobal, _init, _startIndex);
    }

    public void SetHaveRespect(int index)
    {
        nPCs[index].haveRespect = true;
        if (nPCs[index].indexGlobal >= 0)
            MissionList.global.SetHaveRespect(nPCs[index].indexGlobal);
    }
}
