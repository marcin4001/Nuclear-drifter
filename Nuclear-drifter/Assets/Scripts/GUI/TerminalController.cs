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

    // Start is called before the first frame update
    void Start()
    {
        terminalCanvas = GetComponent<Canvas>();
        pause = FindObjectOfType<PauseMenu>();
        typeSc = FindObjectOfType<TypeScene>();
        sound = FindObjectOfType<SoundsTrigger>();
        terminalCanvas.enabled = false;
        active = false;
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
        }
    }

    public void StartTerminal()
    {
        terminalCanvas.enabled = true;
        active = true;
        pause.activeEsc = false;
        typeSc.inMenu = true;
        counter = 0;
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
                text += "B:\\>";
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
                text += "B:\\>";
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
        terminalCanvas.enabled = false;
        active = false;
        pause.activeEsc = true;
        typeSc.inMenu = false;
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
            text = "\n";
            text += "Welcome to the Uranium Deposit Door Control System!\n";
            text += "Logged in as: AdminBunny\n";
            text += "Do you want to open the door to the deposits? (Y/N): ";
            state = TerminalState.working;
        }
        if (state == TerminalState.y)
        {
            text = "\n";
            text += "The doors were opened...\n";
            text += "B:\\>";
            state = TerminalState.end;
        }
    }
}
