using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionListGUI : MonoBehaviour
{
    public Text titleText;
    public Text textList;
    public ScrollRect scroll;
    private string separator = "##########################################################";
    private Canvas listCanvas;
    private bool active = false;
    private GUIScript gUI;
    private TypeScene typeSc;
    private MapControl map;
    private FadePanel fade;
    private SoundsTrigger sound;

    // Start is called before the first frame update
    void Start()
    {
        listCanvas = GetComponent<Canvas>();
        listCanvas.enabled = false;
        gUI = FindObjectOfType<GUIScript>();
        typeSc = FindObjectOfType<TypeScene>();
        map = FindObjectOfType<MapControl>();
        fade = FindObjectOfType<FadePanel>();
        sound = FindObjectOfType<SoundsTrigger>();
    }

    public void OpenList()
    {
        sound.PlayClickButton();
        active = !active;
        listCanvas.enabled = active;
        gUI.blockGUI = active;
        map.keyActive = !active;
        gUI.DeactiveBtn(!active);
        fade.EnableImg(active);
        typeSc.inMenu = active;
        if (active)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        if (active) CurrentMissionShow();
    }

    public bool GetActive()
    {
        return active;
    }

    public void CurrentMissionShow()
    {
        sound.PlayClickButton();
        scroll.verticalNormalizedPosition = 1.0f;
        titleText.text = "Current missions";
        List<MissionObj> list = MissionList.global.GetListMission();
        string content = "";
        if (list != null)
        {
            foreach(MissionObj mission in list)
            {
                if (mission.start && !mission.complete && mission.ownerName != "")
                {
                    content += mission.task + "\n";
                    if(mission.alt != "")
                        content += "Alternative: " + mission.alt + "\n";
                    content += "NPC: " + mission.ownerName + "\n";
                    content += "Location: " + mission.location + "\n";
                    content += separator;
                }
            }
        }
        if (content != "") content = separator + "\n" + content;
        else content = "no mission\n";
        textList.text = ShowRespect() + content;
    }

    public void CompletedMissionShow()
    {
        sound.PlayClickButton();
        scroll.verticalNormalizedPosition = 1.0f;
        titleText.text = "Completed missions";
        List<MissionObj> list = MissionList.global.GetListMission();
        string content = "";
        if (list != null)
        {
            foreach (MissionObj mission in list)
            {
                if (mission.complete && mission.ownerName != "")
                {
                    content += mission.task + "\n";
                    if (mission.alt != "")
                        content += "Alternative: " + mission.alt + "\n";
                    content += "NPC: " + mission.ownerName + "\n";
                    content += "Location: " + mission.location + "\n";
                    content += separator;
                }
            }
        }
        if (content != "") content = separator + "\n" + content;
        else content = "no mission\n";
        textList.text = ShowRespect() + content;
    }

    public string ShowRespect()
    {
        string respectText = separator + "\n";
        respectText += "Respect in Old Zealand settlements: " + MissionList.global.PercentRespect() + "%\n";
        respectText += "Reputation in the United States Army: " + MissionList.global.PercentRespectUSA() + "%\n";
        respectText += separator + "\n";
        return respectText;
    }

}
