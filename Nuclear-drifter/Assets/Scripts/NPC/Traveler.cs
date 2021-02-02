﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveler : Job
{
    private SignController sign;
    // Start is called before the first frame update
    void Start()
    {
        sign = FindObjectOfType<SignController>();
    }

    public override string Work(int opt)
    {
        if(opt == -1)
            FindObjectOfType<SubwayIcon>().SetIconActive();
        else
            sign.FindArea(opt);
        return "";
    }
}
