using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public List<Slot> startSlots;
    public Texture2D cursor;
    public int idStartMission = 0;
    public OptionsMenu options;
    public CreditsCanvas credits;
    public bool[] trapdoorsDefault;

    public string urlHowToPlay;
    public SaveTextInfo[] saveTextInfos;
    public Canvas loadCanvas;
    private SoundsTrigger sound;
    private LoadingScreen loading;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        options = FindObjectOfType<OptionsMenu>();
        credits = FindObjectOfType<CreditsCanvas>();
        loading = FindObjectOfType<LoadingScreen>();
        foreach(Slot slot in startSlots)
        {
            slot.SetId();
        }
        foreach(SaveTextInfo save in saveTextInfos)
        {
            save.SetText();
        }
        loadCanvas.enabled = false;
        sound = GetComponent<SoundsTrigger>();
    }

    public void NewGame()
    {
        ResetProperty();
        sound.PlayClickButton();
        SceneManager.LoadScene("Prologue");
    }

    public void LoadBtn()
    {
        sound.PlayClickButton();
        loadCanvas.enabled = true;
        
    }

    public void CloseLoadPanel()
    {
        sound.PlayClickButton();
        loadCanvas.enabled = false;
    }

    public void Load(int saveNo)
    {
        sound.PlayClickButton();
        if (SaveAndLoad.CanLoad(saveNo))
        {
            bool isLoad = SaveAndLoad.Load(saveNo);
            if (isLoad)
            {
                loading.ShowLoading();
                SceneManager.LoadScene(PropertyPlayer.property.currentScene);
            }
        }
    }

    public void OpenOptions()
    {
        sound.PlayClickButton();
        options.OpenOptions();
    }

    public void OpenCredits()
    {
        sound.PlayClickButton();
        credits.Open();
    }

    public void Exit()
    {
        sound.PlayClickButton();
        Application.Quit(0);
        Debug.Log("Exit");
    }

    public void HowToPlayOpen()
    {
        sound.PlayClickButton();
        Application.OpenURL(urlHowToPlay);
    }

    private void ResetProperty()
    {
        PropertyPlayer.property.currentHealth = 70;
        PropertyPlayer.property.maxHealth = 70;
        PropertyPlayer.property.isPoison = false;
        PropertyPlayer.property.isRad = false;
        PropertyPlayer.property.levelRad = 0;

        PropertyPlayer.property.day = 1;
        PropertyPlayer.property.hour = 6;
        PropertyPlayer.property.minutes = 0;

        PropertyPlayer.property.startPos = new Vector2(235f, 24f);
        PropertyPlayer.property.posOutside = Vector2.zero;
        PropertyPlayer.property.foundArea = new int[9];
        PropertyPlayer.property.foundArea[0] = 2;
        PropertyPlayer.property.foundArea[3] = 1;

        PropertyPlayer.property.inv = startSlots;

        PropertyPlayer.property.currentExp = 0;
        PropertyPlayer.property.level = 1;
        PropertyPlayer.property.prevTh = 0;
        PropertyPlayer.property.lvlPoint = 0;
        MissionList.global.ResetGlobalNPCs();

        foreach(MissionObj mission in MissionList.global.missions)
        {
            mission.ResetObj();
        }
        MissionList.global.StartMission(idStartMission);
        foreach (DeviceElement device in DeviceList.global.devices)
        {
            device.Reset();
        }

        foreach(EnemyMission enemy in EnemyMissionList.global.enemies)
        {
            enemy.Reset();
        }
        SkillsAndPerks.playerSkill.SetDefault();
        AchievementCounter.global.Clear();
        PropertyPlayer.property.waterDay = 0;
        PropertyPlayer.property.trapdoorOpened = trapdoorsDefault;
        PropertyPlayer.property.gotMachete = false;
        PropertyPlayer.property.backpackInv = new List<Slot>();
        PropertyPlayer.property.location = "Wasteland";
        PropertyPlayer.property.foodMartFound = false;
        PropertyPlayer.property.ResetDehydration();
        PropertyPlayer.property.afterFoodmart = false;
        PropertyPlayer.property.bigSandyFound = false;
        PropertyPlayer.property.gotPicture = false;
        SaveAndLoad.NewGame();
    }
}
