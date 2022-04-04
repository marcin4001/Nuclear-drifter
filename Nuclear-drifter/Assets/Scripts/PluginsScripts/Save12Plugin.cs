using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save12Plugin : MonoBehaviour
{
    private string dir = "Saves";
    private string versionText = "Save version: 0.12a";
    private int maxSave = 5;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= maxSave; i++)
        {
            string savePathDir = Path.Combine(Application.persistentDataPath, dir, "save" + i);
            if(Directory.Exists(savePathDir))
            {
                string versionPath = Path.Combine(savePathDir, "version.txt");
                if(!File.Exists(versionPath))
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
