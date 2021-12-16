﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarpetInterior : MonoBehaviour
{
    public string sceneName;
    public Vector2 startPos;
    public bool withoutLoading = false;
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
            PropertyPlayer.property.maxHealth = playerHP.maxAfterRad;
            PropertyPlayer.property.isPoison = playerHP.isPoison;
            PropertyPlayer.property.isRad = playerHP.isRad;
            PropertyPlayer.property.levelRad = playerHP.levelRad;

            PropertyPlayer.property.day = time.day;
            PropertyPlayer.property.hour = time.hour;
            PropertyPlayer.property.minutes = time.minutes;

            PropertyPlayer.property.startPos = startPos;
            PropertyPlayer.property.SaveTemp();
            LoadingScreen loading = FindObjectOfType<LoadingScreen>();
            if (loading != null && !withoutLoading) loading.ShowLoading();
            Invoke("Load", 0.1f);
        }
    }

    private void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
