using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
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

    public static void LoadTemp(InventoryBox inv)
    {
        string path = Path.Combine(Application.persistentDataPath, "saveTemp");
        string fileName = SceneManager.GetActiveScene().name + "InvBox.json";
        string loadPath = Path.Combine(path, fileName);
        if (File.Exists(loadPath))
        {
            Debug.Log("Save Exist");
            string read = File.ReadAllText(loadPath);
            JsonUtility.FromJsonOverwrite(read, inv);
        }
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

}
