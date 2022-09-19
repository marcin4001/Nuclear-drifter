using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save15Plugin : MonoBehaviour
{
    private string dir = "Saves";
    private string versionText = "Save version: 0.15a";
    private string preVersionText = "Save version: 0.14a";
    private int maxSave = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSaveVersion());
    }

    private IEnumerator ChangeSaveVersion()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 1; i <= maxSave; i++)
        {
            Debug.Log("Save15Plugin");
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

                    File.WriteAllText(versionPath, versionText);
                }
            }
        }
    }
}
