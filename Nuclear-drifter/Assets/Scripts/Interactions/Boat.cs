using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : MonoBehaviour
{
    public float minDistance = 1.6f;
    public string sceneName;
    public Vector2 playerPos;
    public int hour = 12;
    public int idMission;
    public string location;
    private FadePanel fade;
    private GUIScript gUI;
    private PlayerClickMove player;
    private Health playerHP;
    private TimeGame time;
    private SoundsTrigger sound;
    private PauseMenu pause;
    private bool active = true;


    // Start is called before the first frame update
    void Start()
    {
        gUI = FindObjectOfType<GUIScript>();
        player = FindObjectOfType<PlayerClickMove>();
        playerHP = player.GetComponent<Health>();
        time = FindObjectOfType<TimeGame>();
        fade = FindObjectOfType<FadePanel>();
        pause = FindObjectOfType<PauseMenu>();
        sound = FindObjectOfType<SoundsTrigger>();
        MissionObj mission = MissionList.global.GetMission(idMission);
        if(!mission.complete)
        {
            tag = "Obstacle";
            active = false;
        }
    }

    public void ShowText()
    {
        gUI.AddText("This is a boat");
    }

    public void Use()
    {
        if (!active)
            return;

        if (player.ObjIsNearPlayer(transform.position, 1.6f))
        {
            pause.activeEsc = false;
            PropertyPlayer.property.currentHealth = playerHP.currentHealth;
            PropertyPlayer.property.maxHealth = playerHP.maxAfterRad;
            PropertyPlayer.property.isPoison = playerHP.isPoison;
            PropertyPlayer.property.isRad = playerHP.isRad;
            PropertyPlayer.property.levelRad = playerHP.levelRad;

            int newHour = time.hour + hour;
            if (newHour >= 24)
            {
                PropertyPlayer.property.day = time.day + 1;
                PropertyPlayer.property.hour = newHour - 24;
            }
            else
            {
                PropertyPlayer.property.day = time.day;
                PropertyPlayer.property.hour = newHour;
            }
            PropertyPlayer.property.minutes = time.minutes;
            PropertyPlayer.property.startPos = playerPos;
            PropertyPlayer.property.AddDehydration(hour * 60);
            PropertyPlayer.property.SaveTemp();
            if (location != "")
                PropertyPlayer.property.location = location;
            fade.FadeIn();
            Invoke("PlaySound", 1.5f);
        }
        else
        {
            gUI.AddText("The boat is too far");
        }
    }

    private void PlaySound()
    {
        sound.PlayBoat();
        Invoke("Load", 4f);
    }

    private void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
