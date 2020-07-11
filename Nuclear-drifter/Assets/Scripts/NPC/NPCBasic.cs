using System.Collections;
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
    private DialogueController controller;
    private PlayerClickMove player;
    
    private GUIScript gUI;


    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<DialogueController>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
    }

    public void ShowText()
    {
        gUI.AddText("This is " + nameNPC);
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.O) && !controller.active)
        //{
        //    Use();
        //}
    }

    public void Use()
    {
        if (player.ObjIsNearPlayer(transform.parent.position, 1.1f)) controller.OpenDialogue(this);
        else gUI.AddText(nameNPC + " is too far away");
    }

    public void SetInit()
    {
        init = false;
    }

    public bool GetInit()
    {
        return init;
    }
}
