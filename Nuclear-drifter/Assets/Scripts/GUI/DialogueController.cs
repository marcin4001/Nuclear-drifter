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
    public MissionNPC mission;

    public Text nameText;
    public Text jobText;
    public Text cityText;

    public Text replyText;
    public DialChoice[] choices;
    public bool active = false;
    private TypeScene typeSc;
    private SoundsTrigger sound;
    // Start is called before the first frame update
    void Start()
    {
        dialCanvas = GetComponent<Canvas>();
        map = FindObjectOfType<MapControl>();
        menu = FindObjectOfType<PauseMenu>();
        dialCanvas.enabled = false;
        typeSc = FindObjectOfType<TypeScene>();
        sound = FindObjectOfType<SoundsTrigger>();
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
        sound.PlayClickButton();
        dialCanvas.enabled = false;
        map.keyActive = true;
        menu.activeEsc = true;
        active = false;
        
        typeSc.inMenu = false;
    }

    public void CloseCanvas()
    {
        dialCanvas.enabled = false;
        active = false;
        typeSc.inMenu = false;
    }

    public void OpenDialogue(NPCBasic _NPC)
    {
        npc = _NPC;
        mission = _NPC.GetMission();
        dialCanvas.enabled = true;
        map.keyActive = false;
        menu.activeEsc = false;
        active = true;
        typeSc.inMenu = true;
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
            if (mission != null) mission.CheckMission();
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
            if(d.nextModule == 100)
            {
                EndGameDialogue end = npc.GetComponent<EndGameDialogue>();
                if (end != null)
                    end.LoadEnd();
                return;
            }
            if(d.nextModule >= npc.modules.Length  || d.nextModule < 0)
            {
                Close();
                return;
            }
            if(d.changeStartIndex)
            {
                npc.SetStartIndex(d.nextModule);
            }
            if(d.missionStart)
            {
                if (mission != null) mission.StartMission(d.startIdMission);
            }
            if (d.missionEnd)
            {
                if (mission != null) mission.CompleteMission(d.endIdMission);
            }
            if(d.isWorker && npc.nPCJob != null)
            {
                replyText.text = npc.nPCJob.Work(d.workOpt);
                if (replyText.text == "") replyText.text = d.reply;
            }
            else replyText.text = d.reply;
            int index = d.nextModule;
            if (index == 0) index = npc.startIndex;
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
