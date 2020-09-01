﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeScene : MonoBehaviour
{
    public bool isInterior = false;
    public bool combatState = false;
    public bool inBox = false;
    public bool radZone = false;
    public bool inMenu = false;

    private PauseMenu pause;
    private MapControl map;
    private DialogueController dial;
    private MissionListGUI mission;
    void Start()
    {
        pause = FindObjectOfType<PauseMenu>();
        map = FindObjectOfType<MapControl>();
        dial = FindObjectOfType<DialogueController>();
        mission = FindObjectOfType<MissionListGUI>();
    }

    public void SetInMenu()
    {
        inMenu = (pause.GetActive() || map.GetActive() || dial.active || mission.GetActive());
    }
}
