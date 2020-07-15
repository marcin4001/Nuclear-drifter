﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBasic : MonoBehaviour
{
    public string nameNPC;
    public string job;
    public string city;
    [TextArea(2,3)]
    public string firstReply;
    [TextArea(2, 3)]
    public string cbReply;
    private bool init = true;
    public DialogueModule[] modules;
    public int startIndex = 0;
    private DialogueController controller;
    private PlayerClickMove player;
    
    private GUIScript gUI;
    private ChangeDirectionNPC dir;
    private NPCList listNPC;
    public int indexNPC;
    private MissionNPC mission;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<DialogueController>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        dir = GetComponent<ChangeDirectionNPC>();
        listNPC = FindObjectOfType<NPCList>();
        if(listNPC != null)
        {
            NPCElement data = listNPC.GetNPC(indexNPC);
            init = data.init;
            startIndex = data.startIndex;
            data.npcName = nameNPC;
        }
        mission = GetComponent<MissionNPC>();
    }

    public void ShowText()
    {
        gUI.AddText("This is " + nameNPC);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O) && !controller.active)
        //{
        //    SetStartIndex(1);
        //}
    }

    public void ResetPos()
    {
        if (dir != null) dir.ResetPos();
    }

    public void Use()
    {
        
        if (player.ObjIsNearPlayer(transform.parent.position, 1.1f)) {
            if (dir != null)
            {
                Vector3 dirNPC = player.transform.position - transform.parent.position;
                Debug.Log(dirNPC);
                dir.SetDir((Vector2)dirNPC);
                Vector3 dirPlayer = transform.parent.position - player.transform.position;
                player.SetDir((Vector2)dirPlayer);
                Debug.Log(dirPlayer);
            }
            controller.OpenDialogue(this);
        }
        else gUI.AddText(nameNPC + " is too far away");
    }

    public void SetInit()
    {
        init = false;
        if (listNPC != null) listNPC.SetNPC(indexNPC, init, startIndex);
    }

    public void SetStartIndex(int index)
    {
        startIndex = index;
        if (listNPC != null) listNPC.SetNPC(indexNPC, init, index);
    }

    public bool GetInit()
    {
        return init;
    }

    public MissionNPC GetMission()
    {
        return mission;
    }
}
