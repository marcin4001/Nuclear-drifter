using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineStartTextMap : MonoBehaviour
{
    public int idMission = 31;
    public GameObject textMine;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj obj = MissionList.global.GetMission(idMission);
        if (obj == null)
        {
            textMine.SetActive(false);
            return;
        }
        if (!obj.start)
            textMine.SetActive(false);
    }

    public void SetActive()
    {
        textMine.SetActive(true);
    }
}
