using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMissionTest : MonoBehaviour
{
    [TextArea(3,4)]
    public string jsonText;
    private bool load = false;
    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 100, 50), "Save"))
        {
            jsonText = JsonUtility.ToJson(MissionList.global);
            load = true;
        }

        if (GUI.Button(new Rect(10, 60, 100, 50), "Load"))
        {
            if(load)
            {
                JsonUtility.FromJsonOverwrite(jsonText, MissionList.global);
            }
        }
    }
}
