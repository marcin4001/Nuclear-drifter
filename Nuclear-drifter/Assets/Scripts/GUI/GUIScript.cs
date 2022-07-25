using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GUIScript : MonoBehaviour
{
    public Text timeLabel;
    public Text consoleLabel;
    public Text expLabel;

    
    public Image radImg;
    public Image bioImg;

    public RectTransform hpBar;

    public Health playerHealth;
    public TimeGame time;

    public PlayerClickMove move;

    public Queue<string> consoleText;

    public bool blockGUI = false;

    public Button pauseBtn;
    public Button missionBtn;
    public Button mapBtn;
    public Button perksBtn;
    public GameObject optBtnPanel;
    public Image newLvlLabel;
    public Image warningLabel;
    private GraphicRaycaster raycaster;
    private EventSystem system;
    private PointerEventData data;
    private TypeScene typeScene;
    private PauseMenu pause;
    private SoundsTrigger sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = FindObjectOfType<SoundsTrigger>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        time = FindObjectOfType<TimeGame>();
        move = FindObjectOfType<PlayerClickMove>();
        consoleLabel.text = "";
        consoleText = new Queue<string>();
        raycaster = GetComponent<GraphicRaycaster>();
        system = FindObjectOfType<EventSystem>();
        typeScene = FindObjectOfType<TypeScene>();
        pause = FindObjectOfType<PauseMenu>();
        if (newLvlLabel != null)
            newLvlLabel.enabled = false;
        if (warningLabel != null)
            warningLabel.enabled = false;
    }

    public void ShowExp(int lvl, int exp, int nextTh)
    {
        expLabel.text = "lvl:" + lvl + " exp:" + exp + "/" + nextTh;
    }

    public void ActiveBtnPanel(bool value)
    {
        optBtnPanel.SetActive(value);
        pause.activeEsc = value;
    }

    public inv_mode GetInvMode()
    {
        return move.modeGui;
    }

    public bool GetCombatState()
    {
        return typeScene.combatState;
    }
    public bool CursorOnSlot()
    {
        data = new PointerEventData(system);
        data.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);
        return results.Exists(s => s.gameObject.tag == "Slot");
    }

    public void ShowNextLvl()
    {
        if (newLvlLabel != null)
            newLvlLabel.enabled = true;
    }

    public void ShowWarning()
    {
        if (warningLabel != null)
            warningLabel.enabled = true;
    }

    public void CloseNextLvl()
    {
        sound.PlayClickButton();
        newLvlLabel.enabled = false;
    }

    public void CloseWarning()
    {
        sound.PlayClickButton();
        warningLabel.enabled = false;
    }

    public void AddText(string _text)
    {
        if(_text.Length > 0)
        {
            consoleText.Enqueue(_text);
            if(consoleText.Count > 6)
            {
                consoleText.Dequeue();
            }

            string textConsole = "";
            foreach(string t in consoleText)
            {
                textConsole += t + "\n";
            }
            consoleLabel.text = textConsole;
        }
    }

    public void ClearText()
    {
        if(consoleText.Count > 0)
        {
            consoleText.Clear();
            consoleLabel.text = "";
        }
    }

    public void DeactiveButtons(bool value)
    {
        pauseBtn.enabled = value;
        missionBtn.enabled = value;
        perksBtn.enabled = value;
    }

    public void DeactiveBtn(bool value)
    {
        pauseBtn.enabled = value;
        missionBtn.enabled = value;
        mapBtn.enabled = value;
        perksBtn.enabled = value;
        pause.activeEsc = value;
    }
    // Update is called once per frame
    void Update()
    {

        radImg.enabled = playerHealth.isRad;
        bioImg.enabled = playerHealth.isPoison;

        float percentHealth = (float)playerHealth.currentHealth / (float)playerHealth.maxHealth;
        percentHealth = Mathf.Clamp01(percentHealth);
        hpBar.localScale = new Vector3(percentHealth, 1.0f, 1.0f);
        int hour = 0;
        string timeOfDay = "";
        if (time.hour > 12)
            hour = time.hour - 12;
        else
            hour = time.hour;

        if (time.hour > 11)
        {
            if(time.hour == 12 && time.minutes == 0)
                timeOfDay = " AM";
            else
                timeOfDay = " PM";
        }
        else
            timeOfDay = " AM";
            
        string timeStr = "Day " + time.day + " Time " + string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", time.minutes) + timeOfDay;
        timeLabel.text = timeStr;
        if (!blockGUI)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                move.active = false;
            }
            else
            {
                move.active = true;
            }
        }
        else
        {
            move.active = false;
        }
    }

    public string GetTime()
    {
        if (timeLabel == null)
            return "";
        return timeLabel.text;
    }
}
