﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public int currentExp = 0;
    public int level = 1;
    public int prevTh = 0;
    private int constExp = 250;
    private GUIScript gUI;
    // Start is called before the first frame update
    void Start()
    {
        currentExp = PropertyPlayer.property.currentExp;
        level = PropertyPlayer.property.level;
        prevTh = PropertyPlayer.property.prevTh;
        gUI = FindObjectOfType<GUIScript>();
        gUI.ShowExp(level, currentExp, (level * constExp + prevTh));
    }

    public void AddExp(int exp)
    {
        if (exp > 0) gUI.AddText("You get " + exp + " exp");
        currentExp = currentExp + exp;
        while(currentExp >= level * constExp + prevTh)
        {
            prevTh = level * constExp + prevTh;
            level = level + 1;
        }
        UpdateProperty();
        gUI.ShowExp(level, currentExp, (level * constExp + prevTh));
    }

    private void UpdateProperty()
    {
        PropertyPlayer.property.currentExp = currentExp;
        PropertyPlayer.property.level = level;
        PropertyPlayer.property.prevTh = prevTh;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        AddExp(50);
    //    }
    //}
}