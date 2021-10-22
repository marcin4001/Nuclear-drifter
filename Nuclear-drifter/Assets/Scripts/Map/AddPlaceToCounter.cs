using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlaceToCounter : MonoBehaviour
{
    public string namePlace = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hero")
        {
            AchievementCounter.global.AddArea(namePlace);
        }
    }
}
