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
    private PauseMenu pause;

    // Start is called before the first frame update
    void Start()
    {
        listCanvas = GetComponent<Canvas>();
        listCanvas.enabled = false;
        gUI = FindObjectOfType<GUIScript>();
        typeSc = FindObjectOfType<TypeScene>();
        map = FindObjectOfType<MapControl>();
        fade = FindObjectOfType<FadePanel>();
        pause = FindObjectOfType<PauseMenu>();
    }

    public void OpenList()
    {
        active = !active;
        listCanvas.enabled = active;
        gUI.blockGUI = active;
        map.keyActive = !active;
        pause.activeEsc = !active;
        fade.EnableImg(active);
        typeSc.SetInMenu();
        if (active) CurrentMissionShow();
    }

    public bool GetActive()
    {
        return active;
    }

    public void CurrentMissionShow()
    {
        scroll.verticalNormalizedPosition = 1.0f;
        titleText.text = "Current missions";
        List<MissionObj> list = MissionList.global.GetListMission();
        string content = "";
        if (list != null)
        {
            foreach(MissionObj mission in list)
            {
                if (mission.start && !mission.complete)
                {
                    content += mission.task + "\n";
                    content += "NPC: " + mission.ownerName + "\n";
                    content += "Location: " + mission.location + "\n";
                    content += separator;
                }
            }
        }
        if (content != "") content = separator + "\n" + content;
        else content = "no mission\n";
        textList.text = content;
    }

    public void CompletedMissionShow()
    {
        scroll.verticalNormalizedPosition = 1.0f;
        titleText.text = "Completed missions";
        List<MissionObj> list = MissionList.global.GetListMission();
        string content = "";
        if (list != null)
        {
            foreach (MissionObj mission in list)
            {
                if (mission.complete)
                {
                    content += mission.task + "\n";
                    content += "NPC: " + mission.ownerName + "\n";
                    content += "Location: " + mission.location + "\n";
                    content += separator;
                }
            }
        }
        if (content != "") content = separator + "\n" + content;
        else content = "no mission\n";
        textList.text = content;
    }

}
