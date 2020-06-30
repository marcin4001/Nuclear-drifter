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
        string path = Application.persistentDataPath + @"\saveTemp";
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        Directory.CreateDirectory(path);
    }

    public static void SaveTemp(InventoryBox inv)
    {
        string path = Application.persistentDataPath + @"\saveTemp";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string savePath = path + @"\" + SceneManager.GetActiveScene().name + @"InvBox.json";
        string write = JsonUtility.ToJson(inv);
        //Debug.Log(write);
        File.WriteAllText(savePath, write);
    }

    public static void LoadTemp(InventoryBox inv)
    {
        string path = Application.persistentDataPath + @"\saveTemp\" + SceneManager.GetActiveScene().name + @"InvBox.json";
        if (File.Exists(path))
        {
            Debug.Log("Save Exist");
            string read = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(read, inv);
        }
    }
    
}
