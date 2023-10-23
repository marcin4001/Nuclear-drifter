 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropertyPlayer : MonoBehaviour
{
    public static PropertyPlayer property;

    public int currentHealth = 0;
    public int maxHealth = 0;
    public bool isRad = false;
    public int levelRad = 0;
    public bool isPoison = false;

    public int day = 0;
    public int hour = 0;
    public int minutes = 0;

    public Vector2 startPos;
    public Vector2 posOutside;
    public int[] foundArea;
    public List<Slot> inv;
    public int currentExp = 0;
    public int level = 1;
    public int prevTh = 0;
    public int lvlPoint = 0;
    public string currentScene;
    public bool[] trapdoorOpened;

    public int waterDay = 0;
    public bool gotMachete = false;
    public List<Slot> backpackInv;
    public string location;
    public bool foodMartFound = false;
    public int dehydration = 0;
    public bool isDehydrated = false;
    public int prevDehydration = 0;
    public bool afterFoodmart = false;
    public string[] consoleText;
    public bool bigSandyFound = false;
    public bool gotPicture = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (!property)
        {
            DontDestroyOnLoad(this.gameObject);
            property = this;
        }
        else Destroy(gameObject);
        
    }

    public void SetCurrentState()
    {
        PlayerClickMove move = FindObjectOfType<PlayerClickMove>();
        Health playerHP = move.GetComponent<Health>();
        TimeGame time = FindObjectOfType<TimeGame>();
        currentScene = SceneManager.GetActiveScene().name;
        

        currentHealth = playerHP.currentHealth;
        property.maxHealth = playerHP.maxAfterRad;
        property.isPoison = playerHP.isPoison;
        property.isRad = playerHP.isRad;
        property.levelRad = playerHP.levelRad;

        property.day = time.day;
        property.hour = time.hour;
        property.minutes = time.minutes;

        property.startPos = move.GetPosPlayer();
        
        property.SaveTemp();
    }

    public void SetTrapdoorOpened(int index)
    {
        if (index >= trapdoorOpened.Length || trapdoorOpened.Length == 0)
            return;
        trapdoorOpened[index] = true;
    }

    public void SetGotMachete()
    {
        gotMachete = true;
    }

    public void SetGotPicture()
    {
        gotPicture = true;
    }

    public void SaveTemp()
    {
        InventoryBox inv = FindObjectOfType<InventoryBox>();
        NPCList nPC = FindObjectOfType<NPCList>();
        if (inv != null) SaveAndLoad.SaveTemp(inv);
        if(nPC != null) SaveAndLoad.SaveTemp(nPC);
    }

    public void OpenBackpack()
    {
        EqChestController eqChestController = FindObjectOfType<EqChestController>();
        if (eqChestController != null)
            eqChestController.OpenBackpack();
    }

    public void AddDehydration(int point)
    {
        dehydration = dehydration + point;
    }

    public void ResetDehydration()
    {
        dehydration = 0;
        prevDehydration = 0;
        isDehydrated = false;
    }

    public void SetIsDehydrated(bool value)
    {
        isDehydrated = value;
    }

    public void SavePrevDehydration()
    {
        prevDehydration = dehydration;
    }

    public void SetAfterFoodmart()
    {
        afterFoodmart = true;
    }

    public static string GetJson()
    {
        property.SetCurrentState();
        string json = JsonUtility.ToJson(property);
        return json;
    }

    public static void JsonToObj(string json)
    {
        JsonUtility.FromJsonOverwrite(json, property);
        ItemDB dB = FindObjectOfType<ItemDB>();
        foreach(Slot slot in property.inv)
        {
            slot.SetItemElement(dB);
        }

        foreach (Slot slot in property.backpackInv)
        {
            slot.SetItemElement(dB);
        }
    }
}
