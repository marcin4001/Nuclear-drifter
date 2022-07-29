using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerMine : Job
{
    private MineStartTextMap textMap;
    // Start is called before the first frame update
    void Start()
    {
        textMap = FindObjectOfType<MineStartTextMap>();
    }

    public override string Work(int opt)
    {
        textMap.SetActive();
        return "";
    }
}
