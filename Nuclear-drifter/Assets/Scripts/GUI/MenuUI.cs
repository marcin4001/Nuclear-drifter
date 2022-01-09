﻿using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        options = FindObjectOfType<OptionsMenu>();
        credits = FindObjectOfType<CreditsCanvas>();
        foreach(Slot slot in startSlots)
        {
            slot.SetId();
        }
    }

    public void NewGame()
    {
        ResetProperty();
        SceneManager.LoadScene("Prologue");
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
        PropertyPlayer.property.currentHealth = 70;
        PropertyPlayer.property.maxHealth = 70;
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
        SaveAndLoad.NewGame();
    }
}
