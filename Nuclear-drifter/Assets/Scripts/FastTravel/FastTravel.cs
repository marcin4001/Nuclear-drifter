using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastTravel : MonoBehaviour
{
    private PlayerClickMove move;
    public string sceneName;
    private Health playerHP;
    private TimeGame time;
    public Vector2 playerPos;


    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<PlayerClickMove>();
        playerHP = move.GetComponent<Health>();
        time = FindObjectOfType<TimeGame>();
    }

    public void Walk(int hour)
    {
        PropertyPlayer.property.currentHealth = playerHP.currentHealth;
        PropertyPlayer.property.maxHealth = playerHP.maxAfterRad;
        PropertyPlayer.property.isPoison = playerHP.isPoison;
        PropertyPlayer.property.isRad = playerHP.isRad;
        PropertyPlayer.property.levelRad = playerHP.levelRad;

        int newHour = time.hour + hour;
        if(newHour >= 24)
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
        PropertyPlayer.property.SaveTemp();
        SceneManager.LoadScene(sceneName);
    }
}
