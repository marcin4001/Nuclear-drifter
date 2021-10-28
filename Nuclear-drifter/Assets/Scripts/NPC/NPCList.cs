using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCList : MonoBehaviour
{
    public NPCElement[] nPCs;
    private void Awake()
    {
        SaveAndLoad.LoadTemp(this);
    }

    public NPCElement GetNPC(int index)
    {
        return nPCs[index];
    }

    public void SetNPC(int index, bool _init, int _startIndex)
    {
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
