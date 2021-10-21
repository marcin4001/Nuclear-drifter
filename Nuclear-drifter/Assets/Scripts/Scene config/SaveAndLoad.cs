using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    private static string saveDir = "save_08a";
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

    public static void HardSave()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        string savePath = Path.Combine(Application.persistentDataPath, saveDir);
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
        }
    }

    public static bool Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        string savePath = Path.Combine(Application.persistentDataPath, saveDir);
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

    public static bool CanLoad()
    {
        string savePath = Path.Combine(Application.persistentDataPath, saveDir);
        return Directory.Exists(savePath);
    }

}
