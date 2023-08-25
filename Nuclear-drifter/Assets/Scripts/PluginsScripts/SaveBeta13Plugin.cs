using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveBeta13Plugin : MonoBehaviour
{
    public EnemyMission[] enemies;
    private int maxEnemiesIndex = 40;

    private string dir = "Saves";
    private string versionText = "Save version: 1.3 Beta";
    private string preVersionText = "Save version: 1.2 Beta";
    private int maxSave = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSaveVersion());
    }

    private IEnumerator ChangeSaveVersion()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 1; i <= maxSave; i++)
        {
            Debug.Log("SaveBeta13Plugin");
            string savePathDir = Path.Combine(Application.persistentDataPath, dir, "save" + i);
            if (Directory.Exists(savePathDir))
            {
                string versionPath = Path.Combine(savePathDir, "version.txt");
                string textVerDoc = File.ReadAllText(versionPath);
                if (textVerDoc == preVersionText)
                {
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
                        string json = JsonUtility.ToJson(enemyList);
                        File.WriteAllText(Path.Combine(savePathDir, "Enemies.json"), json);
                    }

                    File.WriteAllText(versionPath, versionText);
                }
            }
        }
    }
}
