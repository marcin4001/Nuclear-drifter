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

    public string urlHowToPlay;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        options = FindObjectOfType<OptionsMenu>();
        credits = FindObjectOfType<CreditsCanvas>();
        foreach(Slot slot in startSlots)
        {
            slot.SetId();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        ResetProperty();
        SceneManager.LoadScene(1);
    }

    public void LoadBtn()
    {
        bool isLoad = SaveAndLoad.Load();
        if (isLoad) SceneManager.LoadScene(PropertyPlayer.property.currentScene);
    }

    public void OpenOptions()
    {
        options.OpenOptions();
    }

    public void OpenCredits()
    {
        credits.Open();
    }

    public void Exit()
    {
        Application.Quit(0);
        Debug.Log("Exit");
    }

    public void HowToPlayOpen()
    {
        Application.OpenURL(urlHowToPlay);
    }

    private void ResetProperty()
    {
        PropertyPlayer.property.currentHealth = 80;
        PropertyPlayer.property.maxHealth = 80;
        PropertyPlayer.property.isPoison = false;
        PropertyPlayer.property.isRad = false;
        PropertyPlayer.property.levelRad = 0;

        PropertyPlayer.property.day = 1;
        PropertyPlayer.property.hour = 5;
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
        MissionList.global.StartMission(idStartMission);

        foreach(MissionObj mission in MissionList.global.missions)
        {
            mission.ResetObj();
        }
        MissionList.global.StartMission(idStartMission);
        foreach (DeviceElement device in DeviceList.global.devices)
        {
            device.Reset();
        }
        PropertyPlayer.property.waterDay = 0;
        SaveAndLoad.NewGame();
    }
}
