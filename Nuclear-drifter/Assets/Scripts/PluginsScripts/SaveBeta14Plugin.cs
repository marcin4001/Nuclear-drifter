using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveBeta14Plugin : MonoBehaviour
{
    public MissionObj mission;
    private int maxMissionIndex = 36;

    private string dir = "Saves";
    private string versionText = "Save version: 1.4 Beta";
    private string preVersionText = "Save version: 1.3 Beta";
    private int maxSave = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSaveVersion());
    }

    private IEnumerator ChangeSaveVersion()
    {
        yield return new WaitForSeconds(1.2f);
        for (int i = 1; i <= maxSave; i++)
        {
            Debug.Log("SaveBeta14Plugin");
            string savePathDir = Path.Combine(Application.persistentDataPath, dir, "save" + i);
            if (Directory.Exists(savePathDir))
            {
                string versionPath = Path.Combine(savePathDir, "version.txt");
                string textVerDoc = File.ReadAllText(versionPath);
                if (textVerDoc == preVersionText)
                {
                    PropertyPlayer property = new PropertyPlayer();
                    string player = File.ReadAllText(Path.Combine(savePathDir, "Player.json"));
                    JsonUtility.FromJsonOverwrite(player, property);
                    string json = JsonUtility.ToJson(property);
                    File.WriteAllText(Path.Combine(savePathDir, "Player.json"), json);


                    MissionList missionList = new MissionList();
                    string missionTxt = File.ReadAllText(Path.Combine(savePathDir, "Mission.json"));
                    JsonUtility.FromJsonOverwrite(missionTxt, missionList);
                    if (missionList.missions.Length < maxMissionIndex)
                    {
                        List<MissionObj> missions = missionList.missions.ToList();
                        missions.Add(mission);
                        missionList.missions = missions.ToArray();
                        string json1 = JsonUtility.ToJson(missionList);
                        File.WriteAllText(Path.Combine(savePathDir, "Mission.json"), json1);
                    }

                    File.WriteAllText(versionPath, versionText);
                }
            }
        }
    }
}
