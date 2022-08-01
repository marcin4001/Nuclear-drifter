using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineStartTextMap : MonoBehaviour
{
    public int idMission = 31;
    public GameObject textMine;
    public GameObject textOther;
    public Carpet doorMine;
    // Start is called before the first frame update
    void Start()
    {
        MissionObj obj = MissionList.global.GetMission(idMission);
        if (obj == null)
        {
            textMine.SetActive(false);
            if(doorMine != null)
                doorMine.gameObject.SetActive(false);
            if (textOther != null)
                textOther.SetActive(false);
            return;
        }
        if (!obj.start)
        {
            textMine.SetActive(false);
            if (doorMine != null)
                doorMine.gameObject.SetActive(false);
            if (textOther != null)
                textOther.SetActive(false);
        }
    }

    public void SetActive()
    {
        textMine.SetActive(true);
        if (doorMine != null)
            doorMine.gameObject.SetActive(true);
        if (textOther != null)
            textOther.SetActive(true);
    }
}
