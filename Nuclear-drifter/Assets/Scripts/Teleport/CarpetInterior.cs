using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarpetInterior : MonoBehaviour
{
    public string sceneName;
    public Vector2 startPos;
    private Health playerHP;
    private TimeGame time;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        time = FindObjectOfType<TimeGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            PropertyPlayer.property.currentHealth = playerHP.currentHealth;
            PropertyPlayer.property.maxHealth = playerHP.maxHealth;
            PropertyPlayer.property.isPoison = playerHP.isPoison;
            PropertyPlayer.property.isRad = playerHP.isRad;

            PropertyPlayer.property.day = time.day;
            PropertyPlayer.property.hour = time.hour;
            PropertyPlayer.property.minutes = time.minutes;

            PropertyPlayer.property.startPos = startPos;
            LoadingScreen loading = FindObjectOfType<LoadingScreen>();
            if (loading != null) loading.ShowLoading();
            Invoke("Load", 0.1f);
        }
    }

    private void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
