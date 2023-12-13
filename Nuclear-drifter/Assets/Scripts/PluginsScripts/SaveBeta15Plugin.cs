using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SaveBeta15Plugin : MonoBehaviour
{
    public MissionObj[] missions;
    private int maxMissionIndex = 38;

    private string dir = "Saves";
    private string versionText = "Save version: 1.5 Beta";
    private string preVersionText = "Save version: 1.4 Beta";
    private int maxSave = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSaveVersion());
    }

    private IEnumerator ChangeSaveVersion()
    {
        yield return new WaitForSeconds(1.4f);
        for (int i = 1; i <= maxSave; i++)
        {
            Debug.Log("SaveBeta15Plugin");
            string savePathDir = Path.Combine(Application.persistentDataPath, dir, "save" + i);
            if (Directory.Exists(savePathDir))
            {
                string versionPath = Path.Combine(savePathDir, "version.txt");
                string textVerDoc = File.ReadAllText(versionPath);
                if (textVerDoc == preVersionText)
                {
                    MissionList missionList = new MissionList();
                    string missionTxt = File.ReadAllText(Path.Combine(savePathDir, "Mission.json"));
                    JsonUtility.FromJsonOverwrite(missionTxt, missionList);
                    if (missionList.missions.Length < maxMissionIndex)
                    {
                        List<MissionObj> missions1 = missionList.missions.ToList();
                        foreach (MissionObj mission in missions)
                            missions1.Add(mission);
                        missionList.missions = missions1.ToArray();
                        string json = JsonUtility.ToJson(missionList);
                        File.WriteAllText(Path.Combine(savePathDir, "Mission.json"), json);
                    }

                    File.WriteAllText(versionPath, versionText);
                }
            }
        }
    }
}
