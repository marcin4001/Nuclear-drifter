using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveBeta12Plugin : MonoBehaviour
{
    public MissionObj[] missions;
    private int maxMissionIndex = 35;

    public EnemyMission[] enemies;
    private int maxEnemiesIndex = 38;

    private string dir = "Saves";
    private string versionText = "Save version: 1.2 Beta";
    private string preVersionText = "Save version: 1.0 Beta";
    private int maxSave = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSaveVersion());
    }

    private IEnumerator ChangeSaveVersion()
    {
        yield return new WaitForSeconds(0.8f);
        for (int i = 1; i <= maxSave; i++)
        {
            Debug.Log("SaveBeta12Plugin");
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
                        foreach(MissionObj mission in missions)
                            missions1.Add(mission);
                        missionList.missions = missions1.ToArray();
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

                    PropertyPlayer property = new PropertyPlayer();
                    string player = File.ReadAllText(Path.Combine(savePathDir, "Player.json"));
                    JsonUtility.FromJsonOverwrite(player, property);
                    string json3 = JsonUtility.ToJson(property);
                    File.WriteAllText(Path.Combine(savePathDir, "Player.json"), json3);

                    File.WriteAllText(versionPath, versionText);
                }
            }
        }
    }
}
