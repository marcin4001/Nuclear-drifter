using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOffLight : MonoBehaviour
{
    public GameObject directionalLight;
    public GameObject playerLight;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            directionalLight.SetActive(false);
            playerLight.SetActive(true);
        }
    }
}
