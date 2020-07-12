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
    public int startIndex = 0;
    private DialogueController controller;
    private PlayerClickMove player;
    
    private GUIScript gUI;
    private ChangeDirectionNPC dir;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<DialogueController>();
        player = FindObjectOfType<PlayerClickMove>();
        gUI = FindObjectOfType<GUIScript>();
        dir = GetComponent<ChangeDirectionNPC>();
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
    }

    public bool GetInit()
    {
        return init;
    }
}
