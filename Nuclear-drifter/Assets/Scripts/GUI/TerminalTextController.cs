using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalTextController : MonoBehaviour
{
    private Canvas terminalCanvas;
    public bool active = false;
    private PauseMenu pause;
    public int counter = 0;
    public Text mainText;
    [TextArea(5,7)]
    public string text;
    private TypeScene typeSc;
    private SoundsTrigger sound;
    private bool addCR = false;
    private float counterF = 0f;
    public float counterFmax = 0.5f;
    public Text timeText;
    public GameObject StartPanel;
    public GameObject StartButtonPressed;

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
        exp = FindObjectOfType<Experience>();
        map = FindObjectOfType<MapControl>();
        if (StartPanel != null)
            StartPanel.SetActive(false);
        if (StartButtonPressed != null)
            StartButtonPressed.SetActive(false);
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
        }
    }

    public void StartTerminal()
    {
        terminalCanvas.enabled = true;
        active = true;
        pause.activeEsc = false;
        typeSc.inMenu = true;
        map.keyActive = false;
        if (StartPanel != null)
            StartPanel.SetActive(false);
        if (StartButtonPressed != null)
            StartButtonPressed.SetActive(false);
    }

    public void Exit()
    {
        sound.PlayClickButton();
        terminalCanvas.enabled = false;
        active = false;
        pause.activeEsc = true;
        typeSc.inMenu = false;
        map.keyActive = true;
    }

    public void ToggleStartPanel()
    {
        if (StartPanel.activeSelf)
        {
            sound.PlayClickButton();
            StartPanel.SetActive(false);
            if (StartButtonPressed != null)
                StartButtonPressed.SetActive(false);
        }
        else
        {
            sound.PlayClickButton();
            StartPanel.SetActive(true);
            if (StartButtonPressed != null)
                StartButtonPressed.SetActive(true);
        }
    }
}
