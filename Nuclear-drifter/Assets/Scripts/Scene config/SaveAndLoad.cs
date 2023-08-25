using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    private static string saveDir = "Saves";
    private static string versionText = "Save version: 1.3 Beta";
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Application.persistentDataPath); 
    }

    public static void NewGame()
    {
        string path = Path.Combine(Application.persistentDataPath,"saveTemp");
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        Directory.CreateDirectory(path);
    }

    public static void SaveTemp(InventoryBox inv)
    {
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string fileName = SceneManager.GetActiveScene().name + "InvBox.json";
        string savePath = Path.Combine(path, fileName);
        string write = JsonUtility.ToJson(inv);
        //Debug.Log(write);
        File.WriteAllText(savePath, write);
    }

    public static bool LoadTemp(InventoryBox inv)
    {
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        string fileName = SceneManager.GetActiveScene().name + "InvBox.json";
        string loadPath = Path.Combine(path, fileName);
        if (File.Exists(loadPath))
        {
            Debug.Log("Save Exist");
            string read = File.ReadAllText(loadPath);
            JsonUtility.FromJsonOverwrite(read, inv);
            return true;
        }
        else
            return false;
    }

    public static void SaveTemp(NPCList listNPC)
    {
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string fileName = SceneManager.GetActiveScene().name + "NPC.json";
        string savePath = Path.Combine(path, fileName);
        string write = JsonUtility.ToJson(listNPC);
        //Debug.Log(write);
        File.WriteAllText(savePath, write);
    }

    public static void LoadTemp(NPCList listNPC)
    {
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        string fileName = SceneManager.GetActiveScene().name + "NPC.json";
        string loadPath = Path.Combine(path, fileName);
        if (File.Exists(loadPath))
        {
            Debug.Log("Save Exist");
            string read = File.ReadAllText(loadPath);
            JsonUtility.FromJsonOverwrite(read, listNPC);
        }
    }

    public static void CloseGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    public static void HardSave(int saveNo)
    {
        Debug.Log(saveDir);
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        string savePath = Path.Combine(Application.persistentDataPath, saveDir, "save" + saveNo);
        string saveToTemp = Path.Combine(savePath, "saveTemp");
        if (Directory.Exists(path))
        {
            string[] files = Directory.GetFiles(path);
            if (Directory.Exists(savePath))
            {
                Directory.Delete(savePath, true);
            }
            Directory.CreateDirectory(savePath);
            Directory.CreateDirectory(saveToTemp);
            string player = PropertyPlayer.GetJson();
            File.WriteAllText(Path.Combine(savePath, "Player.json"), player);
            string mission = MissionList.GetJson();
            File.WriteAllText(Path.Combine(savePath, "Mission.json"), mission);
            string devices = DeviceList.GetJson();
            File.WriteAllText(Path.Combine(savePath, "Devices.json"), devices);
            string enemy = EnemyMissionList.GetJson();
            File.WriteAllText(Path.Combine(savePath, "Enemies.json"), enemy);
            string skills = SkillsAndPerks.GetJson();
            File.WriteAllText(Path.Combine(savePath, "Skills.json"), skills);
            string achCounters = AchievementCounter.GetJson();
            File.WriteAllText(Path.Combine(savePath, "AchCounter.json"), achCounters);
            foreach (string file in files)
            {
                Debug.Log(Path.GetFileName(file));
                File.Copy(file, Path.Combine(saveToTemp, Path.GetFileName(file)));
            }
            GUIScript gUI = FindObjectOfType<GUIScript>();
            System.DateTime dateTime = System.DateTime.Now;
            
            string infoSave = "";
            infoSave += "Save " + saveNo + " - " + dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString() + "\n";
            infoSave += "Location: " + PropertyPlayer.property.location + "\n";
            infoSave += "Level: " + PropertyPlayer.property.level + "\n";
            infoSave += gUI != null ? gUI.GetTime() : "";
            

            
            File.WriteAllText(Path.Combine(savePath, "InfoSave.txt"), infoSave);
            string versionPath = Path.Combine(savePath, "version.txt");
            File.WriteAllText(versionPath, versionText);
        }
    }

    public static bool Load(int saveNo)
    {
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        string savePath = Path.Combine(Application.persistentDataPath, saveDir, "save" + saveNo);
        string saveToTemp = Path.Combine(savePath, "saveTemp");
        if (Directory.Exists(savePath))
        {
            string player = File.ReadAllText(Path.Combine(savePath, "Player.json"));
            PropertyPlayer.JsonToObj(player);
            string mission = File.ReadAllText(Path.Combine(savePath, "Mission.json"));
            MissionList.JsonToObj(mission);
            string devices = File.ReadAllText(Path.Combine(savePath, "Devices.json"));
            DeviceList.JsonToObj(devices);
            string enemy = File.ReadAllText(Path.Combine(savePath, "Enemies.json"));
            EnemyMissionList.JsonToObj(enemy);
            string skills = File.ReadAllText(Path.Combine(savePath, "Skills.json"));
            SkillsAndPerks.JsonToObj(skills);
            if (File.Exists(Path.Combine(savePath, "AchCounter.json")))
            {
                string achCounters = File.ReadAllText(Path.Combine(savePath, "AchCounter.json"));
                AchievementCounter.JsonToObj(achCounters);
            }
            string[] files = Directory.GetFiles(saveToTemp);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
            foreach (string file in files)
            {
                File.Copy(file, Path.Combine(path, Path.GetFileName(file)));
            }
            return true;
        }
        return false;
    }

    public static bool CanLoad(int saveNo)
    {
        string savePathDir = Path.Combine(Application.persistentDataPath, saveDir, "save" + saveNo);
        if (!Directory.Exists(savePathDir))
            return false;
        string infoSave = Path.Combine(savePathDir, "InfoSave.txt");
        return File.Exists(infoSave);
    }

    public static string GetSaveInfo(int saveNo)
    {
        string empty = "Save " + saveNo + " - Empty";
        string savePathDir = Path.Combine(Application.persistentDataPath, saveDir, "save" + saveNo);
        if (!Directory.Exists(savePathDir))
            return empty;
        string infoSave = Path.Combine(savePathDir, "InfoSave.txt");
        if (File.Exists(infoSave))
            return File.ReadAllText(infoSave);
        else
            return empty;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            Debug.Log(GetSaveInfo(1));
    }
}
