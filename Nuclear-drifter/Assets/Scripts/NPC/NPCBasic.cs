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

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O) && !controller.active)
        {
            Use();
        }
    }

    public void Use()
    {
        controller.OpenDialogue(this);
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
