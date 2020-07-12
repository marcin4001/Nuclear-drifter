using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private Canvas dialCanvas;
    //private bool testB = false;
    private MapControl map;
    private PauseMenu menu;
    public NPCBasic npc;

    public Text nameText;
    public Text jobText;
    public Text cityText;

    public Text replyText;
    public DialChoice[] choices;
    public bool active = false;
    private TypeScene typeSc;
    // Start is called before the first frame update
    void Start()
    {
        dialCanvas = GetComponent<Canvas>();
        map = FindObjectOfType<MapControl>();
        menu = FindObjectOfType<PauseMenu>();
        dialCanvas.enabled = false;
        typeSc = FindObjectOfType<TypeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Close();
        }
    }

    public void Close()
    {
        dialCanvas.enabled = false;
        map.keyActive = true;
        menu.activeEsc = true;
        active = false;
        
        typeSc.SetInMenu();
    }

    public void OpenDialogue(NPCBasic _NPC)
    {
        npc = _NPC;
        dialCanvas.enabled = true;
        map.keyActive = false;
        menu.activeEsc = false;
        active = true;
        typeSc.SetInMenu();
        SetDialogueStart();
        
    }

    private void SetDialogueStart()
    {
        if(npc != null)
        {
            nameText.text = npc.nameNPC;
            jobText.text = npc.job;
            cityText.text = npc.city;
            if(npc.GetInit())
            {
                replyText.text = npc.firstReply;
                npc.SetInit();
            }
            else
            {
                replyText.text = npc.cbReply;
            }
            
            DialogueModule module = npc.modules[npc.startIndex];
            for(int i = 0; i < choices.Length; i++)
            {
                if(i < module.dialogues.Length)
                {
                    choices[i].SetChoice(module.dialogues[i]);
                }
                else
                {
                    choices[i].Clear();
                }
            }
        }
    }

    public void SetDialogue(Dialogue d)
    {
        if (npc != null)
        {
            if(d.nextModule >= npc.modules.Length  || d.nextModule < 0)
            {
                Close();
                return;
            }
            int index = d.nextModule;
            if (index == 0) index = npc.startIndex;
            replyText.text = d.reply;
            DialogueModule module = npc.modules[index];
            for (int i = 0; i < choices.Length; i++)
            {
                if (i < module.dialogues.Length)
                {
                    choices[i].SetChoice(module.dialogues[i]);
                }
                else
                {
                    choices[i].Clear();
                }
            }
        }
    }

}
