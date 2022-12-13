using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class SaveBeta1Plugin : MonoBehaviour
{
    public MissionObj mission;
    private int maxMissionIndex = 33;

    public EnemyMission[] enemies;
    private int maxEnemiesIndex = 37;

    private string dir = "Saves";
    private string versionText = "Save version: 1.0 Beta";
    private string preVersionText = "Save version: 0.15a";
    private int maxSave = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSaveVersion());
    }

    private IEnumerator ChangeSaveVersion()
    {
        yield return new WaitForSeconds(0.6f);
        for (int i = 1; i <= maxSave; i++)
        {
            Debug.Log("SaveBeta1Plugin");
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
                        List<MissionObj> missions = missionList.missions.ToList();
                        missions.Add(mission);
                        missionList.missions = missions.ToArray();
                        string json = JsonUtility.ToJson(missionList);
                        File.WriteAllText(Path.Combine(savePathDir, "Mission.json"), json);
                    }

                    EnemyMissionList enemyList = new EnemyMissionList();
                    string enemyTxt = File.ReadAllText(Path.Combine(savePathDir, "Enemies.json"));
                    JsonUtility.FromJsonOverwrite(enemyTxt, enemyList);
                    if (enemyList.enemies.Length < maxEnemiesIndex)
                    {
                        List<EnemyMission> enemiesTemp = enemyList.enemies.ToList();
                        foreach (EnemyMission enemy in enemies)
                        {
                            enemiesTemp.Add(enemy);
                        }
                        enemyList.enemies = enemiesTemp.ToArray();
                        string json2 = JsonUtility.ToJson(enemyList);
                        File.WriteAllText(Path.Combine(savePathDir, "Enemies.json"), json2);
                    }

                    File.WriteAllText(versionPath, versionText);
                }
            }
        }
    }
}
