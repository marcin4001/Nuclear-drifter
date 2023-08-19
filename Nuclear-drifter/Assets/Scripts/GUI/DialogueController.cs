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
    public GameObject blockEndButton;
    public List<int> idMissionsEndGame;
    private TypeScene typeSc;
    private SoundsTrigger sound;
    private TalkingHeadController head;
    // Start is called before the first frame update
    void Start()
    {
        dialCanvas = GetComponent<Canvas>();
        map = FindObjectOfType<MapControl>();
        menu = FindObjectOfType<PauseMenu>();
        dialCanvas.enabled = false;
        typeSc = FindObjectOfType<TypeScene>();
        sound = FindObjectOfType<SoundsTrigger>();
        head = FindObjectOfType<TalkingHeadController>();
        if(blockEndButton != null)
        {
            blockEndButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) && !blockEndButton.activeSelf)
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
        if(head != null) head.Close();
    }

    public void CloseCanvas()
    {
        dialCanvas.enabled = false;
        active = false;
        typeSc.inMenu = false;
        if (head != null) head.Close();
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
        if (head != null) head.SetHead(_NPC);
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
                replyText.text = CheckDot(npc.firstReply); 
                if(npc.firstReplyClip != null && head != null)
                    head.Play(npc.firstReplyClip);
                npc.SetInit();
            }
            else
            {
                replyText.text = CheckDot(npc.cbReply);
                if (npc.cbReplyClip != null && head != null)
                    head.Play(npc.cbReplyClip);
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
                else MissionList.global.StartMission(d.startIdMission);
            }
            if (d.missionEnd)
            {
                if (mission != null)
                {
                    if(idMissionsEndGame.Contains(d.endIdMission))
                        blockEndButton.SetActive(true);
                    mission.CompleteMission(d.endIdMission);
                }
            }
            if (d.isWorker && npc.nPCJob != null)
            {
                replyText.text = CheckDot(npc.nPCJob.Work(d.workOpt));
                if (replyText.text == "")
                {
                    replyText.text = CheckDot(d.reply);
                    if(d.replyClip != null && head != null)
                        head.Play(d.replyClip);
                    if(d.checkEmptyReplyWork)
                    {
                        npc.SetStartIndex(d.nextModuleWork);
                    }
                }
            }
            else
            {
                replyText.text = CheckDot(d.reply);
                if (d.replyClip != null && head != null)
                    head.Play(d.replyClip);
            }
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

    public string CheckDot(string text)
    {
        if (text == "")
            return text;
        if (text.EndsWith('.') || text.EndsWith('!') || text.EndsWith('?') || text.EndsWith(')'))
            return text;
        else
            return text + ".";
    }
}
