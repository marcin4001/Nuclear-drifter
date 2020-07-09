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
    // Start is called before the first frame update
    void Start()
    {
        dialCanvas = GetComponent<Canvas>();
        map = FindObjectOfType<MapControl>();
        menu = FindObjectOfType<PauseMenu>();
        dialCanvas.enabled = false;
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
    }

    public void OpenDialogue(NPCBasic _NPC)
    {
        npc = _NPC;
        dialCanvas.enabled = true;
        map.keyActive = false;
        menu.activeEsc = false;
        active = true;
        SetDialogueStart();

    }

    private void SetDialogueStart()
    {
        if(npc != null)
        {
            nameText.text = npc.nameNPC;
            jobText.text = npc.job;
            cityText.text = npc.city;
            replyText.text = npc.firstReply;
            DialogueModule module = npc.modules[0];
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
            replyText.text = d.reply;
            DialogueModule module = npc.modules[d.nextModule];
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
