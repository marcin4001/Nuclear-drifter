using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMapText : MonoBehaviour
{
    public GameObject text;
    private int indexAch = 12;
    private Achievement achievement;
    // Start is called before the first frame update
    void Start()
    {
        achievement = FindObjectOfType<Achievement>();
        if (achievement == null)
        {
            text.SetActive(false);
            return;
        }
        if(!achievement.GetComplete(indexAch))
        {
            text.SetActive(false);
        }
    }

    public void SetActive()
    {
        text.SetActive(true);
    }
}
