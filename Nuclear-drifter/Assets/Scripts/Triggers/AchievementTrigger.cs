using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTrigger : MonoBehaviour
{
    private Achievement controller;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<Achievement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Hero")
        {
            controller.SetAchievement(index);
        }

    }
}
