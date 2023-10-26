using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TerminalState
{
    noWorking, working, y, n, end
}

public class TerminalController : MonoBehaviour
{
    public Device[] devices;
    public TerminalState state = TerminalState.noWorking;
    public int missionId;
    public MissionObj mission;
    private Canvas terminalCanvas;
    public bool active = false;
    private PauseMenu pause;
    public int counter = 0;
    public Text mainText;
    public string text;
    private TypeScene typeSc;
    private SoundsTrigger sound;
    private bool addCR = false;
    private float counterF = 0f;
    public float counterFmax = 1f;
    public Text timeText;
    private TimeGame time;
    private Experience exp;
    private MapControl map;
    // Start is called before the first frame update
    void Start()
    {
        terminalCanvas = GetComponent<Canvas>();
        pause = FindObjectOfType<PauseMenu>();
        typeSc = FindObjectOfType<TypeScene>();
        sound = FindObjectOfType<SoundsTrigger>();
        time = FindObjectOfType<TimeGame>();
        terminalCanvas.enabled = false;
        active = false;
        mission = MissionList.global.GetMission(missionId);
        exp = FindObjectOfType<Experience>();
        map = FindObjectOfType<MapControl>();
        if(mission != null)
        {
            if (mission.complete)
                state = TerminalState.end;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if (counterF >= counterFmax)
            {
                counterF = 0f;
                addCR = !addCR;
            }
            else
                counterF += Time.deltaTime;

            if (addCR)
                mainText.text = text + "_";
            else
                mainText.text = text;

            int hour = 0;
            string timeOfDay = "";
            if (time.hour > 12)
            {
                hour = time.hour - 12;
                timeOfDay = " PM";
            }
            else
            {
                hour = time.hour;
                timeOfDay = " AM";
            }
            string timeStr = hour + ":" + string.Format("{0:00}", time.minutes) + timeOfDay;
            timeText.text = timeStr;

            if (Input.GetKeyDown(KeyCode.N))
                NButton();
            if (Input.GetKeyDown(KeyCode.Y))
                YButton();
            if (Input.GetKeyDown(KeyCode.Return))
                EnterButton();
            if (Input.GetKeyDown(KeyCode.End))
                ExitButton();
        }
    }

    public void StartTerminal()
    {
        terminalCanvas.enabled = true;
        active = true;
        pause.activeEsc = false;
        typeSc.inMenu = true;
        counter = 0;
        map.keyActive = false;
        foreach(Device device in devices)
        {
            if(device != null)
            {
                if (device.isFixed)
                    counter += 1;
            }
        }
        if (devices.Length != 0)
        {
            if(state == TerminalState.end)
            {
                text = "\n";
                text += "The doors were opened...\n";
                text += "B:\\Users\\Bunny>";
                return;
            }
            float percent = ((float) counter / (float) devices.Length) * 100f;
            percent = Mathf.Round(percent);
            if (counter < devices.Length)
            {
                text = "\n";
                text += "System not working.\n";
                text += "Server status: " + percent + "%\n";
                text += "Contact the server administration or quit...\n";
                text += "B:\\Users\\Bunny>";
                state = TerminalState.noWorking;
            }
            else
            {
                text = "\n";
                text += "Welcome to the Uranium Deposit Door Control System!\n";
                text += "Logged in as: AdminBunny\n";
                text += "Do you want to open the door to the deposits? (Y/N): ";
                state = TerminalState.working;
            }
        }
    }

    public void ExitButton()
    {
        sound.PlayClickButton();
        terminalCanvas.enabled = false;
        active = false;
        pause.activeEsc = true;
        typeSc.inMenu = false;
        map.keyActive = true;
    }

    public void NButton()
    {
        if(state == TerminalState.noWorking || state == TerminalState.end)
        {
            sound.PlayError();
        }
        else
        {
            text = "\n";
            text += "Welcome to the Uranium Deposit Door Control System!\n";
            text += "Logged in as: AdminBunny\n";
            text += "Do you want to open the door to the deposits? (Y/N): N";
            state = TerminalState.n;
            sound.PlayClickButton();
        }
    }

    public void YButton()
    {
        if (state == TerminalState.noWorking || state == TerminalState.end)
        {
            sound.PlayError();
        }
        else
        {
            text = "\n";
            text += "Welcome to the Uranium Deposit Door Control System!\n";
            text += "Logged in as: AdminBunny\n";
            text += "Do you want to open the door to the deposits? (Y/N): Y";
            state = TerminalState.y;
            sound.PlayClickButton();
        }
    }

    public void EnterButton()
    {
        if (state == TerminalState.noWorking || state == TerminalState.working || state == TerminalState.end)
        {
            sound.PlayError();
        }
        if (state == TerminalState.n)
        {
            //text = "\n";
            //text += "Welcome to the Uranium Deposit Door Control System!\n";
            //text += "Logged in as: AdminBunny\n";
            //text += "Do you want to open the door to the deposits? (Y/N): ";
            ExitButton();
            state = TerminalState.working;
            sound.PlayClickButton();
        }
        if (state == TerminalState.y)
        {
            text = "\n";
            text += "The doors were opened...\n";
            text += "B:\\Users\\Bunny>";
            state = TerminalState.end;
            if (mission != null)
            {
                mission.complete = true;
                if (exp != null)
                    exp.AddExp(mission.exp);
            }
            sound.PlayClickButton();
        }
    }
}
