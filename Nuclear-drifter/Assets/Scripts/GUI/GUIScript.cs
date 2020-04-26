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

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        time = FindObjectOfType<TimeGame>();
        move = FindObjectOfType<PlayerClickMove>();
        expLabel.text = "lvl:1 exp:0/500";
        consoleLabel.text = "";
        consoleText = new Queue<string>();
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
    // Update is called once per frame
    void Update()
    {
            radImg.enabled = playerHealth.isRad;
            bioImg.enabled = playerHealth.isPoison;

            float percentHealth = (float)playerHealth.currentHealth / (float)playerHealth.maxHealth;
            percentHealth = Mathf.Clamp01(percentHealth);
            hpBar.localScale = new Vector3(percentHealth, 1.0f, 1.0f);

            string timeStr = "Day " + time.day + " Time " + string.Format("{0:00}", time.hour) + ":" + string.Format("{0:00}", time.minutes);
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
    }
}
